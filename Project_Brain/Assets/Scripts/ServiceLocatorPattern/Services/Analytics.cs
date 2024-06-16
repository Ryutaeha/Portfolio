using ServiceInterface;
using UnityEngine;

public class Analytics : IAnalyticsService
{
    #region Methods
    // IAnalyticsService 인터페이스의 SendEvent 메서드를 구현한 것입니다.
    // 주어진 이벤트 이름을 Unity의 디버그 로그에 출력합니다.
    // 매개변수 eventName은 전송할 이벤트의 이름입니다.
    public void SendEvent(string eventName)
    {
        Debug.Log($"Sent: {eventName}");
    }
    #endregion
}
