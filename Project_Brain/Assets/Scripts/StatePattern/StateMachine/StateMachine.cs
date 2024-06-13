
#region Advantages
// 장점
// - state 전환을 명확하게 관리할 수 있습니다. 각 state는 독립적으로 구현되므로 state별 동작을 쉽게 정의할 수 있습니다.
// - 코드의 가독성과 유지보수성이 향상됩니다. state별로 분리된 클래스를 통해 각 state의 로직을 명확하게 구분할 수 있습니다.
// - state 전환 로직을 중앙에서 관리할 수 있어, state 전환의 흐름을 쉽게 추적하고 디버깅할 수 있습니다.
// - state별로 다른 동작을 정의할 수 있어, 다양한 state에 대해 유연하게 대응할 수 있습니다.
// - state 패턴을 사용하여 객체의 행동을 변경할 수 있어, 동적인 state 변화를 쉽게 처리할 수 있습니다.
#endregion

#region Disadvantages
// 단점
// - state가 많아질 경우 관리가 복잡해질 수 있습니다. state별로 클래스를 생성해야 하므로 파일 수가 증가할 수 있습니다.
// - state 전환 로직이 복잡해질 수 있습니다. state 전환 조건이 많아질 경우 로직이 복잡해질 수 있습니다.
// - state별로 반복되는 코드가 발생할 수 있습니다. 비슷한 동작을 하는 state가 많을 경우 코드 중복이 발생할 수 있습니다.
// - state 객체가 많아질 경우 메모리 사용량이 증가할 수 있습니다. 각 state 객체가 메모리를 차지하게 됩니다.
// - state 전환 시 성능에 영향을 미칠 수 있습니다. 빈번한 state 전환이 발생할 경우 성능 저하가 발생할 수 있습니다.
#endregion

#region When to Use
// state 머신을 사용할만한 경우
// - 객체의 state가 명확하게 구분되는 경우: 예를 들어, 플레이어의 이동, 공격, 대기 등의 state를 명확히 구분하고 관리할 때.
// - state 전환이 빈번하게 발생하는 경우: 게임 내에서 state 전환이 자주 발생하여 이를 효과적으로 관리하고자 할 때.
// - state별로 다른 행동을 정의해야 하는 경우: 각 state에 따라 객체의 행동이 달라져야 할 때.
// - state 전환 로직을 중앙에서 관리하고 싶을 때: state 전환의 흐름을 일관성 있게 관리하고 디버깅하기 쉽게 만들고자 할 때.
// - 코드의 가독성과 유지보수성을 높이고 싶을 때: state별로 분리된 클래스를 통해 코드의 가독성과 유지보수성을 향상시키고자 할 때.
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
