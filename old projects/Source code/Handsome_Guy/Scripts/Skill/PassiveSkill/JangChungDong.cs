using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JangChungDong : BasePassive
{

    protected override void Init()
    {
        base.Init();
        if (!Main.DataManager.SkillDict.TryGetValue("장충동 왕 족발 보쌈", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
        player.ChangeMoveSpeed(player._moveSpeed * (1 + skill.increamentMoveSpeedRatio / 100f));
        player.ChangeSightRange(skill.increamentSightRange);
        player.ChangeDamage(player._damage + player._damage * (1 + skill.increamentDamage / 100f));
        player.ChangeDamageReduceRatio(skill.reductionDamageRatio);
    }
}
