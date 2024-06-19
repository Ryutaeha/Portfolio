using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPump : MonoBehaviour
{
    #region Fields
    public Engine engine;

    public IEnumerator burnFuel;
    #endregion

    #region Unity Methods
    // 연료 소모 코루틴을 초기화합니다.
    private void Start()
    {
        burnFuel = BurnFuel();
    }

    // 엔진의 현재 연료량을 화면에 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.green;

        GUI.Label(new Rect(100, 40, 500, 20), $"Fuel : {engine.fuelAmount}");
    }
    #endregion

    #region Coroutine
    // 엔진의 연료를 주기적으로 소모시키는 코루틴입니다.
    public IEnumerator BurnFuel()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            engine.fuelAmount -= engine.burnRate;

            if (engine.fuelAmount <= 0f)
            {
                engine.TurnOff();
                yield break;
            }
        }
    }
    #endregion
}
