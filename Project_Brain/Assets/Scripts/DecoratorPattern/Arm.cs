using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : IArm
{
    #region Constructors
    // ArmConfig 인스턴스를 받아 Arm 객체를 초기화하는 생성자입니다.
    public Arm(ArmConfig armConfig)
    {
        _config = armConfig;
    }
    #endregion

    #region Fields
    // ArmConfig 인스턴스를 저장하는 읽기 전용 필드입니다.
    private readonly ArmConfig _config;
    #endregion

    #region Properties
    // 이 속성은 ArmConfig 인스턴스의 사거리 값을 반환합니다.
    public float Range { get { return _config.Range; } }

    // 이 속성은 ArmConfig 인스턴스의 발사 속도 값을 반환합니다.
    public float Rate { get { return _config.Rate; } }

    // 이 속성은 ArmConfig 인스턴스의 힘 값을 반환합니다.
    public float Strength { get { return _config.Strength; } }

    // 이 속성은 ArmConfig 인스턴스의 쿨다운 시간을 반환합니다.
    public float Cooldown { get { return _config.Cooldown; } }
    #endregion
}
