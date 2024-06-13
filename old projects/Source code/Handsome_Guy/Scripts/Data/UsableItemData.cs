using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 소비형 아이템 매개변수 관리
/// </summary>

public abstract class UsableItemData : MonoBehaviour
{
    [SerializeField] private bool destroyOnPickup = true;
    [SerializeField] private LayerMask saveOnPickup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (saveOnPickup.value == (saveOnPickup.value | (1 << other.gameObject.layer)))
        {
            OnUse(other.gameObject);

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
    }
    protected abstract void OnUse(GameObject receiver);
}

// Duration = 60s;

//1. HealPack 
//   PlayerHP += 10

//2. Grenade
//   AttackDamage = 10

//3. Dash
//   player.position += 10

//4. Puppet
//   position = player.position + 5, (monster attack) bool = true, duration = 4s

//5. GoldenKey
//   (locked door) bool = false 