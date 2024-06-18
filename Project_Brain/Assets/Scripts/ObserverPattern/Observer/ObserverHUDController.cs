using UnityEngine;

public class ObserverHUDController : Observer
{
    #region Fields
    // 충전 상태를 나타내는 불리언 변수입니다.
    private bool _isChargingOn;

    // 현재 체력을 저장하는 변수입니다.
    private float _currentHealth;

    // ObserverController 인스턴스를 저장하기 위한 변수입니다.
    private ObserverController _observerController;
    #endregion

    #region Unity Methods

    // 이 메서드에서는 HUD를 표시하고, 충전 상태 및 체력에 따라 경고 메시지를 표시합니다.
    private void OnGUI()
    {
        // HUD 영역을 설정합니다.
        GUILayout.BeginArea(new Rect(50, 50, 100, 200));

        // 현재 체력을 표시하는 라벨을 만듭니다.
        GUILayout.BeginHorizontal("box");
        GUILayout.Label($"Health: {_currentHealth}");
        GUILayout.EndHorizontal();

        // 충전 상태가 활성화된 경우 메시지를 표시합니다.
        if (_isChargingOn)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Charging Activated");
            GUILayout.EndHorizontal();
        }

        // 현재 체력이 50 이하인 경우 경고 메시지를 표시합니다.
        if (_currentHealth <= 50f)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("WARNING: Low Health");
            GUILayout.EndHorizontal();
        }

        // HUD 영역을 끝냅니다.
        GUILayout.EndArea();
    }
    #endregion

    #region Methods

    // Subject로부터 알림을 받는 메서드입니다.
    // 이 메서드에서는 ObserverController의 인스턴스를 가져와 충전 상태와 현재 체력을 업데이트합니다.
    public override void Notify(Subject subject)
    {
        if (!_observerController)
        {
            _observerController = subject.GetComponent<ObserverController>();
        }

        if (_observerController)
        {
            _isChargingOn = _observerController.IsChargingOn;
            _currentHealth = _observerController.CurrentHealth;
        }
    }
    #endregion
}
