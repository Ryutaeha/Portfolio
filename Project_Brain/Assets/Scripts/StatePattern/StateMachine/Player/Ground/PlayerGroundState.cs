using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    #region Constructor
    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods
    // 상태가 시작될 때 호출되는 메서드입니다.
    // 지상 상태 애니메이션을 시작합니다.
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    // 상태가 종료될 때 호출되는 메서드입니다.
    // 지상 상태 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    // 상태가 업데이트될 때 호출되는 메서드입니다.
    // 플레이어가 공격 중인지 확인하고 공격 상태로 전환합니다.
    public override void Update()
    {
        base.Update();

        if (stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    // 물리 업데이트가 호출될 때 실행되는 메서드입니다.
    // 플레이어가 지면에 닿아 있는지 확인하고, 떨어지기 상태로 전환합니다.
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!stateMachine.Player.Controller.isGrounded && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
        }
    }

    // 이동이 취소되었을 때 호출되는 메서드입니다.
    // 이동 입력이 없을 경우 대기 상태로 전환합니다.
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero) return;

        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMovementCanceled(context);
    }

    // 점프가 시작되었을 때 호출되는 메서드입니다.
    // 점프 상태로 전환합니다.
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        base.OnJumpStarted(context);
        stateMachine.ChangeState(stateMachine.JumpState);
    }

    // 공격이 시작되었을 때 호출되는 메서드입니다.
    // 콤보 공격 상태로 전환합니다.
    protected virtual void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.ComboAttackState);
    }
    #endregion
}
