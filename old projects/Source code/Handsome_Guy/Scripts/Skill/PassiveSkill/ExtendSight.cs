using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendSight : BasePassive
{
    private PlayerStatusController _playerStatusController;
    private float _tempPlayerSight;
    protected override void Init()
    {
        base.Init();
        if (!Main.DataManager.SkillDict.TryGetValue("선택적 올빼미", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
        _tempPlayerSight = player._sightRange;
        _playerStatusController = gameObject.GetComponent<PlayerStatusController>();
        _playerStatusController.missionEvent += StartCoroutineAfterCheck;
    }

    IEnumerator IncreamentPlayerSightCoroutine()
    {
        IncreamentPlayerSight();

        yield return new WaitForSeconds(skill.duration);

        player.ChangeSightRange(_tempPlayerSight);
    }

    private void IncreamentPlayerSight()
    {
        player.ChangeSightRange(player._sightRange + skill.increamentSightRange);
    }

    public void StartCoroutineAfterCheck()
    {
        
        StartCoroutine(IncreamentPlayerSightCoroutine());
    }
}
