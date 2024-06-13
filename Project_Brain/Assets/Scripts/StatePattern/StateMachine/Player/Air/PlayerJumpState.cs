using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    #region Constructor
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods

    // 플레이어가 점프 상태에 진입할 때 호출됩니다.
    // 점프력을 설정하고 점프 애니메이션을 시작합니다.
    public override void Enter()
    {
        stateMachine.JumpForce = stateMachine.Player.Data.AirData.JumpForce;
        stateMachine.Player.ForceReceiver.Jump(stateMachine.JumpForce);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    // 플레이어가 점프 상태를 종료할 때 호출됩니다.
    // 점프 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    // 매 프레임마다 호출됩니다.
    // 플레이어의 수직 속도를 확인하여 낙하 상태로 전환할지 결정합니다.
    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.Controller.velocity.y <= 0)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }
    #endregion
}
