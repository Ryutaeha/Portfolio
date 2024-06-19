#region Advantages
// 장점
// - 객체에 동적으로 기능을 추가할 수 있습니다. 상속을 사용하지 않고도 객체의 기능을 확장할 수 있습니다.
// - 기존 클래스의 수정 없이 새로운 기능을 추가할 수 있기 때문에 개방-폐쇄 원칙(Open/Closed Principle)을 준수합니다.
// - 다양한 기능을 조합하여 사용할 수 있습니다. 여러 데코레이터를 조합하여 복잡한 기능을 구현할 수 있습니다.
// - 코드의 유연성과 재사용성이 향상됩니다. 데코레이터 클래스를 별도로 정의하여 다양한 객체에 재사용할 수 있습니다.
// - 클래스 계층 구조가 복잡해지는 것을 방지할 수 있습니다. 기능 확장을 위해 상속을 남발하지 않게 됩니다.
#endregion

#region Disadvantages
// 단점
// - 데코레이터를 많이 사용하면 코드가 복잡해질 수 있습니다. 여러 데코레이터가 중첩되면 코드의 흐름을 따라가기 어려울 수 있습니다.
// - 객체 초기화가 복잡해질 수 있습니다. 데코레이터 객체를 생성하고 연결하는 과정이 복잡할 수 있습니다.
// - 디버깅이 어려울 수 있습니다. 데코레이터가 여러 개 중첩될 경우, 특정 기능이 어디에서 추가되었는지 파악하기 어려울 수 있습니다.
// - 성능 오버헤드가 발생할 수 있습니다. 데코레이터를 많이 사용할수록 호출 계층이 깊어져 성능에 영향을 미칠 수 있습니다.
#endregion

#region When to Use
// 데코레이터 패턴을 사용할 만한 경우
// - 객체에 다양한 기능을 동적으로 추가해야 하는 경우: 실행 중에 객체의 기능을 변경하거나 확장해야 할 때.
// - 상속을 사용하지 않고 기능을 확장하고 싶은 경우: 기존 클래스의 수정 없이 새로운 기능을 추가해야 할 때.
// - 기능의 조합이 필요한 경우: 여러 기능을 조합하여 다양한 객체를 구성해야 할 때.
// - 코드의 유연성과 재사용성을 높이고 싶은 경우: 여러 객체에 동일한 기능을 재사용하고 싶을 때.
// - 객체의 기능을 단계적으로 추가하고 싶은 경우: 객체에 기능을 단계적으로 추가하여 점진적으로 확장하고 싶을 때.
#endregion

public class ArmDecorator : IArm
{
    #region Constructors
    // IArm 인스턴스와 ArmAttachment 인스턴스를 받아 ArmDecorator 객체를 초기화하는 생성자입니다.
    public ArmDecorator(IArm arm, ArmAttachment attachment)
    {
        _decoratedArm = arm;
        _attachment = attachment;
    }
    #endregion

    #region Fields
    // 장식된 IArm 인스턴스를 저장하는 읽기 전용 필드입니다.
    private readonly IArm _decoratedArm;

    // ArmAttachment 인스턴스를 저장하는 읽기 전용 필드입니다.
    private readonly ArmAttachment _attachment;
    #endregion

    #region Properties
    // 이 속성은 장식된 IArm 인스턴스의 발사 속도와 ArmAttachment 인스턴스의 발사 속도를 합산하여 반환합니다.
    public float Rate { get { return _decoratedArm.Rate + _attachment.Rate; } }

    // 이 속성은 장식된 IArm 인스턴스의 사거리와 ArmAttachment 인스턴스의 사거리를 합산하여 반환합니다.
    public float Range { get { return _decoratedArm.Range + _attachment.Range; } }

    // 이 속성은 장식된 IArm 인스턴스의 힘과 ArmAttachment 인스턴스의 힘을 합산하여 반환합니다.
    public float Strength { get { return _decoratedArm.Strength + _attachment.Strength; } }

    // 이 속성은 장식된 IArm 인스턴스의 쿨다운 시간과 ArmAttachment 인스턴스의 쿨다운 시간을 합산하여 반환합니다.
    public float Cooldown { get { return _decoratedArm.Cooldown + _attachment.Cooldown; } }
    #endregion
}
