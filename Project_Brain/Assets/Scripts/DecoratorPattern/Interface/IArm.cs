public interface IArm
{
    #region Properties
    // 이 속성은 팔의 사거리를 반환합니다.
    public float Range { get; }

    // 이 속성은 무기의 발사 속도를 반환합니다.
    public float Rate { get; }

    // 이 속성은 팔의 힘을 반환합니다.
    public float Strength { get; }

    // 이 속성은 쿨다운 시간을 반환합니다.
    public float Cooldown { get; }
    #endregion
}
