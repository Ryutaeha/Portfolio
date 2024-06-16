
#region Advantages
// 장점
// - 상태 전환을 명확하게 관리할 수 있습니다. 각 상태는 독립적으로 구현되므로 상태별 동작을 쉽게 정의할 수 있습니다.
// - 코드의 가독성과 유지보수성이 향상됩니다. 상태별로 분리된 클래스를 통해 각 상태의 로직을 명확하게 구분할 수 있습니다.
// - 상태 전환 로직을 중앙에서 관리할 수 있어, 상태 전환의 흐름을 쉽게 추적하고 디버깅할 수 있습니다.
// - 상태별로 다른 동작을 정의할 수 있어, 다양한 상태에 대해 유연하게 대응할 수 있습니다.
// - 상태 패턴을 사용하여 객체의 행동을 변경할 수 있어, 동적인 상태 변화를 쉽게 처리할 수 있습니다.
#endregion

#region Disadvantages
// 단점
// - 상태가 많아질 경우 관리가 복잡해질 수 있습니다. 상태별로 클래스를 생성해야 하므로 파일 수가 증가할 수 있습니다.
// - 상태 전환 로직이 복잡해질 수 있습니다. 상태 전환 조건이 많아질 경우 로직이 복잡해질 수 있습니다.
// - 상태별로 반복되는 코드가 발생할 수 있습니다. 비슷한 동작을 하는 상태가 많을 경우 코드 중복이 발생할 수 있습니다.
// - 상태 객체가 많아질 경우 메모리 사용량이 증가할 수 있습니다. 각 상태 객체가 메모리를 차지하게 됩니다.
// - 상태 전환 시 성능에 영향을 미칠 수 있습니다. 빈번한 상태 전환이 발생할 경우 성능 저하가 발생할 수 있습니다.
#endregion

#region When to Use
// 상태 패턴을 사용할 만한 경우
// - 객체의 상태가 명확하게 구분되는 경우: 예를 들어, 플레이어의 이동, 공격, 대기 등의 상태를 명확히 구분하고 관리할 때.
// - 상태 전환이 빈번하게 발생하는 경우: 게임 내에서 상태 전환이 자주 발생하여 이를 효과적으로 관리하고자 할 때.
// - 상태별로 다른 행동을 정의해야 하는 경우: 각 상태에 따라 객체의 행동이 달라져야 할 때.
// - 상태 전환 로직을 중앙에서 관리하고 싶을 때: 상태 전환의 흐름을 일관성 있게 관리하고 디버깅하기 쉽게 만들고자 할 때.
// - 코드의 가독성과 유지보수성을 높이고 싶을 때: 상태별로 분리된 클래스를 통해 코드의 가독성과 유지보수성을 향상시키고자 할 때.
#endregion


// 큐브의 상태를 처리하는 메서드를 정의하는 인터페이스입니다.
// 큐브의 상태가 변경될 때마다 Handle 메서드가 호출됩니다.
public interface ICubeState
{
    public void Handle(CubeController cubeController);
}

public class CubeStateContext
{
    #region Fields
    // 큐브의 상태를 처리하는 컨트롤러 객체입니다.
    protected readonly CubeController _cubeController;
    #endregion

    #region Properties
    // 현재 큐브의 상태를 나타내는 프로퍼티입니다.
    public ICubeState CurrentState { get; set; }
    #endregion

    #region Constructor
    // 큐브 상태 컨텍스트를 초기화하는 생성자입니다.
    public CubeStateContext(CubeController cubeController)
    {
        _cubeController = cubeController;
    }
    #endregion

    #region Methods
    // 현재 상태의 Handle 메서드를 호출하여 상태를 전환합니다.
    public void Transition()
    {
        CurrentState.Handle(_cubeController);
    }

    // 새로운 상태로 전환하고, 해당 상태의 Handle 메서드를 호출합니다.
    public void Transition(ICubeState state)
    {
        CurrentState = state;
        CurrentState.Handle(_cubeController);
    }
    #endregion
}
