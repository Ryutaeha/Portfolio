using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    #region Constructor
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods

    // 적이 추적 상태에 진입할 때 호출됩니다.
    // 이동 속도를 설정하고 추적 애니메이션을 시작합니다.
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
    }

    // 적이 추적 상태를 종료할 때 호출됩니다.
    // 추적 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
    }

    // 매 프레임마다 호출됩니다.
    // 추적 상태에서 필요한 로직을 처리합니다.
    public override void Update()
    {
        base.Update();
        if (!IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }

    // 적이 공격 범위 내에 있는지 확인합니다.
    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }

    #endregion
}
