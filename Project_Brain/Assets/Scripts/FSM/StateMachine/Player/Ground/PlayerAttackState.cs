public class PlayerAttackState : PlayerBaseState
{
    #region Constructor
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods
    // 상태가 시작될 때 호출되는 메서드입니다.
    // 플레이어의 이동 속도를 0으로 설정하고, 공격 애니메이션을 시작합니다.
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    // 상태가 종료될 때 호출되는 메서드입니다.
    // 공격 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }
    #endregion
}
