using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public override void Clear()
    {
        Main.ResourceManager.ReleaseAllAsset("LobbyScene");
    }
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Main.ResourceManager.LoadAllAsync<UnityEngine.Object>("LobbyScene", (key, count, totalCount) => {
            if (count >= totalCount)
            {
                SceneType = Define.Scene.LobbyScene;
                UI = Main.UIManager.ShowSceneUI<UIScene_Lobby>();
                Launcher launcher = GetComponent<Launcher>();
                launcher.Connect();
            }
        });
        
        print($"닉네임 : {Main.GameManager.NickName}, 캐릭터 : {Main.GameManager.CharacterType}, 스킬 : {Main.GameManager.SkillType}");
        
        return true;
    }
}
