using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보급상자의 생성, 획득, 파괴
/// 아이템 획득, 사용, 파괴
/// </summary>

public class ItemObject : MonoBehaviour, IInteraction
{
    // 무기 아이템
    Rifle rifle = new Rifle();
    Sniper sniper = new Sniper();

    // 소비 아이템
    Healpack healpack = new Healpack();
    Dash dash = new Dash();
    Puppet puppet = new Puppet();
    Grenade grenade = new Grenade();

    // 닿으면 상자 파괴
    [SerializeField] private bool destroyOnPickup = true;
    [SerializeField] private bool saveOnPickup = true;

    // 아이템 레이어 생성
    [SerializeField] private LayerMask supplies;

    public virtual void Interaction()
    {

    }

    // 아이템 획득
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 조건) 아이템 레이어라면 
        if (supplies.value == (supplies.value | (1 << other.gameObject.layer)))
        {
            // 조건) 장착 아이템이라면
            //if (items == equipitems)
            {
            // 기본 총 해제, 아이템 장착
            
            }

            // 조건) 소비 아이템이라면
            // if (items == usableitems)
            {
                // 소비 아이템 칸에 아이템 등록
            }
            Destroy(gameObject);
        }
    }

    //protected void Supplies(GameObject receiver);
    //{
    //}
}

// 성규님 작성
//public class gun2 : ItemObject
//{
//    public override void Interaction()
//    {
//        base.Interaction();
//        // 소총 상호작용 함수

//        // 소총 능력치 반영
//        // 갈아끼우는 작업
//    }
//}

