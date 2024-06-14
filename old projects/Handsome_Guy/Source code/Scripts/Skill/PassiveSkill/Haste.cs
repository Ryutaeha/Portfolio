using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haste : BasePassive
{
    protected override void Init()
    {
        base.Init();
        if (!Main.DataManager.SkillDict.TryGetValue("헤이스트", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
        player.ChangeMoveSpeed(player._moveSpeed * (1 + skill.increamentMoveSpeedRatio / 100f));
    }
}
