using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using System;
public class HUDController : MonoBehaviour
{
    #region Fields
    private bool _isDisplayOn;
    #endregion

    #region Unity Methods
    // START 이벤트를 구독합니다.
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.START, DisplayHUD);
    }

    // START 이벤트 구독을 해제합니다.
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.START, DisplayHUD);
    }

    // GUI 이벤트가 발생할 때 호출됩니다.
    // HUD가 표시되어 있을 때 "Stop Game" 버튼을 그립니다.
    // 버튼이 클릭되면 HUD 표시를 끄고, STOP 이벤트를 발행합니다.
    private void OnGUI()
    {
        if (_isDisplayOn)
        {
            if(GUILayout.Button("Stop Game"))
            {
                _isDisplayOn = false;
                GameEventBus.Publish(GameEventType.STOP);
            }
        }
    }
    #endregion

    #region Methods
    // HUD 표시를 켭니다.
    private void DisplayHUD()
    {
        _isDisplayOn = true;
    }
    #endregion
}
