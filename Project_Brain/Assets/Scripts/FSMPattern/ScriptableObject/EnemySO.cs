using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    #region Fields
    [field: SerializeField] public int Damage;
    #endregion

    #region Properties
    [field: SerializeField][SerializeField] public float PlayerChasingRnage { get; private set; } = 10f;
    [field: SerializeField][SerializeField] public float AttackRange { get; private set; } = 1.5f;
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
    #endregion

    #region Methods
    /// 이 메서드는 적의 피해량을 설정합니다.
    public void SetDamage(int damage)
    {
        Damage = damage;
    }

    /// 이 메서드는 플레이어를 추적하는 범위를 설정합니다.
    public void SetPlayerChasingRange(float range)
    {
        PlayerChasingRnage = range;
    }

    /// 이 메서드는 적의 공격 범위를 설정합니다.
    public void SetAttackRange(float range)
    {
        AttackRange = range;
    }

    /// 이 메서드는 힘의 전환 시간을 설정합니다.
    public void SetForceTransitionTime(float time)
    {
        ForceTransitionTime = time;
    }

    /// 이 메서드는 적에게 가해지는 힘을 설정합니다.
    public void SetForce(float force)
    {
        Force = force;
    }

    /// 이 메서드는 적이 땅과 상호작용하는 데이터를 설정합니다.
    public void SetGroundData(PlayerGroundData data)
    {
        GroundData = data;
    }

    /// 이 메서드는 공격 시작 시의 전환 시간을 설정합니다.
    public void SetDealingStartTransitionTime(float time)
    {
        Dealing_Start_TransitionTime = time;
    }

    /// 이 메서드는 공격 종료 시의 전환 시간을 설정합니다.
    public void SetDealingEndTransitionTime(float time)
    {
        Dealing_End_TransitionTime = time;
    }
    #endregion
}
