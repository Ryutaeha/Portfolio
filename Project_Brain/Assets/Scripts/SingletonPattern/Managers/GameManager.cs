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
    private void Start()
    {
        _startTime = DateTime.Now;
        Debug.Log($"Game Start Time : {_startTime}");
    }

    private void OnApplicationQuit()
    {
        _endTime = DateTime.Now;
        TimeSpan timeDifference = _endTime.Subtract( _startTime );
        Debug.Log($"Game End Time : {_endTime}");
        Debug.Log($"Game Lasted Time : {timeDifference}");
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Next Scene"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    #endregion
}
