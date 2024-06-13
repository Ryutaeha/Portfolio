using System;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    // 이 이벤트는 플레이어가 이동할 때 호출됩니다.
    public event Action<Vector2> OnMoveEvent;

    // 이 이벤트는 플레이어가 조준할 때 호출됩니다.
    public event Action<Vector2> OnLookEvent;

    // 이 이벤트는 플레이어가 스킬을 사용할 때 호출됩니다.
    public event Action OnSpaceSkillEvent;

    // 이 메서드는 이동 이벤트를 호출합니다.
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    // 이 메서드는 조준 이벤트를 호출합니다.
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    // 이 메서드는 스킬 사용 이벤트를 호출합니다.
    public void CallSpaceSkillEvent()
    {
        OnSpaceSkillEvent?.Invoke();
    }
}
