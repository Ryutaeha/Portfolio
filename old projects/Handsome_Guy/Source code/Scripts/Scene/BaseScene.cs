using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    public UI_Scene UI { get; protected set; }

    private bool _Initialized = false;
    void Start()
    {
        if (Main.ResourceManager.Loaded)
        {
            
            Main.DataManager.Initialize();
            //Main.Game.Initialize();
            Initialize();
        }
        else
        {
            Main.ResourceManager.LoadAllAsync<UnityEngine.Object>("PreLoad", (key, count, totalCount) => {
                //Debug.Log($"[GameScene] Load asset {key} ({count}/{totalCount})");
                if (count >= totalCount)
                {
                    Main.ResourceManager.Loaded = true;
                    Main.DataManager.Initialize();
                    //Main.Game.Initialize();
                    Initialize();
                }
            });
        }

    }

    public abstract void Clear();

    protected virtual bool Initialize()
    {
        if (_Initialized) return false;

        //Main.SceneManagerEx.CurrentScene = this;

        Object obj = GameObject.FindObjectOfType<EventSystem>();
        if (obj == null) Main.ResourceManager.Instantiate("EventSystem.prefab").name = "@EventSystem";

        _Initialized = true;
        return true;
    }
}
