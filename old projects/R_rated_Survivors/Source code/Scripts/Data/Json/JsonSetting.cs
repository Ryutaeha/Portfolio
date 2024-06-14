
using UnityEngine;
using static GameManager;
using Newtonsoft.Json;


public class JsonSetting : MonoBehaviour
{
    private static JsonSetting instance;


    // 이 속성은 JsonSetting 클래스의 인스턴스를 반환합니다.
    // 만약 인스턴스가 존재하지 않으면 새로 생성합니다.
    public static JsonSetting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JsonSetting>();

                if (instance == null)
                {
                    GameObject container = new GameObject("JsonSettings");
                    instance = container.AddComponent<JsonSetting>();
                }
            }

            return instance;
        }
    }

    // 싱글톤 패턴을 구현하여 인스턴스가 중복되지 않도록 합니다.
    private void Awake()
    {
        if (gameObject.name == "PlayerSelectCanvas")
        {
            return;
        }

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        
        DontDestroyOnLoad(gameObject);

        
        instance = this;
    }


    // 이 메서드는 주어진 직업 이름을 사용하여 해당 직업의 설정 데이터를 반환합니다.
    // JSON 파일을 읽고 직업 이름에 해당하는 데이터를 찾아 반환합니다.
    public JobSettings AbilitySettings(string jobName)
    {

        // "(Clone)"이 포함되어 있으면 제외, 없으면 그대로
        string baseName = jobName.Contains("(Clone)") ? jobName.Replace("(Clone)", "").TrimEnd() : jobName;
        JobContainer jobContainer = new JobContainer();

        // 파일 경로 설정
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/CharacterDate");
        // 파일이 존재하는지 확인
        if(jsonFile != null)
        {
            // JSON 문자열을 객체로 역직렬화 (Newtonsoft.Json 사용)
            jobContainer = JsonConvert.DeserializeObject<JobContainer>(jsonFile.text);
            // 특정 플레이어 가져오기
            string targetJobSettingsKey = baseName;
            if (jobContainer.Jobs.ContainsKey(targetJobSettingsKey))
            {
                //받은 오브젝트 이름을 키값으로 해당 값이 들어있는 json객체를 JobSettings으로 포맷하여 넣어줌
                JobSettings targetJobSettings = jobContainer.Jobs[targetJobSettingsKey];
                //성공시 데이터 리턴
                return targetJobSettings;
            }
            else Debug.LogError("Job not found: " + targetJobSettingsKey);
        }
        else Debug.LogError("File not found: Json/CharacterDate");
        //실패시 null 리턴
        return null;
    }

    // 이 메서드는 주어진 직업 이름을 사용하여 해당 직업의 레벨 업 능력 데이터를 반환합니다.
    // JSON 파일을 읽고 직업 이름에 해당하는 데이터를 찾아 반환합니다.
    public JobAbility AbilityUP(string jobName)
    {
        // "(Clone)"이 포함되어 있으면 제외, 없으면 그대로
        string baseName = jobName.Contains("(Clone)") ? jobName.Replace("(Clone)", "").TrimEnd() : jobName;
        AbilityContainer abilityContainer = new AbilityContainer();

        // 파일 경로 설정
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/CharacterDate");
        // 파일이 존재하는지 확인
        //if (File.Exists(filePath))
        if (jsonFile != null)
        {
            // JSON 문자열을 객체로 역직렬화 (Newtonsoft.Json 사용)
            abilityContainer = JsonConvert.DeserializeObject<AbilityContainer>(jsonFile.text);

            // 특정 플레이어 가져오기
            string targetJobSettingsKey = baseName;
            if (abilityContainer.Ability.ContainsKey(targetJobSettingsKey))
            {
                //받은 오브젝트 이름을 키값으로 해당 값이 들어있는 json객체를 JobSettings으로 포맷하여 넣어줌
                JobAbility targetJobAbility = abilityContainer.Ability[targetJobSettingsKey];
                //성공시 데이터 리턴
                return targetJobAbility;
            }
            else Debug.LogError("Job not found: " + targetJobSettingsKey);
        }
        else Debug.LogError("File not found: Json/CharacterDate");
        //실패시 null 리턴
        return null;
    }
}
