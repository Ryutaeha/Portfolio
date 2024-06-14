using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public override void Clear()
    {
        Main.ResourceManager.ReleaseAllAsset("StartScene");
    }

    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Main.ResourceManager.LoadAllAsync<UnityEngine.Object>("StartScene", (key, count, totalCount) => {
            //Debug.Log($"[GameScene] Load asset {key} ({count}/{totalCount})");
            if (count >= totalCount)
            {
                SceneType = Define.Scene.StartScene;
                UI = Main.UIManager.ShowSceneUI<UIScene_Title>();
            }
        });

        return true;
    }
}
