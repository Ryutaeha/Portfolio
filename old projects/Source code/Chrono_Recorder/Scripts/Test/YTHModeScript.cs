using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YTHModeScript : MonoBehaviour
{
    public Define.Scene scene = Define.Scene.Default;
    public void TestYTHSceneLord()
    {
        Main.Scenes.LoadScene(scene);
    }
}
