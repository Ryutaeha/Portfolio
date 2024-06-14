using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sadist : BasePassive
{
    private int nextGoalKill;
    private int stack;

    protected override void Init()
    {
        base.Init();
        if (!Main.DataManager.SkillDict.TryGetValue("사디스트", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
        stack = 1;
        nextGoalKill = skill.increamentUnit * stack;
    }

    private void Update()
    {
        if (player._killCount >= nextGoalKill)
        {
            UpdatePlayerAttackDamage();
        }
    }

    public void UpdatePlayerAttackDamage()
    {
        player.ChangeDamage(player._damage + skill.increamentDamage);
        stack++;
        nextGoalKill = skill.increamentUnit * stack;
    }
}
