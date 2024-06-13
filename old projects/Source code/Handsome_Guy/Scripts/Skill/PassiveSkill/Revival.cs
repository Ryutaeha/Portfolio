using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival : BasePassive
{
    private int _revivalCount;
    public int RevivalCount { get { return _revivalCount; } private set { _revivalCount = value; } }

    protected override void Init()
    {
        base.Init();
        _revivalCount = 1;
        if (!Main.DataManager.SkillDict.TryGetValue("부활", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
    }

    IEnumerator OnRevivalCouroutine()
    {
        yield return new WaitForSeconds(2.0f);
        player.ChangeCurrentHp(Mathf.RoundToInt((player._maxHp / 100.0f) * skill.revivalHealthRatio));
        _revivalCount--;
    }

    public void OnRevival()
    {
        StartCoroutine(OnRevivalCouroutine());
    }
}
