using System;
using System.Collections;
using UnityEngine;

public class CharacterArm : MonoBehaviour
{
    
    #region Fields
    // ArmConfig 인스턴스입니다. 무기의 기본 설정을 담고 있습니다.
    public ArmConfig armConfig;

    // ArmAttachment 인스턴스입니다. 메인 부착물 설정을 담고 있습니다.
    public ArmAttachment mainAttachment;

    // ArmAttachment 인스턴스입니다. 보조 부착물 설정을 담고 있습니다.
    public ArmAttachment secondaryAttachment;

    // 무기가 발사 중인지 여부를 나타내는 플래그입니다.
    private bool _isFiring;

    // IArm 인스턴스입니다. 무기의 현재 상태를 나타냅니다.
    private IArm _arm;

    // 무기가 장식되었는지 여부를 나타내는 플래그입니다.
    private bool _isDecorated;
    #endregion

    #region Unity Methods
    // IArm을 초기화합니다.
    private void Start()
    {
        _arm = new Arm(armConfig);
    }

    // 각종 정보를 띄웁니다.
    private void OnGUI()
    {
        GUI.color = Color.green;

        GUI.Label(new Rect(5, 50, 150, 100), $"Range : {_arm.Range}");

        GUI.Label(new Rect(5, 70, 150, 100), $"Strength : {_arm.Strength}");

        GUI.Label(new Rect(5, 90, 150, 100), $"Cooldown : {_arm.Cooldown}");

        GUI.Label(new Rect(5, 110, 150, 100), $"Firing Rate : {_arm.Rate}");

        GUI.Label(new Rect(5, 130, 150, 100), $"Weapon Firing : {_isFiring}");

        if (mainAttachment && _isDecorated) GUI.Label(new Rect(5, 150, 500, 100), $"Main Attachment : {mainAttachment.name}");

        if (secondaryAttachment && _isDecorated) GUI.Label(new Rect(5, 170,500, 100), $"Secondary Attachment : {secondaryAttachment.name}");
    }

    // 무기의 상태를 초기화합니다.
    public void Reset()
    {
        _arm = new Arm(armConfig);
        _isDecorated = !_isDecorated;
    }
    #endregion

    #region Methods
    // 무기의 발사 상태를 토글합니다.
    public void ToggleFire()
    {
        _isFiring = !_isFiring;

        if (_isFiring) StartCoroutine(FireWeapon());
    }

    // 무기를 장식합니다.
    public void Decorate()
    {
        if (mainAttachment && !secondaryAttachment) _arm = new ArmDecorator(_arm, mainAttachment);

        if (mainAttachment && secondaryAttachment) _arm = new ArmDecorator(new ArmDecorator(_arm, mainAttachment), secondaryAttachment);

        _isDecorated = !_isDecorated;
    }
    #endregion

    #region Coroutine
    // 무기를 발사하는 코루틴입니다.
    private IEnumerator FireWeapon()
    {
        float firingRate = 1.0f / _arm.Rate;

        while (_isFiring)
        {
            yield return new WaitForSeconds(firingRate);
            Debug.Log($"fire {DateTime.Now}");
        }
    }
    #endregion
}
