using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class BaseSkill : MonoBehaviour
{
    protected Player player;
    protected Data.Skill skill;
    private void Start()
    {
        Init();
    }


    protected virtual void Init()
    {
        player = Main.ObjectManager.Player;
        Debug.Log(player.tag);
    }
}
