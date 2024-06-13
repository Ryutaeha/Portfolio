using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Pistol : WeaponItemData
{
    protected override void OnEquip(GameObject receiver)
    {
        Main.ObjectManager.Player.ChangeDamage(Main.ObjectManager.Player._damage + 5);
        Main.ObjectManager.Player.ChangeAttackSpeed(Main.ObjectManager.Player._attackSpeed + 5);
    }

    // player stat 을 직접 변경하는 방법 (대안)
    public class pistol
    {
        //_originAtk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power;
        //GameManager.instance.UpdatePlayerAttackSODatas(0, _originAtk, 0);
    }
}



