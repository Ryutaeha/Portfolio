using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CHHScene : BaseScene
{
    private EnemySpawn enemySpawn;

    public override void Clear()
    {
    }

    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        if (enemySpawn == null) enemySpawn = this.gameObject.GetOrAddComponent<EnemySpawn>();


        //Main.ResourceManager.Instantiate("Map");
        //Main.ResourceManager.Instantiate("NavMesh");
        StartWave();

        return true;
    }

    private void StartWave()
    {
        enemySpawn.StartSpawn();
    }
}
