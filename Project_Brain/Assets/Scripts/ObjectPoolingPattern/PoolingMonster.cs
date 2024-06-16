using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PoolingMonster : MonoBehaviour
{
    #region Fields
    // 현재 체력입니다.
    private float _currentHealth;

    // 최대 체력입니다.
    [SerializeField] private float maxHealth = 100f;

    // 반환까지의 시간입니다.
    [SerializeField] private float timeToSelfDestruct = 3f;
    #endregion

    #region Properties
    // 객체 풀
    public IObjectPool<PoolingMonster> Pool { get; set; }
    #endregion

    #region Unity Methods
    // 시작할 때 체력을 최대 체력으로 설정합니다.
    private void Start()
    {
        
        _currentHealth = maxHealth;
    }

    // 활성화될 때 플레이어 공격 및 반환 테스트 코루틴을 시작합니다.
    private void OnEnable()
    {

        AttackPlayer();
        StartCoroutine(SelfDestruct());
    }

    // 비활성화될 때 몬스터 초기화합니다.
    private void OnDisable()
    {

        ResetMonster();
    }
    #endregion

    #region Methods
    // 객체 풀로 반환하는 메서드 입니다.
    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    // 몬스터 초기화 메서드 입니다.
    private void ResetMonster()
    {
        _currentHealth = maxHealth;
    }

    // 플레이어 공격 (디버그 메시지)를 출력하는 메서드입니다.
    public void AttackPlayer()
    {
        Debug.Log("Attack player");
    }

    // 데미지를 입는 메서드 입니다.
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        // 체력이 0 이하가 되면 객체 풀로 반환
        if (_currentHealth <= 0) ReturnToPool();
    }
    #endregion

    #region Coroutine
    // 일정 시간 대기 후 최대 체력만큼 데미지를 입는 테스트 코루틴입니다.
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToSelfDestruct);
        TakeDamage(maxHealth);
    }
    #endregion
}
