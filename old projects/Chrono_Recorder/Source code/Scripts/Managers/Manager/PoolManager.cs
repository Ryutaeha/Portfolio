using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    // 풀링할 오브젝트의 프리팹을 저장하는 변수입니다.
    private GameObject prefab;

    // Object Pool을 관리하는 변수입니다.
    private IObjectPool<GameObject> pool;

    // 풀링된 오브젝트들이 부모로 가질 Transform입니다.
    private Transform root;

    // root를 반환하는 프로퍼티입니다. 필요할 때 생성합니다.
    private Transform Root
    {
        get
        {
            // root가 null이면 새로운 GameObject를 생성하여 root로 설정합니다.
            if (root == null)
            {
                GameObject obj = new() { name = $"[Pool_Root] {prefab.name}" };
                root = obj.transform;
            }
            return root;
        }
    }

    // Pool 클래스의 생성자입니다.
    // 프리팹을 인자로 받아 ObjectPool을 초기화할 때 콜백 메서드들을 전달합니다.
    public Pool(GameObject prefab)
    {
        this.prefab = prefab;
        this.pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    // 오브젝트를 풀에서 가져오는 메서드입니다.
    // 풀에서 활성화된 오브젝트를 반환합니다.
    public GameObject Pop()
    {
        return pool.Get();
    }

    // 오브젝트를 풀에 반환하는 메서드입니다.
    // 반환된 오브젝트는 비활성화됩니다.
    public void Push(GameObject obj)
    {
        pool.Release(obj);
    }

    // 풀을 초기화하는 메서드입니다.
    // 모든 풀링된 오브젝트를 제거하고 root를 null로 설정합니다.
    public void Clear()
    {
        root = null;
        pool.Clear();
    }

    #region Callbacks

    // 오브젝트를 생성하는 콜백 메서드입니다.
    // 프리팹을 인스턴스화하고, 이를 root의 자식으로 설정합니다.
    private GameObject OnCreate()
    {
        GameObject obj = GameObject.Instantiate(prefab);
        obj.transform.SetParent(Root);
        obj.name = prefab.name;
        return obj;
    }

    // 오브젝트를 풀에서 가져올 때 호출되는 콜백 메서드입니다.
    // 오브젝트를 활성화합니다.
    private void OnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    // 오브젝트를 풀에 반환할 때 호출되는 콜백 메서드입니다.
    // 오브젝트를 비활성화합니다.
    private void OnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    // 오브젝트를 파괴할 때 호출되는 콜백 메서드입니다.
    // 오브젝트를 실제로 파괴합니다.
    private void OnDestroy(GameObject obj)
    {
        GameObject.Destroy(obj);
    }

    #endregion
}

public class PoolManager
{
    // 여러 풀을 관리하는 딕셔너리입니다.
    private Dictionary<string, Pool> pools = new();

    // 오브젝트를 풀에서 가져오는 메서드입니다.
    // 해당 프리팹에 대한 풀이 없으면 새로 생성합니다.
    public GameObject Pop(GameObject prefab)
    {
        if (pools.ContainsKey(prefab.name) == false) CreatePool(prefab);

        return pools[prefab.name].Pop();
    }

    // 오브젝트를 풀에 반환하는 메서드입니다.
    // 반환된 오브젝트가 속한 풀이 없으면 false를 반환합니다.
    public bool Push(GameObject obj)
    {
        if (pools.ContainsKey(obj.name) == false) return false;

        pools[obj.name].Push(obj);
        return true;
    }

    // 새로운 풀을 생성하는 메서드입니다.
    // 프리팹을 인자로 받아 새로운 풀을 생성하고 딕셔너리에 추가합니다.
    private void CreatePool(GameObject prefab)
    {
        Pool pool = new(prefab);
        pools.Add(prefab.name, pool);
    }

    // 모든 풀을 초기화하는 메서드입니다.
    // 모든 풀을 초기화하고 딕셔너리를 비웁니다.
    public void Clear()
    {
        foreach (var pool in pools.Values)
        {
            pool.Clear();
        }
        pools.Clear();
    }
}

