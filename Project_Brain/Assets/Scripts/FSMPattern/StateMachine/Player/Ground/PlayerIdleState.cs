using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    #region Constructor
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods
    // 상태가 시작될 때 호출되는 메서드입니다.
    // 이동 속도 수정자를 0으로 설정하고, 대기 상태 애니메이션을 시작합니다.
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    // 상태가 종료될 때 호출되는 메서드입니다.
    // 대기 상태 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    // 상태가 업데이트될 때 호출되는 메서드입니다.
    // 플레이어의 이동 입력을 확인하고, 이동 입력이 있을 경우 걷기 상태로 전환합니다.
    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
            return;
        }
    }
    #endregion
}
