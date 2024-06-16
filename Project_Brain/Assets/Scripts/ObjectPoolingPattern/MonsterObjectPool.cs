
#region Advantages
// 장점
// - 성능 향상: 오브젝트를 반복적으로 생성하고 파괴하는 비용을 줄여 성능을 향상시킵니다.
// - 메모리 관리: 메모리 할당과 해제를 최소화하여 메모리 관리 효율성을 높입니다.
// - 일관성: 재사용 가능한 오브젝트를 사용하여 게임이나 애플리케이션의 일관성을 유지합니다.
// - 초기화 시간 감소: 오브젝트의 초기화 시간을 줄여 게임 시작 시 로딩 시간을 줄입니다.
// - 자원 관리: 제한된 자원을 효과적으로 관리할 수 있습니다.
#endregion

#region Disadvantages
// 단점
// - 초기 설정 비용: 오브젝트 풀을 초기화할 때 많은 메모리와 시간이 소요될 수 있습니다.
// - 복잡성 증가: 오브젝트 풀링 시스템을 구현하고 관리하는 데 추가적인 복잡성이 발생할 수 있습니다.
// - 메모리 낭비: 사용하지 않는 오브젝트가 메모리에 남아 있을 수 있어 메모리가 낭비될 가능성이 있습니다.
// - 관리 필요: 오브젝트 풀의 크기와 상태를 모니터링하고 관리해야 합니다.
#endregion

#region When to Use
// 오브젝트 풀링을 사용할만한 경우
// - 빈번한 오브젝트 생성과 파괴: 적, 총알, 특수 효과 등 게임 내에서 빈번하게 생성되고 파괴되는 오브젝트를 관리할 때.
// - 성능 최적화: 성능이 중요한 애플리케이션에서 오브젝트 생성과 파괴로 인한 성능 저하를 방지할 때.
// - 메모리 최적화: 메모리 할당과 해제를 최소화하여 메모리 사용을 최적화할 때.
// - 자원 재사용: 데이터베이스 연결, 네트워크 소켓 등 비용이 많이 드는 자원을 재사용할 때.
// - 일관된 상태 유지: 오브젝트가 일관된 상태를 유지해야 할 때.
#endregion


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
