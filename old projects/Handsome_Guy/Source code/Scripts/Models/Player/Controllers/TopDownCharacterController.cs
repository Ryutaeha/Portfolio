using System;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public event Action<Vector2> onMoveEvent;
    public event Action<Vector2> onLookEvent;
    public event Action onAttackEvent;
    public event Action onInteractionEvent;
    public event Action onSkillEvent;

    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    protected virtual void Update()
    {
        AttackDelay();
    }

    private void AttackDelay()
    {
        if(_timeSinceLastAttack <= 0.2f)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        if(IsAttacking &&  _timeSinceLastAttack > 0.2f)
        {
            _timeSinceLastAttack = 0;
             CallAttackEvent();
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        onMoveEvent?.Invoke(direction);
    }
    public void CallLookEvent(Vector2 direction)
    {
        onLookEvent?.Invoke(direction);
    }

    public void CallAttackEvent()
    {
        onAttackEvent?.Invoke();
    }

    public void CallInteratcionEvent()
    {
        onInteractionEvent?.Invoke();
    }

    public void CallSkillEvent()
    {
        onSkillEvent?.Invoke();
    }
}
