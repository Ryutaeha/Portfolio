
using UnityEngine;

public class ClientVisitor : MonoBehaviour
{
    #region Fields
    // 각종 파워업 객체를 저장하는 필드입니다.
    public PowerUp speedPowerUp;
    public PowerUp shieldPowerUp;
    public PowerUp weaponPowerUp;

    // 캐릭터 컨트롤러 객체를 저장하는 필드입니다.
    private VisitorCharacterController _characterController;
    #endregion

    #region Unity Methods
    // 스크립트가 시작될 때 호출되는 메서드입니다.
    private void Start()
    {
        // VisitorCharacterController 컴포넌트를 게임 오브젝트에 추가합니다.
        _characterController = gameObject.AddComponent<VisitorCharacterController>();
    }

    // Unity의 GUI 시스템을 사용하여 화면에 버튼을 표시합니다.
    private void OnGUI()
    {
        if (GUILayout.Button("PowerUp Shield")) _characterController.Accept(shieldPowerUp);

        if (GUILayout.Button("PowerUp Speed")) _characterController.Accept(speedPowerUp);

        if (GUILayout.Button("PowerUp Weapon")) _characterController.Accept(weaponPowerUp);
    }
    #endregion
}
