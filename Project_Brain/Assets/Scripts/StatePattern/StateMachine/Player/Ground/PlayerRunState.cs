public class PlayerRunState : PlayerGroundState
{
    #region Constructor
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods
    // 상태가 시작될 때 호출되는 메서드입니다.
    // 이동 속도 수정자를 달리기 속도로 설정하고, 달리기 상태 애니메이션을 시작합니다.
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.RunSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    // 상태가 종료될 때 호출되는 메서드입니다.
    // 달리기 상태 애니메이션을 중지합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }
    #endregion
}
