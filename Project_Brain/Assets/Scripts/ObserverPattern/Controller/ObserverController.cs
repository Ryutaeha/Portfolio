using UnityEngine;

public class ObserverController : Subject
{
    #region Fields
    // 게임이 진행 중인지 여부를 나타내는 불리언 변수입니다.
    private bool _isGameOn;

    // ObserverHUDController 인스턴스를 저장하기 위한 필드입니다.
    private ObserverHUDController _observerHUDController;

    // ObserverCameraController 인스턴스를 저장하기 위한 필드입니다.
    private ObserverCameraController _observerCameraController;

    // 플레이어의 체력을 저장하는 필드입니다. 인스펙터에서 설정할 수 있습니다.
    [SerializeField] private float health = 100f;
    #endregion

    #region Properties
    // 충전이 켜져 있는지 여부를 나타내는 속성입니다. 외부에서 설정할 수 없습니다.
    public bool IsChargingOn { get; private set; }

    // 현재 체력을 반환하는 속성입니다.
    public float CurrentHealth { get { return health; } }
    #endregion

    #region Unity Methods
    // 이 메서드에서는 ObserverHUDController와 ObserverCameraController의 인스턴스를 초기화합니다.
    private void Awake()
    {
        _observerHUDController = gameObject.AddComponent<ObserverHUDController>();
        _observerCameraController = (ObserverCameraController)FindObjectOfType(typeof(ObserverCameraController));
    }

    // 이 메서드에서는 게임을 시작합니다.
    private void Start()
    {
        StartGame();
    }

    // 이 메서드에서는 HUD와 카메라 컨트롤러를 Observer로 등록합니다.
    private void OnEnable()
    {
        if (_observerHUDController) Attach(_observerHUDController);

        if (_observerCameraController) Attach(_observerCameraController);
    }

    // 이 메서드에서는 HUD와 카메라 컨트롤러의 Observer 등록을 해제합니다.
    private void OnDisable()
    {
        if (_observerHUDController) Detach(_observerHUDController);

        if (_observerCameraController) Detach(_observerCameraController);
    }
    #endregion

    #region Methods
    // 게임을 시작하는 메서드입니다. 게임이 시작되었음을 알리고 Observer에게 알림을 보냅니다.
    private void StartGame()
    {
        _isGameOn = true;
        NotifyObservers();
    }

    // 충전 상태를 토글하는 메서드입니다. 게임이 진행 중일 때만 동작합니다.
    // 상태가 변경되면 Observer에게 알림을 보냅니다.
    public void ToggleCharging()
    {
        if (_isGameOn) IsChargingOn = !IsChargingOn;

        NotifyObservers();
    }

    // 데미지를 입는 메서드입니다. 주어진 양만큼 체력을 감소시키고 충전을 중지합니다.
    // 체력이 0 이하로 떨어지면 객체를 파괴합니다.
    // 상태가 변경되면 Observer에게 알림을 보냅니다.
    public void TakeDamage(float amount)
    {
        health -= amount;
        IsChargingOn = false;

        NotifyObservers();

        if (health < 0) Destroy(gameObject);
    }
    #endregion
}
