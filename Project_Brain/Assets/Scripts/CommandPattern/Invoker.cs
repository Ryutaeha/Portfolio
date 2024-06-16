using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Invoker 클래스는 명령을 기록하고 재생하는 역할을 합니다.
public class Invoker : MonoBehaviour
{
    #region Fields
    // 녹화 중인지 여부를 나타내는 변수입니다.
    private bool _isRecording;
    // 재생 중인지 여부를 나타내는 변수입니다.
    private bool _isReplaying;
    // 재생 시간입니다.
    private float _replayTime;
    // 녹화 시간입니다.
    private float _recordingTime;

    // 녹화된 명령들을 저장하는 정렬된 리스트입니다.
    private SortedList<float, Command> _recordedCommands = new SortedList<float, Command>();
    #endregion

    #region Unity Methods
    // 녹화 중이라면 녹화 시간을 증가 시킵니다.
    // 재생 중이라면 재생 시간을 증가시키고 녹화된 명령이 있는지 확인합니다.
    // 재생 시간이 녹화된 명령의 시간과 거의 같은지 확인하고 거의 같다면 명령을 실행하고 리스트에서 제거합니다.
    private void FixedUpdate()
    {
        if (_isRecording) _recordingTime += Time.fixedDeltaTime;

        if (_isReplaying)
        {
            _replayTime += Time.deltaTime;

            if (_recordedCommands.Any())
            {
                if (Mathf.Approximately(_replayTime, _recordedCommands.Keys[0]))
                {
                    Debug.Log($"Replay Time : {_replayTime}");
                    Debug.Log($"Replay Command : {_recordedCommands.Values[0]}");

                    _recordedCommands.Values[0].Execute();
                    _recordedCommands.RemoveAt(0);
                }
            }
            else _isReplaying = false;
        }
    }
    #endregion

    #region Methods
    // 명령을 실행하고, 녹화 중이면 명령을 녹화합니다.
    public void ExecuteCommand(Command command)
    {
        command.Execute();

        if (_isRecording) _recordedCommands.Add(_recordingTime, command);

        Debug.Log($"Recorded Time : {_recordingTime}");
        Debug.Log($"Recorded Command : {command}");
    }

    // 녹화를 시작합니다.
    public void Record()
    {
        _recordingTime = 0;
        _isRecording = true;
    }

    // 녹화된 명령을 역순으로 재생합니다.
    public void Replay()
    {
        _replayTime = 0;
        _isReplaying = true;

        if (_recordedCommands.Count <= 0) Debug.LogError("No commands to replay");

        _recordedCommands.Reverse();
    }
    #endregion
}
