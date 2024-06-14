using UnityEngine;

public abstract class SpaceSkill : MonoBehaviour
{
    // 스킬의 공격력을 나타내는 속성입니다.
    public int SkillDamage { get; protected set; }

    // 스킬을 사용할 때 호출되는 메서드입니다. 파라미터로 위치를 받습니다.
    public virtual void UseSkill(Vector3 dir) { } 
}
