using Unity.VisualScripting;
using UnityEngine;

// 장점
// - 기존 프로젝트들에서 사용하던 방법이 아닌 Singleton을 따로 분리하였습니다.
// - Singleton이 필요한 클래스마다 Singleton을 직접 구현할 필요 없이 해당 클래스를 상속받아 사용할 수 있습니다.
// - Singleton이 단일 클래스로 캡슐화 되어 유지보수성이 향상됩니다.
// - Singleton을 상속받아 사용하는 클래스들의 코드가 더욱 간결해지고 명확해집니다.

// 단점
// - 너무 많은 Singleton은 메모리 사용량이 증가하므로 적절한 사용이 요구됩니다.
// - 각각의 Singleton은 사용하는 클래스들이 독립적으로 초기화되어 명시적으로 관리하기 어렵습니다.
// - 클래스마다 각자 상속 받아야 하므로 구조가 다소 복잡해질 수 있습니다.

public class Singleton<T> : MonoBehaviour where T : Component
{
    #region Field
    // Singleton 인스턴스를 저장하는 정적 필드입니다.
    private static T _instance;
    #endregion

    #region Properties
    // Singleton 인스턴스를 반환하는 정적 프로퍼티입니다.
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.name = $"@{typeof(T).Name}";
                    _instance = singleton.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Unity Methods
    // MonoBehaviour의 Awake 메서드를 오버라이드하여 초기화 작업이 간편해집니다.
    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    #endregion
}
