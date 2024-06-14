using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invisible : BaseActive
{
    private Color _tempColor;
    private float _transparency;
    private SpriteRenderer _playerSR;
    IEnumerator InvisibleManCoroutine()
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
        if (!Main.DataManager.SkillDict.TryGetValue("투명인간", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
        _playerSR = player.transform.Find("MainSprite").GetComponent<SpriteRenderer>();
        _tempColor = _playerSR.color;
        _transparency = 0.35f;
    }

    public override void UseSkill()
    {
        if (isSkillCool) return;
        base.UseSkill();
        StartCoroutine(InvisibleManCoroutine());
        StartCoroutine(SkillCoolCoroutine());
    }

    private void ApplySkillEffect()
    {
        
        Color invisibleColor = _tempColor;
        invisibleColor.a = invisibleColor.a * _transparency;

        player.tag = "Untagged";
        player.transform.Find("MainSprite").GetComponent<SpriteRenderer>().color = invisibleColor;
        GameObject.Find("WeaponSprite").GetComponent<SpriteRenderer>().color = invisibleColor;
    }

    private void ApplyOriginStatus()
    {
        player.tag = "Player";
        player.transform.Find("MainSprite").GetComponent<SpriteRenderer>().color = _tempColor;
        GameObject.Find("WeaponSprite").GetComponent<SpriteRenderer>().color = _tempColor; 
    }
}
