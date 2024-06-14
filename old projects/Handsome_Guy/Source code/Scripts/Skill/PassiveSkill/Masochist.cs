using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masochist : BasePassive
{
    protected override void Init()
    {
        base.Init();
        if (!Main.DataManager.SkillDict.TryGetValue("마조히스트", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
        player.ChangeDamageReduceRatio(skill.reductionDamageRatio);
    }
}
