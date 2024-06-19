
using System.Collections.Generic;
using UnityEngine;

public class ClientStrategy : MonoBehaviour
{
    #region Fields
    private GameObject _enemyCube;

    private List<IManeuverBehaviour> _components = new List<IManeuverBehaviour>();
    #endregion

    #region Unity Methods
    // 유니티의 GUI 이벤트를 처리하는 메서드입니다.
    // "Spawn Enemy" 버튼을 클릭하면 적 큐브를 생성합니다.
    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Enemy"))
        {
            SpawnEnemy();
        }
    }
    #endregion

    #region Methods
    // 적 큐브를 생성하는 메서드입니다.
    private void SpawnEnemy()
    {
        _enemyCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        _enemyCube.AddComponent<EnemyCube>();

        _enemyCube.transform.position = Random.insideUnitSphere * 10;

        ApplyRandomStrategies();
    }

    // 생성된 적 큐브에 무작위 전략을 적용하는 메서드입니다.
    private void ApplyRandomStrategies()
    {
        _components.Add(_enemyCube.AddComponent<WeavingManeuver>());
        _components.Add(_enemyCube.AddComponent<BoppingManeuver>());
        _components.Add(_enemyCube.AddComponent<FallbackManeuver>());

        int index = Random.Range(0, _components.Count);

        _enemyCube.GetComponent<EnemyCube>().ApplyStrategy(_components[index]);
    }

    #endregion
}
