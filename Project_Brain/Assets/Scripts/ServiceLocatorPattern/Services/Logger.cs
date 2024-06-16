using ServiceInterface;
using UnityEngine;

public class Logger : ILoggerService
{
    #region Methods
    // ILoggerService 인터페이스의 Log 메서드를 구현한 것입니다.
    // 주어진 메시지를 Unity의 디버그 로그에 출력합니다.
    // 매개변수 message는 기록할 메시지입니다.
    public void Log(string message)
    {
        Debug.Log($"Logged: {message}");
    }
    #endregion
}
