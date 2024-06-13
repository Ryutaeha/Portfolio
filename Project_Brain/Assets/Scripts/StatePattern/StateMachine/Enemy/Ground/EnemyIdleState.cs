using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    #region Constructor
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods

    // 적이 대기 상태에 진입할 때 호출됩니다.
    // 적의 이동 속도를 0으로 설정하고 대기 애니메이션을 시작합니다.
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    // 적이 대기 상태를 종료할 때 호출됩니다.
    // 대기 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    // 매 프레임마다 호출됩니다.
    // 추적 범위에 있는지 확인하고 필요한 경우 상태를 변경합니다.
    public override void Update()
    {
        base.Update();
        if (IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }
    #endregion
}
