using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Default;
    public UI_Scene CurSceneUI { get; protected set; }

    private bool _Initialized = false;

    // 게임이 시작될 때 호출되며, 프레임 속도와 VSync 설정, 커서 상태를 초기화합니다.
    // 또한, 리소스가 이미 로드되었는지 확인하고, 그렇지 않다면 비동기적으로 리소스를 로드한 후 초기화를 수행합니다.
    void Awake()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
        Cursor.lockState = CursorLockMode.Locked;

        if (Main.Resource.Loaded) Initialize();
        else
        {
            Main.Resource.LoadAllAsync<UnityEngine.Object>("PreLoad", (key, count, totalCount) => {
                if (count >= totalCount)
                {
                    Main.Resource.Loaded = true;
                    Initialize();
                }
            });
        }
    }

    // 씬이 삭제될 때 자원을 정리하는 작업을 수행합니다.
    public abstract void Clear();

    // 이 메서드는 씬을 초기화하는 역할을 합니다.
    // 사운드 초기화, EventSystem 생성 등을 수행합니다.
    // 또한 게임을 시작하고 해상도를 설정하며, 에디터 모드에서는 커서 상태를 잠금으로 설정합니다.
    protected virtual bool Initialize()
    {
        if (_Initialized) return false;
        Main.Sound.InitializedSound();
        Object obj = FindObjectOfType<EventSystem>();
        if (obj == null) obj = Main.Resource.Instantiate("EventSystem.prefab");
        obj.name = "@EventSystem";
        DontDestroyOnLoad(obj);
        Main.Game.StartGame();
        Main.Scenes.SetResolution();
#if UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
#endif
        _Initialized = true;
        return true;
    }
}

