using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeed : MonoBehaviour, ICharacterElement
{
    #region Fields
    // 캐릭터의 현재 부스트 값을 저장합니다. 기본값은 25입니다.
    public float Boost = 25f;

    // 캐릭터가 가질 수 있는 최대 부스트 값을 저장합니다. 기본값은 200입니다.
    public float maxBoost = 200;

    // 캐릭터가 충전 모드인지 여부를 나타내는 불리언 값입니다.
    private bool _isChargingOn;

    // 캐릭터의 기본 속도를 저장합니다. 기본값은 300입니다.
    private float _defaultSpeed = 300f;
    #endregion

    #region Properties
    // 캐릭터의 현재 속도를 계산하여 반환합니다.
    // 충전 모드일 때는 기본 속도에 부스트 값을 더한 속도를 반환합니다.
    public float CurrentSpeed
    {
        get
        {
            if (_isChargingOn) return _defaultSpeed + Boost;
            return _defaultSpeed;
        }
    }
    #endregion

    #region Unity Methods
    // Unity의 GUI 시스템을 사용하여 화면에 부스트 값을 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.green;

        GUI.Label(new Rect(125, 20, 200, 20), $"Boost: {Boost}");
    }
    #endregion

    #region Methods
    // 캐릭터의 충전 모드를 토글합니다.
    public void ToggleCharging()
    {
        _isChargingOn = !_isChargingOn;
    }

    // 방문자 패턴을 구현하기 위한 메서드입니다.
    // visitor 매개변수로 전달된 방문자 객체가 이 속도 객체를 방문할 수 있도록 합니다.
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
    #endregion
}
