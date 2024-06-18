using System.Collections.Generic;
using UnityEngine;


public class VisitorCharacterController : MonoBehaviour, ICharacterElement
{
    #region Fields
    // 캐릭터의 구성 요소들을 저장하는 리스트입니다.
    private List<ICharacterElement> _characterElements = new();
    #endregion

    #region Unity Methods
    // 캐릭터에 무기, 쉴드, 속도 컴포넌트를 추가하고 리스트에 저장합니다.
    private void Start()
    {
        _characterElements.Add(gameObject.AddComponent<CharacterShield>());

        _characterElements.Add(gameObject.AddComponent<CharacterSpeed>());

        _characterElements.Add(gameObject.AddComponent<CharacterWeapon>());
    }
    #endregion

    #region Methods
    // 방문자 패턴을 구현하기 위한 메서드입니다.
    // visitor 매개변수로 전달된 방문자 객체가 캐릭터의 모든 구성 요소를 방문할 수 있도록 합니다.
    public void Accept(IVisitor visitor)
    {
        foreach (ICharacterElement element in _characterElements)
        {
            element.Accept(visitor);
        }
    }
    #endregion
}
