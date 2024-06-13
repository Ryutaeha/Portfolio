using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateSupplies : MonoBehaviour
{
    // 보급상자
    public List<GameObject> supplies = new List<GameObject>();
    // 생성 위치
    public List<Transform> spawnPositions = new List<Transform>();

    void Start()
    {
        StartCoroutine(SuppliesSpawn());
    }

    IEnumerator SuppliesSpawn()
    {
        CreateBox();
        yield return new WaitForSecondsRealtime(5.0f);
    }
    void CreateBox()
    {
        int idx = Random.Range(0, supplies.Count);
        int posIdx = Random.Range(0, spawnPositions.Count);

        GameObject obj = supplies[idx];
        Instantiate(obj, spawnPositions[posIdx].position, Quaternion.identity);
    }

}
