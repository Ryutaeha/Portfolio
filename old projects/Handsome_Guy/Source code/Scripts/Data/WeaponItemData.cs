using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장착형 아이템 매개변수 관리
/// </summary>

public abstract class WeaponItemData : MonoBehaviour
{
    [SerializeField] private bool destroyOnPickup = true;
    [SerializeField] private LayerMask saveOnPickup;

    protected float originAtk;
    protected float originAtkSpeed;
    protected float originAtkRange;
    protected float changeAtk;
    protected float changeAtkSpeed;
    protected float changeAtkRange;

    PlayerData damage;
    PlayerData attackSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (saveOnPickup.value == (saveOnPickup.value | (1 << other.gameObject.layer)))
        {
            OnEquip(other.gameObject);

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
    }
    protected abstract void OnEquip(GameObject receiver);
}


// Duration = 30s;

// 1. Pistol (5, 5, 5)
// 2. Bow, (7, 7, 7)
// 3. Rifle, (10, 10, 10)


// switch 방식으로 무기 타입을 바꿔 입력하는 방식 (대안)
//    public void WeaponItem()
//    {
//        switch (WeaponData.weaponType)
//        {
//            case WeaponType.Pistol:
//                Pistol();
//                break;

//            case WeaponType.Rifle:
//                Rifle();
//                break;

//            case WeaponType.Bow:
//                Bow();
//                break;

//            default:
//                break;
//        }
//    }

