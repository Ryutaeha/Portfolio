using UnityEngine;

public class PaladinSkill : SpaceSkill
{
    private float _skillDuration = 3f; // 팔라딘 방패 지속시간

    // 객체가 생성될 때 호출되는 메서드입니다. 스킬의 기본 공격력을 설정하고, 일정 시간이 지나면 스킬 오브젝트를 제거합니다.
    private void Awake()
    {
        SkillDamage = 10;
        Destroy(gameObject, _skillDuration);
    }

    // 스킬이 2D 콜라이더와 충돌할 때 호출되는 메서드입니다. 적 투사체와 충돌하면 해당 투사체를 제거합니다.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemySkill")) Managers.Resource.Destroy(other.gameObject);
    }
}
