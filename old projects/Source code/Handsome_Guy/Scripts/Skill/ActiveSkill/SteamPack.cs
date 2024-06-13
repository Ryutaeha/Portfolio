using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPack : BaseActive
{
    private float _tempAttackSpeed;
    IEnumerator SteamPackCoroutine()
    {
        ApplySkillEffect();

        yield return new WaitForSeconds(skill.duration);

        ApplyOriginStatus();
    }

    IEnumerator SkillCoolCoroutine()
    {
        isSkillCool = true;

        yield return new WaitForSeconds(skill.coolTime);

        isSkillCool = false;
    }

    protected override void Init()
    {
        base.Init();
        if (!Main.DataManager.SkillDict.TryGetValue("스팀팩", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
        _tempAttackSpeed = player._attackSpeed;
    }

    public override void UseSkill()
    {
        if (isSkillCool) return;
        base.UseSkill();
        StartCoroutine(SteamPackCoroutine());
        StartCoroutine(SkillCoolCoroutine());
    }

    private void ApplySkillEffect()
    {
        isSkillCool = true;
        player.ChangeAttackSpeed(player._attackSpeed * (1 + (player._attackSpeed * skill.increamentAttackSpeedRatio) / 100));
    }

    private void ApplyOriginStatus()
    {
        player.ChangeAttackSpeed(_tempAttackSpeed);
        isSkillCool = false;
    }
}
