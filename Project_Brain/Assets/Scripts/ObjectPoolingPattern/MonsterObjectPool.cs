
using UnityEngine;
using UnityEngine.Pool;

public class MonsterObjectPool : MonoBehaviour
{
    #region Fields
    // 풀의 최대 크기입니다.
    public int maxPoolSize = 10;
    // 풀의 기본 용량입니다.
    public int stackDefaultCapacity = 10;

    // 객체 풀을 관리하는 인터페이스입니다.
    private IObjectPool<PoolingMonster> _pool;
    #endregion

    #region Properties
    public IObjectPool<PoolingMonster> Pool
    {
        get
        {
            if (_pool == null) _pool = new ObjectPool<PoolingMonster>(
                CreatedPooledItem,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                true,
                stackDefaultCapacity,
                maxPoolSize);
            return _pool;
        }
    }
    #endregion

    #region Methods
    // 풀에 새로운 몬스터 객체를 생성하여 반환합니다.
    private PoolingMonster CreatedPooledItem()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        PoolingMonster monster = go.AddComponent<PoolingMonster>();

        go.name = "monster";
        monster.Pool = Pool;

        return monster;
    }

    // 풀로 반환된 몬스터 객체를 비활성화합니다.
    private void OnReturnedToPool(PoolingMonster monster)
    {
        monster.gameObject.SetActive(false);
    }

    // 풀에서 가져온 몬스터 객체를 활성화합니다.
    private void OnTakeFromPool(PoolingMonster monster)
    {
        monster.gameObject.SetActive(true);
    }

    // 풀에서 제거된 몬스터 객체를 파괴합니다.
    private void OnDestroyPoolObject(PoolingMonster monster)
    {
        Destroy(monster.gameObject);
    }

    // 랜덤한 위치에 몬스터 객체를 생성하여 배치합니다.
    public void Spawn()
    {
        var amount = Random.Range(1, 10);

        for (int i = 0; i < amount; i++)
        {
            var monster = Pool.Get();
            monster.transform.position = Random.insideUnitSphere * 10;
        }
    }

    #endregion
}
