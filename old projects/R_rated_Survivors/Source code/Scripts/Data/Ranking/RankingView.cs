using DBConfig;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingView : MonoBehaviour
{
    [SerializeField] GameObject rankingUserUI;
    [SerializeField] GameObject infoMSG;
    List<GameObject> rankingUsers = new List<GameObject>();
    int pageNum = 0;
    int pageCount = 0;
    // Start is called before the first frame update
    private void Start()
    {
        PageCalculation();
    }

    // 페이지 수를 계산하고 데이터베이스 연결 상태를 확인하는 메서드입니다.
    public void PageCalculation()
    {
        (bool, string) ck = RankingManager.Instance.DBConnectTest();
        if (ck.Item1)
        {

            pageCount = RankingManager.Instance.RankingUserCount();
            if (pageCount == 0)
            {
                infoMSG.GetComponent<TMP_Text>().text = "정보 없음";
                infoMSG.SetActive(true);
                return;
            }
            else infoMSG.SetActive(false);

            if (pageCount % 10 == 0) pageCount = pageCount / 10;
            else pageCount = pageCount / 10 + 1;

        }
        else
        {

            infoMSG.GetComponent<TMP_Text>().text = ck.Item2;
            infoMSG.SetActive(true);
        }
    }

    // 랭킹 화면을 설정하는 메서드입니다. 페이지 번호를 증가 또는 감소시키고, 랭킹 데이터를 불러와 화면에 표시합니다.
    public void SetRankingView(bool set)
    {
        if (set)
        {
            if(pageNum == pageCount) pageNum = 0;
            pageNum++;
        }
        else if (!set)
        {
            if (pageNum == 1) pageNum = pageCount + 1;
            pageNum--;
        }
        else if (!RankingManager.Instance.DBConnectTest().Item1) return;
        
        if(rankingUsers.Count != 0) DestroyUserUI();

        List<UserInfo> user = RankingManager.Instance.RankingView(pageNum);


        for(int i = 0; i < user.Count; i++)
        {
            rankingUserUI.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{i + 1+((pageNum-1)*10)}";
            rankingUserUI.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{user[i].User_Name}";
            rankingUserUI.transform.GetChild(2).GetComponent<TMP_Text>().text = $"{user[i].User_Score}";

            GameObject uiObject = Instantiate(rankingUserUI, transform.position, Quaternion.identity);
            rankingUsers.Add(uiObject);
            RectTransform rect = uiObject.GetComponent<RectTransform>();
            rect.SetParent(gameObject.transform);
            rect.localPosition = new Vector3(0f, 125+(-30f * i), 0f);
        }
    }

    // 기존에 생성된 사용자 UI를 제거하는 메서드입니다.
    void DestroyUserUI()
    {
        for(int i = 0;i < rankingUsers.Count; i++)
        {
            Destroy(rankingUsers[i]);
        }
    }

    // 취소 버튼을 눌렀을 때 호출되는 메서드입니다. 페이지 번호를 초기화합니다.
    public void CancleBtn()
    {
        pageNum = 0;
    }
}
