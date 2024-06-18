
using UnityEngine;

public class CharacterWeapon : MonoBehaviour, ICharacterElement
{
    #region Fields
    [Header("Range")]
    // 무기의 현재 사거리를 나타내는 필드입니다. 기본값은 5입니다.
    public int range = 5;

    // 무기의 최대 사거리를 나타내는 필드입니다. 기본값은 25입니다.
    public int maxRange = 25;

    [Header("Strength")]
    // 무기의 현재 공격력을 나타내는 필드입니다. 기본값은 25입니다.
    public float strength = 25f;

    // 무기의 최대 공격력을 나타내는 필드입니다. 기본값은 50입니다.
    public float maxStrength = 50f;
    #endregion

    #region Unity Methods
    // Unity의 GUI 시스템을 사용하여 화면에 무기의 사거리와 공격력을 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.green;

        GUI.Label(new Rect(125, 40, 200, 20), $"Weapon Range : {range}");

        GUI.Label(new Rect(125, 60, 200, 20), $"Weapon Strength: {strength}");
    }
    #endregion

    #region Methods
    // 무기를 발사할 때 호출되는 메서드입니다.
    public void Fire()
    {
        Debug.Log("Weapon Fired");
    }

    // 방문자 패턴을 구현하기 위한 메서드입니다.
    // visitor 매개변수로 전달된 방문자 객체가 이 무기 객체를 방문할 수 있도록 합니다.
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
    #endregion
}
