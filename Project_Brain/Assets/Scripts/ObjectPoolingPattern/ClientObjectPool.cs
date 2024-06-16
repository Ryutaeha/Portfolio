using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientObjectPool : MonoBehaviour
{
    #region Fields
    private MonsterObjectPool _pool;
    #endregion

    #region Unity Methods
    private void Start()
    {
        _pool = gameObject.AddComponent<MonsterObjectPool>();
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Spawn Monsters")) _pool.Spawn();
    }
    #endregion
}
