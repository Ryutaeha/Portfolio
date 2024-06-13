using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Fields
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    private List<Collider> alreadyColliderWith = new List<Collider>();
    #endregion

    #region Methods
    // 무기가 활성화될 때 호출되며, 이미 충돌한 콜라이더 목록을 초기화합니다.
    private void OnEnable()
    {
        alreadyColliderWith.Clear();
    }

    // 다른 콜라이더와 충돌이 발생했을 때 호출되며, 충돌 처리 로직을 실행합니다.
    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (alreadyColliderWith.Contains(other)) return;

        alreadyColliderWith.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        if (other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    // 무기의 공격력을 설정하는 메서드입니다.
    // 공격력과 넉백 값을 설정합니다.
    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
    #endregion
}
