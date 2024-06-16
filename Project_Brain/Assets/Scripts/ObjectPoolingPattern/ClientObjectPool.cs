using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientObjectPool : MonoBehaviour
{
    #region Fields
    // MonsterObjectPool 객체를 참조하는 필드입니다.
    private MonsterObjectPool _pool; 
    #endregion

    #region Unity Methods
    // 게임 오브젝트에 MonsterObjectPool 컴포넌트를 추가합니다.
    private void Start()
    {
        
        _pool = gameObject.AddComponent<MonsterObjectPool>();
    }

    // "Spawn Monsters" 버튼을 생성하고, 클릭 시 몬스터를 스폰합니다.
    private void OnGUI()
    {

        if (GUILayout.Button("Spawn Monsters")) _pool.Spawn();
    }
    #endregion
}
