using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    #region Fields
    public float burnRate = 1f;

    public float fuelAmount = 100f;

    public float tempRate = 5f;

    public float minTemp = 50f;

    public float maxTemp = 65f;

    public float currentTemp;

    public float turboDuration = 2f;

    private bool _isEngineOn;

    private FuelPump _fuelPump;

    private TurboCharger _turboCharger;

    private CoolingSystem _coolingSystem;
    #endregion

    #region Unity Methods
    // 게임 오브젝트에 필요한 컴포넌트를 추가합니다.
    private void Awake()
    {
        _fuelPump = gameObject.AddComponent<FuelPump>();
        _turboCharger = gameObject.AddComponent<TurboCharger>();
        _coolingSystem = gameObject.AddComponent<CoolingSystem>();
    }

    // 각 컴포넌트에 엔진을 설정합니다.
    private void Start()
    {
        _fuelPump.engine = this;
        _turboCharger.engine = this;
        _coolingSystem.engine = this;
    }

    // 엔진이 켜져 있는지 여부를 화면에 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.green;

        
        GUI.Label(new Rect(100, 0, 500, 20), $"Engine Running : {_isEngineOn}");
    }
    #endregion

    #region Methods
    // 엔진을 켜는 메서드입니다.
    public void TurnOn()
    {
        _isEngineOn = true;
        StartCoroutine(_fuelPump.BurnFuel());
        StartCoroutine(_coolingSystem.CoolEngine());
    }

    // 엔진을 끄는 메서드입니다.
    public void TurnOff()
    {
        _isEngineOn = false;
        _coolingSystem.ResetTemperature();
        StopCoroutine(_fuelPump.BurnFuel());
        StopCoroutine(_coolingSystem.CoolEngine());
    }

    // 터보를 토글하는 메서드입니다.
    public void ToggleTurbo()
    {
        if (_isEngineOn) _turboCharger.ToggleTurbo(_coolingSystem);
    }
    #endregion
}

