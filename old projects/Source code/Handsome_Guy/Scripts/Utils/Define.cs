using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum MouseEvent
    {
        Click,
        Press,
    }
    public enum Scene
    {
        Unknown,
        StartScene,
        LobbyScene,
        Loading,
        Game,
        testScene
    }
}
