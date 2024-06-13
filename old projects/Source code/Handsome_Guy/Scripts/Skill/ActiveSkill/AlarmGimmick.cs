using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmGimmick : BaseActive
{
    private GameObject _alarmPrefab;
    IEnumerator SteamPackCoroutine()
    {
        isSkillCool = true;

        yield return new WaitForSeconds(skill.coolTime);

        isSkillCool = false;
    }
    protected override void Init()
    {
        base.Init();
        if (!Main.DataManager.SkillDict.TryGetValue("기계광", out skill))
        {
            Debug.Log("해당 스킬을 가져오는데 실패하였습니다.");
            return;
        }
    }

    public override void UseSkill()
    {
        if (isSkillCool) return;
        Transform[] gimmickpoints = GameObject.Find("GimmickSpwnPoint").GetComponentsInChildren<Transform>();

        Vector2 targetDir = gimmickpoints[0].position - player.transform.position;

        for (int i = 0; i < gimmickpoints.Length; i++)
        {
            Vector2 dirToGimmick = gimmickpoints[i].position - player.transform.position;

            if (targetDir.magnitude > dirToGimmick.magnitude)
            {
                targetDir = dirToGimmick;
            }
            
        }

        GameObject alarmPrefab = Main.ResourceManager.Instantiate("Alarm.prefab");

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, targetDir);

        alarmPrefab.transform.rotation = rotation;
        alarmPrefab.transform.position = new Vector2 (player.transform.position.x, player.transform.position.y + 5);

        base.UseSkill();
        StartCoroutine(SteamPackCoroutine());
    }
}
