using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Healpack : UsableItemData
{
    [SerializeField] int healValue = 10;
    //private HealthSystem _healthSystem;

    protected override void OnUse(GameObject receiver)
    {
        Main.ObjectManager.Player.ChangeCurrentHp(Main.ObjectManager.Player._currentHp + 20);
    }
}


