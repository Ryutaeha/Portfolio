using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboCharger : MonoBehaviour
{
    #region Fields
    public Engine engine;

    private bool _isTurboOn;

    private CoolingSystem _coolingSystem;
    #endregion

    #region Unity Methods
    // 터보가 활성화되어 있는지 여부를 화면에 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.green;

        GUI.Label(new Rect(100, 60, 500, 20), $"Turbo Activated : {_isTurboOn}");
    }
    #endregion

    #region Methods
    // 터보를 토글하는 메서드입니다. 냉각 시스템을 인수로 받습니다.
    public void ToggleTurbo(CoolingSystem coolingSystem)
    {
        _coolingSystem = coolingSystem;

        if (!_isTurboOn) StartCoroutine(TurboCharge());
    }
    #endregion

    #region Coroutines
    // 터보를 일정 시간 동안 활성화하는 코루틴입니다.
    private IEnumerator TurboCharge()
    {
        _isTurboOn = true;

        _coolingSystem.PauseCooling();

        yield return new WaitForSeconds(engine.turboDuration);

        _isTurboOn = false;

        _coolingSystem.PauseCooling();
    }
    #endregion
}
