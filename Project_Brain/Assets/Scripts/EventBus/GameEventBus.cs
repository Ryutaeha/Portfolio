using System.Collections.Generic;
using UnityEngine.Events;
using Define;

#region Advantages
// 장점
// - GameEventBus를 통해 모든 Event를 중앙에서 관리할 수 있습니다. 이를 통해 코드의 가독성과 유지보수성이 향상됩니다.
// - 다양한 Event Type에 대해 구독하고 발행할 수 있어 확장성이 높습니다.
// - Event 처리 로직을 중앙화함으로써 각 클래스에서 Event를 직접 관리하는 코드 중복을 줄일 수 있습니다.
// - Event 처리 로직이 분리되어 있어 각 모듈 간의 결합도가 낮아집니다.
// - 특정 Event에 대해 여러 listener를 관리하고 순차적으로 실행할 수 있어, 실행 흐름을 제어하기 용이합니다.
#endregion

#region Disadvantages
// 단점
// - 많은 Event Type과 listener가 등록될 경우 메모리 사용량이 증가할 수 있습니다.
// - Event가 중앙에서 관리되기 때문에, Event의 흐름을 추적하거나 디버깅하는 것이 어려울 수 있습니다.
// - Event를 구독한 객체가 적절히 해제되지 않으면 메모리 누수가 발생할 수 있습니다.
// - 모든 클래스가 GameEventBus에 의존하게 되면, 의존성 관리가 복잡해질 수 있습니다.
// - Event가 빈번하게 발생할 경우, Event 처리 로직이 성능에 영향을 미칠 수 있습니다.
#endregion

#region When to Use
// Event버스를 사용할만한 경우
// - 여러 객체가 동일한 Event를 구독해야 하는 경우: 예를 들어, 플레이어의 체력 변화 Event를 여러 UI 요소가 구독해야 할 때.
// - 모듈 간 결합도를 낮추고 싶을 때: 서로 다른 시스템 간의 의존성을 줄이고 모듈화를 강화하고자 할 때.
// - Event 기반 아키텍처를 구현하고 싶을 때: 게임 내에서 발생하는 여러 Event를 중심으로 실행 흐름을 제어하고자 할 때.
// - 비동기 Event 처리가 필요할 때: Event가 발생하면 즉시 처리되지 않고, 비동기적으로 처리되어야 할 때.
// - 코드의 가독성과 유지보수성을 높이고 싶을 때: Event 로직을 중앙에서 관리하여 코드가 더욱 명확하고 유지보수하기 쉽게 만들고자 할 때.
#endregion

public class GameEventBus
{
    #region Fields
    // Event Type과 해당 Event를 저장하는 Dictionary입니다.
    // IDictionary를 사용하여 여러 가지 Event Type에 대응할 수 있게 해줍니다.
    private static readonly IDictionary<GameEventType, UnityEvent> Events = new Dictionary<GameEventType, UnityEvent>();
    #endregion

    #region Methods
    // 특정 Event Type이 존재하는지 확인하고, 존재한다면 listener를 추가합니다.
    // 특정 Event Type이 존재하지 않으면 새로운 Event를 생성하고 listener를 추가한 후, 새로 생성된 Event를 Dictionary에 추가합니다.
    public static void Subscribe(GameEventType eventType, UnityAction listener)
    {
        if (Events.TryGetValue(eventType, out UnityEvent thisEvent)) thisEvent.AddListener(listener);
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    // 특정 Event Type이 존재하는지 확인하고, 존재한다면 listener를 삭제합니다.
    // 매개변수로 받은 listener가 Event에 등록되어 있지 않은 경우 아무런 동작도 하지 않습니다.
    public static void Unsubscribe(GameEventType eventType, UnityAction listener)
    {
        if (Events.TryGetValue(eventType, out UnityEvent thisEvent)) thisEvent.RemoveListener(listener);
    }

    // 특정 Event Type이 존재하는지 확인하고, 존재한다면 해당 Event Type의 UnityEvent를 발생시킵니다.
    public static void Publish(GameEventType eventType)
    {
        if (Events.TryGetValue(eventType, out UnityEvent thisEvent)) thisEvent.Invoke();
    }
    #endregion
}
