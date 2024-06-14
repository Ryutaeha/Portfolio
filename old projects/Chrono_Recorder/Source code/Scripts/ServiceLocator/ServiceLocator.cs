using System.Collections.Generic;
using System;

// ServiceLocator 클래스는 여러 서비스들을 중앙에서 관리하고 제공하는 역할을 합니다.
public class ServiceLocator
{
    // 서비스를 저장하기 위한 딕셔너리입니다.
    private readonly Dictionary<Type, object> services = new();

    // 서비스에 저장할 메서드를 등록하기 위한 이벤트입니다.
    public event EventHandler ServiceCreated;

    // 서비스 로케이터를 설정하는 메소드입니다.
    public void SetServiceLocator()
    {
        ServiceCreated?.Invoke(this, EventArgs.Empty);
    }

    // 이미 등록된 서비스인지 확인 후 서비스를 추가하는 메소드입니다.
    public void RegisterService<T>(T service)
    {
        if (services.ContainsKey(typeof(T))) return;
        
        services[typeof(T)] = service;
    }

    // 요청된 서비스가 딕셔너리에 있는지 확인 후 서비스를 가져오는 메소드입니다.
    public T GetService<T>()
    {

        if (!services.TryGetValue(typeof(T), out object service)) return default;
        
        return (T)service;
    }
}
