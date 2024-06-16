using UnityEngine;

// InputHandler 클래스는 사용자 입력을 처리하고 명령을 실행하는 클래스입니다.
public class InputHandler : MonoBehaviour
{
    #region Fields
    // Invoker 인스턴스입니다.
    private Invoker _invoker;
    // 재생 중인지 여부를 나타내는 플래그입니다.
    private bool _isReplaying;
    // 녹화 중인지 여부를 나타내는 플래그입니다.
    private bool _isRecording;
    // 명령을 실행할 CommandTestController 인스턴스입니다.
    private CommandTestController _commandTestController;
    // 각 키에 대응하는 명령들입니다.
    private Command _buttonA, _buttonD, _buttonW;
    #endregion

    #region Unity Methods
    // 초기화를 실행합니다.
    private void Start()
    {
        _invoker = gameObject.AddComponent<Invoker>();
        _commandTestController = FindObjectOfType<CommandTestController>();

        _buttonD = new TurnRight(_commandTestController);
        _buttonA = new TurnLeft(_commandTestController);
        _buttonW = new ToggleCharging(_commandTestController);
    }

    // // 재생 중이 아니고 녹화 중일 때 키 입력을 처리합니다.
    private void Update()
    {
        
        if (!_isReplaying && _isRecording)
        {
            if (Input.GetKeyUp(KeyCode.A)) _invoker.ExecuteCommand(_buttonA);
            if (Input.GetKeyUp(KeyCode.D)) _invoker.ExecuteCommand(_buttonD);
            if (Input.GetKeyUp(KeyCode.W)) _invoker.ExecuteCommand(_buttonW);
        }
    }

    // Start Recording, Stop Recording 버튼과 녹화 중이 아닐 때만 Start Replay버튼  GUI를 그립니다.
    private void OnGUI()
    {
        
        if (GUILayout.Button("Start Recording"))
        {
            _commandTestController.ResetPosition();
            _isReplaying = false;
            _isRecording = true;
            _invoker.Record();
        }

        if (GUILayout.Button("Stop Recording"))
        {
            _commandTestController.ResetPosition();
            _isRecording = false;
        }

        if (!_isRecording)
        {
            if (GUILayout.Button("Start Replay"))
            {
                _commandTestController.ResetPosition();
                _isRecording = false;
                _isReplaying = true;
                _invoker.Replay();
            }
        }
    }
    #endregion
}
