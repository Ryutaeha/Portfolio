using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Fields
    [SerializeField] private int maxHealth = 100;
    private int health;

    public bool IsDie;
    #endregion

    #region Events
    public event Action OnDie;
    #endregion

    #region Methods
    // 이 메서드에서는 현재 체력을 최대 체력으로 초기화하고, 
    // IsDie 값을 false로 설정합니다.
    void Start()
    {
        health = maxHealth;
        IsDie = false;
    }
     
    // TakeDamage 메서드는 외부에서 데미지를 입었을 때 호출됩니다.
    // damage 매개변수는 입은 데미지의 양을 나타냅니다.
    // 이 메서드는 현재 체력에서 데미지를 뺀 후, 체력이 0 이하가 되면 IsDie를 true로 설정하고 OnDie 이벤트를 호출합니다.
    public void TakeDamage(int damage)
    {
        if (health == 0) return;

        health = Mathf.Max(health - damage, 0);

        if (health == 0)
        {
            IsDie = true;
            OnDie?.Invoke();
        }

        Debug.Log(health);
    }
    #endregion
}
