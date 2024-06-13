using DG.Tweening;
using UnityEngine;

public class MageSpaceSkill : SpaceSkill
{
    private Transform _spriteRenderTrans;
    private Vector3 _maxScope = new Vector3(2.0f, 2.0f);
    private float _skillDuration = 1.5f; // 메이지스킬 지속시간

    // 객체가 생성될 때 호출되는 메서드입니다. 스킬의 기본 공격력을 설정합니다.
    private void Awake()
    {
        SkillDamage = 30;
    }

    // 스킬을 사용할 때 호출되는 메서드입니다. 스킬의 위치를 설정하고, 스킬의 크기를 애니메이션으로 변경한 후 스킬을 제거합니다.
    public override void UseSkill(Vector3 dir)
    {
        transform.position = dir;
        _spriteRenderTrans = GetComponent<SpriteRenderer>().transform;
        _spriteRenderTrans.DOScale(_maxScope, _skillDuration).OnComplete(() =>
        {
            Managers.Resource.Destroy(gameObject);
        });
        Managers.Sound.SoundPlay("SFX/mageskill", SoundType.Effect);
    }

    // 스킬이 2D 콜라이더와 충돌할 때 호출되는 메서드입니다. 적 투사체와 충돌하면 해당 투사체를 제거합니다.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemySkill")) Managers.Resource.Destroy(other.gameObject);
    }
}
