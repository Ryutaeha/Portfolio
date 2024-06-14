using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string id;
    public float currrentHp;
    public float maxHp;
    public float moveSpeed;
    public float damage;
    public float attackSpeed;
    public float damageReduceRatio;
    public float sightRange;
    public int killCount;
}
