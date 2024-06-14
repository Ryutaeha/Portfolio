using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DBConfig
{
    public class UserInfo
    {
        public int User_No;
        public string User_Name;
        public int User_Score;
        public string User_Date;
        public UserInfo(int user_No, string user_name, int user_score, string user_date)
        {
            User_No = user_No;
            User_Name = user_name;
            User_Score = user_score;
            User_Date = user_date;
        }
    }

    // 서버 연결값을 설정하는 클래스입니다.
    public class DBSetting
    {
        // 데이터베이스 서버 주소 (고정값)
        public string Server { get; } = "000.000.00.000";

        // 데이터베이스 사용자 이름 (고정값)
        public string User { get; } = "RSurvivor";

        // 사용할 데이터베이스 이름 (고정값)
        public string Database { get; } = "RSurvivorUnity";

        // 데이터베이스 연결 비밀번호 (고정값)
        public string Password { get; } = "1234";

        // 데이터베이스 연결 포트 (고정값)
        public string Port { get; } = "3306";

        public string ConnectionString
        {
            get
            {
                // 연결 문자열 생성 및 반환
                return $"server={Server};user={User};database={Database};port={Port};password={Password}";
            }
        }
    }
    public class RankingSystem
    {
        DBSetting setting = new DBSetting();

        // 이 메서드는 데이터베이스 연결을 테스트합니다. 연결이 성공적으로 이루어지면 true를 반환하고, 그렇지 않으면 false와 에러 메시지를 반환합니다.
        public (bool, string) ConnectionTest()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(setting.ConnectionString))
                {
                    conn.Open();
                }
                return (true, "");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Exception: {ex.Number} : {ex.Message}");
                return (false, $"MySQL Exception: {ex.Number} : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return (false, $"Exception: {ex.Message}");
            }

        }

        // 이 메서드는 특정 페이지 번호에 따른 사용자 랭킹 목록을 반환합니다. 페이지 번호에 따라 10명의 사용자를 반환합니다.
        public List<UserInfo> RankList(int pageNum)
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(setting.ConnectionString))
                {

                    connection.Open();

                    //순위와 관련된 정보를 가져오는 SELECT 쿼리를 사용
                    string sql = "SELECT * FROM user ORDER BY user_score DESC LIMIT @Limit, @PageSize";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    int pageSize = 10; // 페이지당 표시할 항목 수
                    int limit = (pageNum - 1) * pageSize; // 페이지 번호에 따른 오프셋 계산
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@Limit", limit);

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int userNo = Convert.ToInt32(rdr["USER_NO"]);
                            string userName = (string)rdr["USER_NAME"];
                            int userScore = Convert.ToInt32(rdr["USER_SCORE"]);
                            DateTime userInsertDate = Convert.ToDateTime(rdr["USER_INSERT_DATE"]);

                            list.Add(new UserInfo(userNo, userName, userScore, userInsertDate.ToString()));
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Exception: {ex.Number} : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return list;
        }

        // 이 메서드는 새로운 사용자의 이름과 점수를 데이터베이스에 추가합니다. 성공 여부와 서버 메시지를 반환합니다.
        public (bool,string) InsertRanking(string name, int score)
        {
            bool Success = false;
            string serverMsg;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(setting.ConnectionString))
                {

                    connection.Open();

                    string sql = "INSERT into user VALUES (nextval(user_no_seq),@Name,@Score,NOW());";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Score", score);
                    // Add parameters for other columns as needed

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} rows inserted.");
                    Success = true;
                    serverMsg = "등록완료";
                }
            }
            catch (MySqlException ex)
            {
                serverMsg = $"MySQL Exception: {ex.Number} : {ex.Message}";
            }
            catch (Exception ex)
            {
                serverMsg = $"Exception: {ex.Message}";
            }
            return (Success, serverMsg);
        }

        // 이 메서드는 데이터베이스에 저장된 사용자 수를 반환합니다.
        public int RankingUserCount()
        {
            int userCount = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(setting.ConnectionString))
                {

                    connection.Open();

                    //순위와 관련된 정보를 가져오는 SELECT 쿼리를 사용
                    string sql = "SELECT COUNT(*) as user FROM user;";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        
                        if (rdr.Read()) userCount = Convert.ToInt32(rdr["user"]);
                    }
                }

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Exception: {ex.Number} : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return userCount;
        }
    }
   
}
