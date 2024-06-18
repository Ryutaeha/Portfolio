using UnityEngine;

public class ClientObserver : MonoBehaviour
{
    #region Fields
    // ObserverController 인스턴스를 저장하기 위한 필드입니다.
    private ObserverController _observerController;
    #endregion

    #region Unity Methods
    // Unity의 Start 메서드를 재정의하여 처음에 호출됩니다.
    // 이 메서드에서는 ObserverController의 인스턴스를 찾습니다.
    private void Start()
    {
        _observerController = (ObserverController)FindObjectOfType(typeof(ObserverController));
    }

    // 이 메서드에서는 두 개의 버튼을 생성합니다.
    // 첫 번째 버튼은 "Damage This"로, 클릭 시 ObserverController의 TakeDamage 메서드를 호출합니다.
    // 두 번째 버튼은 "Toggle Charging"으로, 클릭 시 ObserverController의 ToggleCharging 메서드를 호출합니다.
    private void OnGUI()
    {
        if (GUILayout.Button("Damage This"))
        {
            if (_observerController) _observerController.TakeDamage(15f);
        }

        if (GUILayout.Button("Toggle Charging"))
        {
            if (_observerController) _observerController.ToggleCharging();
        }
    }
    #endregion
}
