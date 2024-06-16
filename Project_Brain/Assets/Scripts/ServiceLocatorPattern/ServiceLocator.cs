
#region Advantages
// 장점
// - 유연성: 클래스들이 서로의 구체적인 구현에 의존하지 않고 인터페이스에 의존하게 되어 코드의 유연성과 재사용성을 높일 수 있습니다.
// - 중앙 집중 관리: 모든 서비스의 인스턴스를 중앙에서 관리하므로, 인스턴스를 중앙에서 일괄적으로 관리할 수 있습니다.
// - 간단한 의존성 해결: 의존성 주입 없이도 간단하게 의존성을 해결할 수 있으며, 특정 서비스의 구현체를 변경할 때 코드 수정이 최소화됩니다.
// - 테스트 용이성: 모의 객체(mock objects)를 쉽게 주입할 수 있어 단위 테스트가 쉬워집니다.
#endregion

#region Disadvantages
// 단점
// - 숨겨진 의존성: 클래스가 어떤 서비스를 사용하는지 명시적으로 드러나지 않아 코드 읽기가 어려워지고 의존성을 추적하기 어렵습니다.
// - 전역 상태: 전역 상태를 사용하게 되어, 상태 관리가 어려워질 수 있으며, 전역 상태를 사용하는 코드는 멀티스레딩 환경에서 문제가 발생할 수 있습니다.
// - 복잡성 증가: 모든 서비스의 등록과 해제를 중앙에서 관리해야 하므로, 코드의 복잡성이 증가할 수 있습니다.
// - 코드 설계의 어려움: 적절한 시점에 서비스를 등록하고 해제해야 하므로, 서비스의 생명주기를 관리하는 것이 어려울 수 있습니다.
#endregion

#region When to Use
// 서비스 로케이터를 사용할만한 경우
// - 기존 코드베이스: 기존 코드베이스에 의존성 주입을 도입하기 어려운 경우, `ServiceLocator`를 사용하여 의존성을 해결할 수 있습니다.
// - 간단한 애플리케이션: 의존성 주입 컨테이너를 사용할 정도로 복잡하지 않은 간단한 애플리케이션에서 의존성을 해결할 때.
// - 테스트 용이성: 테스트 코드에서 모의 객체를 쉽게 주입해야 할 때.
// - 빠른 프로토타이핑: 빠르게 프로토타입을 작성해야 하는 경우, `ServiceLocator`를 사용하여 의존성을 간단히 해결할 수 있습니다.
// - 특정 서비스의 중앙 관리: 특정 서비스의 인스턴스를 중앙에서 관리하고, 다양한 클래스에서 쉽게 접근해야 할 때.
#endregion

using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    #region Fields
    // 서비스 타입과 인스턴스를 저장하는 딕셔너리입니다.
    // 키는 서비스 타입(Type)이고, 값은 서비스 인스턴스(object)입니다.
    private static readonly IDictionary<Type, object> Services = new Dictionary<Type, object>();
    #endregion

    #region Methods
    // 특정 타입의 서비스를 등록합니다. 단 이미 동일한 타입의 서비스가 등록되어 있는 경우 예외가 발생합니다.
    public static void RegisterService<T>(T service)
    {
        if (!Services.ContainsKey(typeof(T))) Services[typeof(T)] = service;
        else throw new ApplicationException("Service already registered");
    }


    // 특정 타입의 서비스를 가져옵니다. 단 요청한 타입의 서비스가 등록되어 있지 않은 경우 예외가 발생합니다.
    public static T GetService<T>()
    {
        try
        { return (T)Services[typeof(T)];}
        catch { throw new ApplicationException("Requested service not found"); }
    }
    #endregion
}
