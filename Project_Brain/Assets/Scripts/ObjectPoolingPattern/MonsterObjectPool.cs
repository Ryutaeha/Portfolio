using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterObjectPool : MonoBehaviour
{
    #region Fields
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;

    private IObjectPool<PoolingMonster> _pool;
    #endregion

    #region Properties
    public IObjectPool<PoolingMonster> Pool
    {
        get
        {
            if (_pool == null) _pool = new ObjectPool<PoolingMonster>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
            return _pool;
        }
    }
    #endregion

    #region Methods
    private PoolingMonster CreatedPooledItem()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        PoolingMonster monster = go.AddComponent<PoolingMonster>();

        go.name = "monster";
        monster.Pool = Pool;

        return monster;
    }

    private void OnReturnedToPool(PoolingMonster monster)
    {
        monster.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(PoolingMonster monster)
    {
        monster.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(PoolingMonster monster)
    {
        Destroy(monster.gameObject);
    }

    public void Spawn()
    {
        var amount = Random.Range(1, 10);

        for(int i = 0; i < amount; i++)
        {
            var monster = Pool.Get();
            monster.transform.position = Random.insideUnitSphere * 10;
        }
    }

    #endregion
}
