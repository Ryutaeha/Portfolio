using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    #region Properties
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;

    [field: Header("IdleData")]

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;

    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
    #endregion
}

[Serializable]
public class PlayerAirData
{
    #region Properties
    [field: Header("JumpData")]
    [field: SerializeField][field: Range(0f, 25f)] public float JumpForce { get; private set; } = 5f;
    #endregion
}

[Serializable]
public class PlayerAttackData
{
    #region Properties
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    #endregion

    #region Methods
    /// 이 메서드는 공격 정보 데이터 리스트의 길이를 반환합니다.
    public int GetAttackInfoCount()
    {
        return AttackInfoDatas.Count;
    }

    /// 이 메서드는 주어진 인덱스에 해당하는 공격 정보 데이터를 반환합니다.
    public AttackInfoData GetAttackInfoData(int index)
    {
        return AttackInfoDatas[index];
    }
    #endregion
}

[Serializable]
public class AttackInfoData
{
    #region Fields
    [field: SerializeField] public int Damage;
    #endregion

    #region Properties
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
    #endregion
}

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    #region Properties
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }

    [field: SerializeField] public PlayerAirData AirData { get; private set; }

    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
    #endregion
}
