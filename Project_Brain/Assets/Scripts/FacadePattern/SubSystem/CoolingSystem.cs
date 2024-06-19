using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolingSystem : MonoBehaviour
{
    #region Fields
    public Engine engine;

    public IEnumerator coolEngine;

    private bool _isPaused;
    #endregion

    #region Unity Methods
    // 냉각 코루틴을 초기화합니다.
    private void Start()
    {
        coolEngine = CoolEngine();
    }
    // 엔진의 현재 온도를 화면에 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.green;

        GUI.Label(new Rect(100, 20, 500, 20), $"Temp : {engine.currentTemp}");
    }
    #endregion

    #region Methods
    // 냉각 시스템을 일시 중지하거나 다시 시작하는 메서드입니다.
    public void PauseCooling()
    {
        _isPaused = !_isPaused;
    }

    // 엔진의 온도를 초기화하는 메서드입니다.
    public void ResetTemperature()
    {
        engine.currentTemp = 0.0f;
    }
    #endregion

    #region Coroutine
    // 엔진을 주기적으로 냉각시키는 코루틴입니다.
    public IEnumerator CoolEngine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if (!_isPaused)
            {
                if (engine.currentTemp > engine.minTemp) engine.currentTemp -= engine.tempRate;

                if (engine.currentTemp < engine.minTemp) engine.currentTemp += engine.tempRate;
            }
            else engine.currentTemp += engine.tempRate;

            if (engine.currentTemp > engine.maxTemp) engine.TurnOff();
        }
    }
    #endregion
}
