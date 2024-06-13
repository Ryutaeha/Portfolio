using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ResourceManager
{
    public bool Loaded { get; set; }
    private Dictionary<string, UnityEngine.Object> resources = new Dictionary<string, UnityEngine.Object>();
    private Dictionary<string, AsyncOperationHandle> resourcesHandle = new Dictionary<string, AsyncOperationHandle>();
    private Dictionary<string, IList<IResourceLocation>> resourcesLabelHandle = new Dictionary<string, IList<IResourceLocation>>();

    // 주어진 키에 해당하는 게임 오브젝트를 인스턴스화합니다.
    public GameObject Instantiate(string key, Transform parent = null, bool instantiateInWorld = false, bool pooling = false)
    {
        GameObject go = GetResource<GameObject>(key);
        if (go == null)
        {
            Debug.LogError($"[ResourceManager] Instantiate({key}): 프리팹을 불러오지 못했습니다.");
            return null;
        }

        if (pooling) return Main.PoolManager.Pop(go);

        return UnityEngine.Object.Instantiate(go, parent, instantiateInWorld);
    }

    // 주어진 게임 오브젝트를 파괴합니다.
    public void Destroy(GameObject obj)
    {
        if (obj == null) return;

        if (Main.PoolManager.Push(obj)) return;

        UnityEngine.Object.Destroy(obj);
    }

    // 주어진 키에 해당하는 리소스를 반환합니다.
    public T GetResource<T>(string key) where T : UnityEngine.Object
    {
        if (!resources.TryGetValue(key, out UnityEngine.Object resource)) return null;
        return resource as T;
    }

    // 주어진 키에 해당하는 자산을 해제합니다.
    public void ReleaseAsset(string key)
    {
        if (resourcesHandle.TryGetValue(key, out AsyncOperationHandle operationHandle) == false) return;
        Addressables.Release(operationHandle);
        resources.Remove(key);
        resourcesHandle.Remove(key);
    }

    // 주어진 키에 해당하는 모든 자산을 해제합니다.
    public void ReleaseAllAsset(string key)
    {
        if (resourcesLabelHandle.TryGetValue(key, out IList<IResourceLocation> operationHandle) == false) return;
        foreach (IResourceLocation handle in operationHandle)
        {
            ReleaseAsset(handle.PrimaryKey);
        }
        resourcesLabelHandle.Remove(key);
    }

    // 주어진 키에 해당하는 자산을 비동기적으로 로드하고, 콜백을 호출합니다.
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        if (resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            callback?.Invoke(resource as T);
            return;
        }
        string loadKey = key;

        if (key.Contains(".sprite"))
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";
        if (key.Contains(".sprite"))
        {
            AsyncOperationHandle<Sprite> asyncOperation = Addressables.LoadAssetAsync<Sprite>(loadKey);
            asyncOperation.Completed += obj =>
            {
                resources.Add(key, obj.Result);
                resourcesHandle.Add(key, obj);
                callback?.Invoke(obj.Result as T);
            };
        }
        else if (key.Contains(".multiSprite"))
        {
            AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssetAsync<IList<Sprite>>(loadKey);
            HandleCallback<Sprite>(key, handle, objs => callback?.Invoke(objs as T));
        }
        else
        {
            var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
            asyncOperation.Completed += obj =>
            {
                resources.Add(key, obj.Result);
                resourcesHandle.Add(key, obj);
                callback?.Invoke(obj.Result as T);
            };
        }
    }

    // 주어진 키에 해당하는 자산을 인스턴스화합니다.
    public void InstantiateAssetAsync(string key, Transform parent = null, bool instantiateInWorld = false)
    {
        if (resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            Instantiate(key, parent, instantiateInWorld);
            return;
        }

        AsyncOperationHandle<GameObject> asyncOperation = Addressables.InstantiateAsync(key, parent, instantiateInWorld);
        asyncOperation.Completed += (AsyncOperationHandle<GameObject> obj) => {
            resources.Add(key, obj.Result);
            resourcesHandle.Add(key, obj);
        };
    }

    // 주어진 라벨에 해당하는 모든 자산을 비동기적으로 로드하고, 콜백을 호출합니다.
    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        AsyncOperationHandle<IList<IResourceLocation>> operation = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        operation.Completed += (AsyncOperationHandle<IList<IResourceLocation>> obj) => {
            int loadCount = 0;
            int totalCount = obj.Result.Count;
            resourcesLabelHandle.TryAdd(label, obj.Result);
            foreach (IResourceLocation location in obj.Result)
            {
                LoadAsync<T>(location.PrimaryKey, obj => {
                    loadCount++;
                    callback?.Invoke(location.PrimaryKey, loadCount, totalCount);
                });
            }
        };
        Loaded = true;
    }

    // 주어진 라벨에 해당하는 모든 자산을 비동기적으로 인스턴스화합니다.
    public void InstantialteAllAsync(string label, Transform parent = null, bool instantiateInWorld = false)
    {
        AsyncOperationHandle<IList<IResourceLocation>> operation = Addressables.LoadResourceLocationsAsync(label, typeof(GameObject));
        operation.Completed += (AsyncOperationHandle<IList<IResourceLocation>> obj) => {
            resourcesLabelHandle.Add(label, obj.Result);
            foreach (IResourceLocation location in obj.Result)
            {
                InstantiateAssetAsync(location.PrimaryKey, parent, instantiateInWorld);
            }
        };
    }

    // 비동기 핸들 콜백을 처리합니다.
    private void HandleCallback<T>(string key, AsyncOperationHandle<IList<T>> handle, Action<IList<T>> callback) where T : UnityEngine.Object
    {
        handle.Completed += operationHandle =>
        {
            IList<T> resultList = operationHandle.Result;

            for (int i = 0; i < resultList.Count; i++)
            {
                resources.Add(resultList[i].name, resultList[i]);
            }
            callback?.Invoke(resultList);
        };
    }
}

