
#region Advantages
// 장점
// - 시스템의 상태와 전환을 명확하게 정의할 수 있습니다. 상태와 전환 규칙이 명확히 구분되므로 이해하기 쉽습니다.
// - 상태 전이가 명확하게 정의되어 있어, 예측 가능한 동작을 보장할 수 있습니다.
// - 복잡한 시스템의 동작을 단순화할 수 있습니다. 상태와 전환을 통해 복잡한 로직을 간단하게 표현할 수 있습니다.
// - 상태 전환을 시각적으로 표현할 수 있어, 시스템의 동작을 쉽게 이해할 수 있습니다.
#endregion

#region Disadvantages
// 단점
// - 상태와 전환 규칙이 많아질 경우 관리가 복잡해질 수 있습니다. 상태와 전환이 많을수록 로직이 복잡해집니다.
// - 상태 전환 조건이 복잡해질 경우 디버깅이 어려워질 수 있습니다. 전환 조건이 많을수록 디버깅이 복잡해집니다.
// - 상태 전환 시 성능에 영향을 미칠 수 있습니다. 빈번한 상태 전환이 발생할 경우 성능 저하가 발생할 수 있습니다.
// - 상태와 전환을 정의하는 데 많은 시간이 소요될 수 있습니다. 초기 설정에 많은 노력이 필요합니다.
#endregion

#region When to Use
// FSM 패턴을 사용할 만한 경우
// - 시스템의 상태가 명확하게 구분되는 경우: 예를 들어, 프로토콜 상태 관리, 사용자 인터페이스 상태 관리 등.
// - 상태 전환이 빈번하게 발생하는 경우: 상태 전환이 자주 발생하여 이를 효과적으로 관리하고자 할 때.
// - 상태와 전환 규칙을 명확히 정의해야 하는 경우: 시스템의 동작을 상태와 전환 규칙으로 명확히 표현해야 할 때.
// - 복잡한 시스템의 동작을 단순화하고자 할 때: 복잡한 로직을 상태와 전환을 통해 단순화하고자 할 때.
// - 상태 전환을 시각적으로 표현하고자 할 때: 상태 다이어그램 등을 통해 시스템의 동작을 시각적으로 표현하고자 할 때.
#endregion


// state의 행동을 정의하는 인터페이스입니다.
// 각 state는 이 인터페이스를 구현하여 state 전환 시의 동작을 정의하게 됩니다.
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
    // 현재 state를 나타내는 필드입니다.
    protected IState currentState;
    #endregion

    #region Methods
    // 현재 state를 변경하는 method입니다.
    // 기존 state의 Exit method를 호출하고, 새로운 state의 Enter method를 호출합니다.
    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    // 현재 state의 HandleInput method를 호출하는 method입니다.
    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    // 현재 state의 Update method를 호출하는 method입니다.
    public void Update()
    {
        currentState.Update();
    }

    // 현재 state의 PhysicsUpdate method를 호출하는 method입니다.
    public void PhysicsUpdate()
    {
        currentState.PhysicsUpdate();
    }
    #endregion
}
