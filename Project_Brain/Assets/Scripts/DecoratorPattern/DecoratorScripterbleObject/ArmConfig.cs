using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmConfig", menuName = "Arm/Config", order = 1)]
public class ArmConfig : ScriptableObject, IArm
{
    #region Fields
    // 이 필드는 무기의 발사 속도를 초당 몇 발인지 설정합니다.
    [Range(0, 60)]
    [Tooltip("Rate of firing per second")]
    [SerializeField] public float rate;

    // 이 필드는 팔의 사거리를 설정합니다.
    [Range(0, 50)]
    [Tooltip("Arm range")]
    [SerializeField] public float range;

    // 이 필드는 팔의 힘을 설정합니다.
    [Range(0, 100)]
    [Tooltip("Arm strength")]
    [SerializeField] public float strength;

    // 이 필드는 쿨다운 시간을 설정합니다.
    [Range(0, 5)]
    [Tooltip("Cooldown duration")]
    [SerializeField] public float cooldown;

    // 이 필드는 팔의 이름을 저장합니다.
    public string armName;

    // 이 필드는 팔의 프리팹을 저장합니다.
    public GameObject amrPrefab;

    // 이 필드는 팔의 설명을 저장합니다.
    public string armDescription;
    #endregion

    #region Properties
    // 이 속성은 발사 속도 값을 반환합니다.
    public float Rate { get { return rate; } }

    // 이 속성은 사거리 값을 반환합니다.
    public float Range { get { return range; } }

    // 이 속성은 힘 값을 반환합니다.
    public float Strength { get { return strength; } }

    // 이 속성은 쿨다운 값을 반환합니다.
    public float Cooldown { get { return cooldown; } }
    #endregion
}
