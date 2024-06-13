using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActive : BaseSkill
{
    protected bool isSkillCool;

    protected override void Init()
    {
        base.Init();
        isSkillCool = false;
    }

    public virtual void UseSkill()
    {

    }

}
