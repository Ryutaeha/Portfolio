using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager
{
    // 이 프로퍼티는 현재 활성화된 BaseScene 객체를 반환합니다.
    // GameObject.FindObjectOfType<BaseScene>()를 사용하여 씬에서 해당 타입의 객체를 찾습니다.
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    // 이 메서드는 Define.Scene 타입의 인자를 받아 씬을 로드합니다.
    // 현재 씬을 로그로 출력하고, 로드하려는 씬 타입을 로그로 출력한 후 현재 씬을 정리합니다.
    // 다음 씬의 이름을 설정하고 "LoadingScene" 씬을 로드합니다.
    public void LoadScene(Define.Scene type)
    {
        Debug.Log(CurrentScene);
        Debug.Log(type);
        CurrentScene.Clear();
        Main.NextScene = GetSceneName(type);
        SceneManager.LoadScene("LoadingScene");
    }

    // 이 메서드는 문자열 형태의 씬 이름을 받아 씬을 로드합니다.
    // 현재 씬을 로그로 출력하고, 현재 씬을 정리한 후 다음 씬의 이름을 설정합니다.
    // 그리고 "LoadingScene" 씬을 로드합니다.
    public void LoadScene(string sceneName)
    {
        Debug.Log(CurrentScene);

        CurrentScene.Clear();
        Main.NextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // 이 메서드는 Define.Scene 타입의 인자를 받아 해당 씬 타입의 이름을 문자열로 반환합니다.
    // Enum.GetName 메서드를 사용하여 씬 타입의 이름을 얻습니다.
    public string GetSceneName(Define.Scene type)
    {
        return Enum.GetName(typeof(Define.Scene), type);
    }
    public void SetResolution()
    {

        int idx = PlayerPrefs.GetInt("Resolution");
        bool isFull = false;
        if (PlayerPrefs.GetInt("FullScreen") == 0)
            isFull = false;
        else
            isFull = true;

        int w = 1920, h = 1080;
        switch (idx)
        {
            case 0:
                w = 1920;
                h = 1080;
                break;
            case 1:
                w = 1280;
                h = 720;
                break;
            case 2:
                w = 1366;
                h = 768;
                break;
            case 3:
                w = 1536;
                h = 864;
                break;
            case 4:
                w = 2560;
                h = 1440;
                break;
            default:
                break;
        }

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장
        Screen.SetResolution(w, h, isFull);
        

    }
}

