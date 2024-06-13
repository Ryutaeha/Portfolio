using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class CountDownTimer : MonoBehaviour
{
    #region Fields
    private float _currentTime;
    private float duration = 10f;

    private IEnumerator timer;
    #endregion

    #region Unity Methods
    // 타이머의 현재 시간을 초기화하고, COUNTDOWN 및 STOP 이벤트를 구독합니다.
    private void OnEnable()
    {
        _currentTime = duration;
        GameEventBus.Subscribe(GameEventType.COUNTDOWN, StartTimer);
        GameEventBus.Subscribe(GameEventType.STOP, TimeReset);
    }

    // COUNTDOWN 및 STOP 이벤트 구독을 해제합니다.
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.COUNTDOWN, StartTimer);
        GameEventBus.Unsubscribe(GameEventType.STOP, TimeReset);
    }

    // OnGUI 메서드는 GUI 이벤트가 발생할 때 호출됩니다.
    // 현재 타이머의 시간을 화면에 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.blue;
        GUI.Label(new Rect(125, 0, 120, 20), $"COUNTDOWN: {_currentTime}");
    }
    #endregion

    #region Methods
    // 타이머 코루틴을 시작합니다.
    private void StartTimer()
    {
        timer = CountDown();
        StartCoroutine(timer);
    }

    // CountDown 메서드는 타이머를 실행하는 코루틴입니다.
    // 타이머가 진행되는 동안 1초마다 시간을 감소시키고, 타이머가 끝나면 시간을 초기화합니다.
    private IEnumerator CountDown()
    {
        GameEventBus.Publish(GameEventType.START);
        
        while(_currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            _currentTime--;
        }

        _currentTime = duration;
    }

    // TimeReset 메서드는 STOP 이벤트가 발생했을 때 호출됩니다.
    // 타이머를 중지하고 시간을 초기화합니다.
    private void TimeReset()
    {
        if(timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        _currentTime = duration;
    }
    #endregion
}
