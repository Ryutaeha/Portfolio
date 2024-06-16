using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    #region Constructor
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods

    // 플레이어가 낙하 상태에 진입할 때 호출됩니다.
    // 낙하 애니메이션을 시작합니다.
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    // 플레이어가 낙하 상태를 종료할 때 호출됩니다.
    // 낙하 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    // 매 프레임마다 호출됩니다.
    // 플레이어가 지면에 닿았는지 확인하고 필요한 경우 상태를 변경합니다.
    public override void Update()
    {
        base.Update();
        if (stateMachine.Player.Controller.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
    #endregion
}
