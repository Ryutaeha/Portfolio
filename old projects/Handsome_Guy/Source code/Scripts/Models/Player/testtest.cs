using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtest : BaseScene
{
    public override void Clear()
    {
        
    }

    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;
        Main.ObjectManager.Spawn<Player>("SeongGyuPlayer", Vector2.zero);
        return true;
    }
}
