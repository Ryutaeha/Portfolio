using System.Collections;
using UnityEngine;

// 메인 클래스는 싱글톤 패던을 사용하여 게임의 여러 관리자를 초기화하고 관리하는 역할을 합니다.
public class Main : MonoBehaviour
{
    // 싱글톤 인스턴스와 초기화 상태를 저장하는 정적 변수입니다.
    private static Main _instance;
    private static bool _initialized;

    // 싱글톤 인스턴스를 변환하는 프로퍼티입니다.
    private static Main Instance
    {
        get
        {
            if (_initialized) return _instance;
            _initialized = true;
            GameObject main = GameObject.Find("@Main");
            if (main != null) return _instance;
            main = new GameObject { name = "@Main" };
            main.AddComponent<Main>();
            DontDestroyOnLoad(main);
            _instance = main.GetComponent<Main>();
            _instance.Initialize();
            return _instance;
        }
    }
    
    // 초기화 메서드입니다.
    private void Initialize()
    {
        _uiManager.Initialize();
        _serviceLocator.SetServiceLocator();
    }

    // 각종 관리자 클래스의 인스턴스를 저장하는 필드입니다.
    #region Fields
    private ServiceLocator _serviceLocator = new();
    private readonly GameManager _gameManager = new();
    private readonly DataManager _dataManager = new();
    private readonly PoolManager _poolManager = new();
    private readonly ResourceManager _resourceManager = new();
    private readonly ScenesManager _scenesManager = new();
    private SoundManager _soundManager = new();
    private readonly TimeManager _timeManager = new();
    private readonly UIManager _uiManager = new();
    private DataManager.StageClear _stageClear = new();
    #endregion

    // 각종 관리자 클래스의 인스턴스를 외부에서 접근할 수 있도록 하는 프로퍼티입니다.
    #region Properties
    public static ServiceLocator Service => Instance._serviceLocator;
    public static string NextScene { get; set; }
    public static bool FirstLord { get; set; } = true;
    public static GameManager Game => Instance._gameManager;
    public static DataManager Data => Instance._dataManager;
    public static PoolManager Pool => Instance._poolManager;
    public static ResourceManager Resource => Instance._resourceManager;
    public static ScenesManager Scenes => Instance._scenesManager;
    public static SoundManager Sound
    {
        get => Instance._soundManager;
        set => Instance._soundManager = value;
    }
    public static TimeManager Time => Instance._timeManager;
    public static UIManager UI => Instance._uiManager;
    public static DataManager.StageClear StageClear
    {
        get => Instance._stageClear;
        set => Instance._stageClear = value;
    }
    #endregion

    //코루틴을 시작하고 중지하는 헬퍼 메서드 입니다.
    #region CoroutineHelper

    public new static Coroutine StartCoroutine(IEnumerator coroutine) => (Instance as MonoBehaviour).StartCoroutine(coroutine);
    public new static void StopCoroutine(Coroutine coroutine) => (Instance as MonoBehaviour).StopCoroutine(coroutine);
    public static void StopAllCoroutine() => (Instance as MonoBehaviour).StopAllCoroutines();
    #endregion
}
