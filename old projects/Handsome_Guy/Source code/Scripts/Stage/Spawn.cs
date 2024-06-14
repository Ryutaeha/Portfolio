using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn :BaseScene
{
    private EnemySpawn enemySpawn;

    private CinemachineVirtualCamera _virtualCamera;
    private PhotonView _pv;
    private Player _player;
    public override void Clear()
    {
        Main.ResourceManager.ReleaseAllAsset("GameScene");
    }
    public Transform[] playerpoints;
    public Transform[] gimmickpoints;

    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;
        SceneType = Define.Scene.Game;

        if (enemySpawn == null) enemySpawn = this.gameObject.GetOrAddComponent<EnemySpawn>();

        Main.ResourceManager.LoadAllAsync<UnityEngine.Object>("GameScene", (key, count, totalCount) =>
        {
            if (count >= totalCount)
            {
                InitialAndSpawnPlayer();
                SpawnMachine(6);
                Main.ResourceManager.Instantiate($"NavMesh.prefab");
                StartWave();
            }
        });

        return true;
    }

    private void Update()
    {
        if (_virtualCamera == null || _player == null) return;


        if (_pv.IsMine)
        {
            _virtualCamera.Follow = _player.transform;
        }
    }

    private void SpawnMachine(int set)
    {

        gimmickpoints = GameObject.Find("GimmickSpwnPoint").GetComponentsInChildren<Transform>();

        int[] numbers = Enumerable.Range(1, gimmickpoints.Length - 1).ToArray();
        int[] result = numbers.OrderBy(x => Random.value).Take(set).ToArray();

        for (int i = 0; i < result.Length; i++)
        {
            Main.ResourceManager.Instantiate("Machine.prefab", gimmickpoints[result[i]]);
        }
        Main.GameManager.UISet();
    }

    private void InitialAndSpawnPlayer()
    {
        playerpoints = GameObject.Find("PlayerSpawnGroup").GetComponentsInChildren<Transform>();

        int idx = Random.Range(0, playerpoints.Length);
        _player  = Main.ObjectManager.Spawn<Player>("Player", playerpoints[idx].position);
        Main.DataManager.SkillDict.TryGetValue(Main.GameManager.SkillType, out Data.Skill skill);
        _player.gameObject.AddComponent(skill.type);
        _pv = _player.GetComponent<PhotonView>();
        _virtualCamera = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    private void StartWave()
    {
        if(PhotonNetwork.IsMasterClient)
        enemySpawn.StartSpawn();
    }
}
