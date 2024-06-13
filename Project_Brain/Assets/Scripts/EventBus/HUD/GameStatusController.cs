using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
public class GameStatusController : MonoBehaviour
{
    #region Fields
    private string _status = "StopGame";
    #endregion

    #region Unity Methods
    // START 및 STOP 이벤트를 구독합니다.
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.START, StartGame);
        GameEventBus.Subscribe(GameEventType.STOP, StopGame);
    }

    // START 및 STOP 이벤트 구독을 해제합니다.
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.START, StartGame);
        GameEventBus.Unsubscribe(GameEventType.STOP, StopGame);
    }

    // GUI 이벤트가 발생할 때 호출됩니다.
    // 현재 게임 상태를 화면에 표시합니다.
    private void OnGUI()
    {
        GUI.color = Color.green;
        GUI.Label(new Rect(10, 60, 200, 20), $"GAME STATUS: {_status}");
    }
    #endregion

    #region Methods
    // 게임 상태를 "StartGame"으로 변경합니다.
    private void StartGame()
    {
        _status = "StartGame";
    }

    // 게임 상태를 "StopGame"으로 변경합니다.
    private void StopGame()
    {
        _status = "StopGame";
    }
    #endregion

}
