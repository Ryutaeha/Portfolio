#region Advantages
// 장점
// - 코드의 유연성과 확장성이 향상됩니다. 새로운 전략을 추가하는 것이 기존 코드를 수정하지 않고도 가능해집니다.
// - 코드의 응집도가 높아집니다. 알고리즘을 별도의 클래스에 분리하여 각 클래스가 하나의 책임만 가지도록 할 수 있습니다.
// - 런타임에 전략을 변경할 수 있습니다. 실행 중에 전략 객체를 교체함으로써 동적으로 알고리즘을 변경할 수 있습니다.
// - 코드 재사용성이 증대됩니다. 다양한 맥락(Context)에서 동일한 전략을 재사용할 수 있습니다.
// - 클라이언트 코드가 간결해집니다. 클라이언트는 전략의 구체적인 구현에 대해 알 필요가 없고, 전략 인터페이스를 통해서만 상호작용합니다.
#endregion

#region Disadvantages
// 단점
// - 전략 객체가 많아질 수 있습니다. 다양한 전략이 필요한 경우 전략 클래스가 많아져 관리가 복잡해질 수 있습니다.
// - 클라이언트 코드에서 전략을 인스턴스화하고 관리해야 합니다. 클라이언트가 적절한 전략을 선택하고 설정해야 하므로 어느 정도의 복잡성이 추가됩니다.
// - 작은 규칙 변경에도 많은 클래스가 필요할 수 있습니다. 알고리즘의 작은 변경 사항에도 새로운 전략 클래스를 만들어야 할 수 있습니다.
// - 전략 패턴을 잘못 사용하면 오히려 코드의 가독성과 유지보수성이 떨어질 수 있습니다. 전략을 남용하거나 불필요하게 분리하면 코드가 복잡해질 수 있습니다.
#endregion

#region When to Use
// 전략 패턴을 사용할 만한 경우
// - 여러 알고리즘이 필요한 경우: 동일한 작업을 수행하지만 다양한 방식으로 처리해야 하는 경우.
// - 알고리즘을 런타임에 변경해야 하는 경우: 실행 중에 알고리즘을 동적으로 변경해야 할 때.
// - 알고리즘의 구현을 클라이언트로부터 분리하고 싶은 경우: 클라이언트 코드가 알고리즘의 세부 사항을 알 필요 없이 전략 인터페이스를 통해 상호작용하도록 하고 싶을 때.
// - 조건문이나 switch 문을 대체하고 싶은 경우: 여러 조건에 따라 다른 동작을 수행해야 하는 상황을 전략 패턴으로 대체하여 코드의 복잡성을 줄이고 싶을 때.
// - 코드의 재사용성을 높이고 싶은 경우: 다양한 맥락(Context)에서 동일한 알고리즘을 재사용할 수 있도록 하고 싶을 때.
#endregion


public interface IManeuverBehaviour
{
    #region Methods
    // 이 메서드는 적 큐브(EnemyCube)가 특정한 기동을 수행하도록 합니다.
    // 메서드에 전달되는 enemyCube 매개변수는 기동을 수행할 적 큐브 객체를 나타냅니다.
    // 이 인터페이스를 구현하는 클래스는 이 메서드를 통해 다양한 기동 동작을 정의할 수 있습니다.
    public void Maneuver(EnemyCube enemyCube);
    #endregion
}