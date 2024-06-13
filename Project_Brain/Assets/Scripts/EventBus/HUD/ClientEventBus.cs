using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class ClientEventBus : MonoBehaviour
{
    #region Fields
    // _isButtonEnabled는 버튼이 활성화되어 있는지 여부를 나타내는 변수입니다.
    private bool _isButtonEnabled;
    #endregion

    #region Unity Methods
    // HUDController, CountDownTimer, GameStatusController 컴포넌트를 게임 오브젝트에 추가하고, 버튼을 활성화 상태로 설정합니다.
    private void Start()
    {
        gameObject.AddComponent<HUDController>();
        gameObject.AddComponent<CountDownTimer>();
        gameObject.AddComponent<GameStatusController>();

        _isButtonEnabled = true;
    }

    // GameEventType.STOP 이벤트가 발생할 때 Restart 메서드를 호출하도록 구독합니다.
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.STOP, Restart);
    }

    // GameEventType.STOP 이벤트에 대한 구독을 해제합니다.
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.STOP, Restart);
    }

    // 버튼이 활성화되어 있을 때 "Start Countdown" 버튼을 생성하고,
    // 버튼이 클릭되면 GameEventType.COUNTDOWN 이벤트를 발행하며 버튼을 비활성화 상태로 설정합니다.
    private void OnGUI()
    {
        if (_isButtonEnabled)
        {
            if (GUILayout.Button("Start Countdown"))
            {
                _isButtonEnabled = false;

                GameEventBus.Publish(GameEventType.COUNTDOWN);
            }
        }
    }
    #endregion

    #region Methods
    // 이 메서드는 버튼을 다시 활성화 상태로 설정합니다.
    private void Restart()
    {
        _isButtonEnabled = true;
    }
    #endregion

}
