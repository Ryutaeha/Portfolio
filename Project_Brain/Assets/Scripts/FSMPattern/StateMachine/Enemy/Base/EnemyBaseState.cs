using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    #region Fields
    protected EnemyStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;
    #endregion

    #region Constructor
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Enemy.Data.GroundData;
    }
    #endregion

    #region Methods

    // 상태가 시작될 때 호출되는 메서드입니다.
    public virtual void Enter()
    {
    }

    // 상태가 종료될 때 호출되는 메서드입니다.
    public virtual void Exit()
    {
    }

    // 입력을 처리하는 메서드입니다.
    public virtual void HandleInput()
    {
    }

    // 물리 업데이트를 처리하는 메서드입니다.
    public virtual void PhysicsUpdate()
    {
    }

    // 상태를 업데이트하는 메서드입니다.
    public virtual void Update()
    {
        Move();
    }

    // 애니메이션을 시작하는 메서드입니다.
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    // 애니메이션을 중지하는 메서드입니다.
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    // 적을 이동시키는 메서드입니다.
    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);

        Rotate(movementDirection);
    }

    // 이동 방향을 계산하는 메서드입니다.
    private Vector3 GetMovementDirection()
    {
        Vector3 direction = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).normalized;

        return direction;
    }

    // 특정 방향으로 적을 이동시키는 메서드입니다.
    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy.Controller.Move((direction * movementSpeed + stateMachine.Enemy.ForceReceiver.Movement) * Time.deltaTime);
    }

    // 이동 속도를 계산하는 메서드입니다.
    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    // 적을 특정 방향으로 회전시키는 메서드입니다.
    void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Enemy.transform.rotation = Quaternion.Lerp(stateMachine.Enemy.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    // 강제로 이동시키는 메서드입니다.
    protected void ForceMove()
    {
        stateMachine.Enemy.Controller.Move(stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    }

    // 애니메이터에서 특정 태그의 정상화된 시간을 반환하는 메서드입니다.
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag)) return nextInfo.normalizedTime;

        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag)) return currentInfo.normalizedTime;

        else return 0f;
    }

    // 적이 플레이어를 추적할 범위 내에 있는지 확인하는 메서드입니다.
    protected bool IsInChasingRange()
    {
        if (stateMachine.Target.IsDie) return false;

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRnage * stateMachine.Enemy.Data.PlayerChasingRnage;
    }
    #endregion
}
