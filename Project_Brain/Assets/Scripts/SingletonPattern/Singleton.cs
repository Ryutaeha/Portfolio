using Unity.VisualScripting;
using UnityEngine;

#region Advantages
// 장점
// - 기존 프로젝트들에서 사용하던 방법이 아닌 Singleton을 따로 분리하였습니다.
// - Singleton이 필요한 클래스마다 Singleton을 직접 구현할 필요 없이 해당 클래스를 상속받아 사용할 수 있습니다.
// - Singleton이 단일 클래스로 캡슐화 되어 유지보수성이 향상됩니다.
// - Singleton을 상속받아 사용하는 클래스들의 코드가 더욱 간결해지고 명확해집니다.
#endregion

#region Disadvantages
// 단점
// - 너무 많은 Singleton은 메모리 사용량이 증가하므로 적절한 사용이 요구됩니다.
// - 각각의 Singleton은 사용하는 클래스들이 독립적으로 초기화되어 명시적으로 관리하기 어렵습니다.
// - 클래스마다 각자 상속 받아야 하므로 구조가 다소 복잡해질 수 있습니다.
#endregion

#region When to Use
// Singleton을 사용할만한 경우
// - 전역 상태 관리: 애플리케이션 전체에서 공유되는 전역 상태나 설정 정보를 관리할 때.
// - 자원 관리: 데이터베이스 연결, 파일 시스템 접근 등 비용이 많이 드는 자원을 관리할 때.
// - 로깅: 애플리케이션 전반에 걸쳐 로그를 기록할 때.
// - 캐싱: 자주 사용되는 데이터를 캐싱하여 성능을 높일 때.
// - 구성 설정: 애플리케이션의 설정 정보를 중앙에서 관리할 때.
// - 스레드 풀: 스레드 풀과 같이 제한된 자원을 여러 스레드에서 공유할 때.
#endregion


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
                    GameObject singleton = new ();
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
