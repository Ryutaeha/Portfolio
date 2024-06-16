using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    #region Constructor
    public PlayerAirState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods

    // 플레이어가 공중 상태에 진입할 때 호출됩니다.
    // 공중 애니메이션을 시작합니다.
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    // 플레이어가 공중 상태를 종료할 때 호출됩니다.
    // 공중 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }
    #endregion
}
