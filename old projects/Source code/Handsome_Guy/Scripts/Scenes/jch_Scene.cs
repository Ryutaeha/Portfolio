using Data;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    public Transform[] points;

    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;
        SceneType = Define.Scene.Game;

        Main.ResourceManager.LoadAllAsync<UnityEngine.Object>("GameScene", (key, count, totalCount) =>
        {
            if (count >= totalCount)
            {
                
            }
        });
        InitialAfterLoad();

        return true;
    }

    public void InitialAfterLoad()
    {
        points = GameObject.Find("PlayerSpawnGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, points.Length);
        Main.ObjectManager.Spawn<Player>("Player", points[idx].position);
        Main.DataManager.SkillDict.TryGetValue(Main.GameManager.SkillType, out Data.Skill skill);
        GameObject.Find("Player(Clone)").AddComponent(skill.type);
    }

    public override void Clear()
    {
        Main.ResourceManager.ReleaseAllAsset("GameScene");
    }
}
