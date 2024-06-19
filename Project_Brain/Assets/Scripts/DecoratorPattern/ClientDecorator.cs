using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientDecorator : MonoBehaviour
{
    #region Fields
    // CharacterArm 인스턴스를 저장합니다.
    private CharacterArm _characterArm;

    // Arm이 장식되었는지 여부를 나타내는 플래그입니다.
    private bool _isArmDecorated;
    #endregion

    #region Unity Methods
    // 게임 내에서 CharacterArm의 인스턴스를 찾습니다.
    private void Start()
    {

        _characterArm = (CharacterArm)FindObjectOfType(typeof(CharacterArm));
    }

    // 각종 정보를 띄웁니다.
    private void OnGUI()
    {
        if (!_isArmDecorated)
        {
            if (GUILayout.Button("Decorate Arm"))
            {
                _characterArm.Decorate();
                _isArmDecorated = !_isArmDecorated;
            }
        }

        if (_isArmDecorated)
        {
            if (GUILayout.Button("Reset Arm"))
            {
                _characterArm.Reset();
                _isArmDecorated = !_isArmDecorated;
            }
        }

        if (GUILayout.Button("Toggle Fire")) _characterArm.ToggleFire();

    }
    #endregion
}
