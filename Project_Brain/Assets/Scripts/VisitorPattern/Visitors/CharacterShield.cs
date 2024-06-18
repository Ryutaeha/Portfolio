
using UnityEngine;

public class CharacterShield : MonoBehaviour, ICharacterElement
{
    #region Fields
    // 캐릭터의 실드 체력을 나타내는 필드입니다. 기본값은 50입니다.
    public float health = 50f;
    #endregion

    #region Unity Methods
    // Unity의 GUI 시스템을 사용하여 화면에 실드 체력을 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.green;

        // 실드 체력을 화면의 특정 위치에 레이블로 표시합니다.
        GUI.Label(new Rect(125, 0, 200, 20), $"Shield Health: {health}");
    }
    #endregion

    #region Methods
    // 실드에 데미지를 입힐 때 호출됩니다.
    // damage 매개변수는 입힐 데미지의 양을 나타냅니다.
    // 데미지를 입힌 후 남은 실드 체력을 반환합니다.
    public float Damage(float damage)
    {
        health -= damage;
        return health;
    }

    // 방문자 패턴을 구현하기 위한 메서드입니다.
    // visitor 매개변수로 전달된 방문자 객체가 이 실드 객체를 방문할 수 있도록 합니다.
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
    #endregion
}
