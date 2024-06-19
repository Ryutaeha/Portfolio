using UnityEngine;

[CreateAssetMenu(fileName = "newArmAttachment", menuName = "Arm/Attachment", order = 1)]
public class ArmAttachment : ScriptableObject, IArm
{
    #region Fields
    // 이 필드는 무기 발사 속도를 증가시키기 위한 값입니다.
    [Range(0, 50)]
    [Tooltip("Increase rate of firing per second")]
    [SerializeField] public float rate;

    // 이 필드는 팔의 사거리를 증가시키기 위한 값입니다.
    [Range(0, 50)]
    [Tooltip("Increase arm range")]
    [SerializeField] public float range;

    // 이 필드는 팔의 힘을 증가시키기 위한 값입니다.
    [Range(0, 100)]
    [Tooltip("Increase arm strength")]
    [SerializeField] public float strength;

    // 이 필드는 쿨다운 시간을 감소시키기 위한 값입니다.
    [Range(0, -5)]
    [Tooltip("Reduce cooldown duration")]
    [SerializeField] public float cooldown;

    // 이 필드는 부착물의 이름을 저장합니다.
    public string attachmentName;

    // 이 필드는 부착물의 프리팹을 저장합니다.
    public GameObject attachmentPrefab;

    // 이 필드는 부착물의 설명을 저장합니다.
    public string attachmentDescription;
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
