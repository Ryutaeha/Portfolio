public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}

public class StateMachine
{
    #region Fields
    protected IState currentState;
    #endregion

    #region Methods
    // 현재 상태를 변경하는 메서드입니다.
    // 기존 상태의 Exit 메서드를 호출하고, 새로운 상태의 Enter 메서드를 호출합니다.
    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    // 현재 상태의 HandleInput 메서드를 호출하는 메서드입니다.
    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    // 현재 상태의 Update 메서드를 호출하는 메서드입니다.
    public void Update()
    {
        currentState.Update();
    }

    // 현재 상태의 PhysicsUpdate 메서드를 호출하는 메서드입니다.
    public void PhysicsUpdate()
    {
        currentState.PhysicsUpdate();
    }
    #endregion
}
