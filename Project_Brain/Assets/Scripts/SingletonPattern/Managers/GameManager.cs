using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Field
    private DateTime _startTime;
    private DateTime _endTime;
    #endregion

    #region Unity Methods
    // 현재 시간을 _startTime 필드에 저장하고, 시작 시간을 디버그 로그로 출력합니다.
    private void Start()
    {
        _startTime = DateTime.Now;

        Debug.Log($"Game Start Time : {_startTime}");
    }

    // 애플리케이션이 종료될 때 호출되는 메서드입니다.
    // 현재 시간을 _endTime 필드에 저장하고, 게임이 시작된 시간과 종료된 시간의 차이를 계산하여 디버그 로그로 출력합니다.
    private void OnApplicationQuit()
    {
        _endTime = DateTime.Now;

        TimeSpan timeDifference = _endTime.Subtract(_startTime);

        Debug.Log($"Game End Time : {_endTime}");
        Debug.Log($"Game Lasted Time : {timeDifference}");
    }

    // GUI를 그릴 때 호출되는 메서드입니다.
    // "Next Scene" 버튼을 생성하고, 버튼이 클릭될 경우 현재 씬의 다음 씬을 로드합니다.
    private void OnGUI()
    {
        if (GUILayout.Button("Next Scene")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
