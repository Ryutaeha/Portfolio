using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Sniper : WeaponItemData
{

    protected override void OnEquip(GameObject receiver)
    {
        Main.ObjectManager.Player.ChangeDamage(Main.ObjectManager.Player._damage + 3);
        Main.ObjectManager.Player.ChangeAttackSpeed(Main.ObjectManager.Player._attackSpeed + 10);
    }
}