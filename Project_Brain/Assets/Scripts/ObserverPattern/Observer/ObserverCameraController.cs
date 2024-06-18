using UnityEngine;

public class ObserverCameraController : Observer
{
    #region Fields
    // 충전 상태를 나타내는 불리언 변수입니다.
    private bool _isChargingOn;

    // 카메라의 초기 위치를 저장하는 변수입니다.
    private Vector3 _initialPosition;

    // 카메라 흔들림의 강도를 나타내는 변수입니다.
    private float _shakeMagnitude = 0.1f;

    // ObserverController 인스턴스를 저장하기 위한 변수입니다.
    private ObserverController _observerController;
    #endregion

    #region Unity Methods

    // 이 메서드에서는 카메라의 초기 위치를 저장합니다.
    private void OnEnable()
    {
        _initialPosition = gameObject.transform.localPosition;
    }

    // 충전 상태에 따라 카메라의 위치를 흔들거나 초기 위치로 되돌립니다.
    private void Update()
    {
        if (_isChargingOn)
        {
            gameObject.transform.localPosition = _initialPosition + (Random.insideUnitSphere * _shakeMagnitude);
        }
        else
        {
            gameObject.transform.localPosition = _initialPosition;
        }
    }
    #endregion

    #region Methods
    // Subject로부터 알림을 받는 메서드입니다.
    // 이 메서드에서는 ObserverController의 인스턴스를 가져와 충전 상태를 업데이트합니다.
    public override void Notify(Subject subject)
    {
        if (!_observerController)
        {
            _observerController = subject.GetComponent<ObserverController>();
        }

        if (_observerController)
        {
            _isChargingOn = _observerController.IsChargingOn;
        }
    }
    #endregion
}
