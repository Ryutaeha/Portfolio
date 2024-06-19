using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDecorator : IArm
{
    #region Constructors
    // IArm 인스턴스와 ArmAttachment 인스턴스를 받아 ArmDecorator 객체를 초기화하는 생성자입니다.
    public ArmDecorator(IArm arm, ArmAttachment attachment)
    {
        _decoratedArm = arm;
        _attachment = attachment;
    }
    #endregion

    #region Fields
    // 장식된 IArm 인스턴스를 저장하는 읽기 전용 필드입니다.
    private readonly IArm _decoratedArm;

    // ArmAttachment 인스턴스를 저장하는 읽기 전용 필드입니다.
    private readonly ArmAttachment _attachment;
    #endregion

    #region Properties
    // 이 속성은 장식된 IArm 인스턴스의 발사 속도와 ArmAttachment 인스턴스의 발사 속도를 합산하여 반환합니다.
    public float Rate { get { return _decoratedArm.Rate + _attachment.Rate; } }

    // 이 속성은 장식된 IArm 인스턴스의 사거리와 ArmAttachment 인스턴스의 사거리를 합산하여 반환합니다.
    public float Range { get { return _decoratedArm.Range + _attachment.Range; } }

    // 이 속성은 장식된 IArm 인스턴스의 힘과 ArmAttachment 인스턴스의 힘을 합산하여 반환합니다.
    public float Strength { get { return _decoratedArm.Strength + _attachment.Strength; } }

    // 이 속성은 장식된 IArm 인스턴스의 쿨다운 시간과 ArmAttachment 인스턴스의 쿨다운 시간을 합산하여 반환합니다.
    public float Cooldown { get { return _decoratedArm.Cooldown + _attachment.Cooldown; } }
    #endregion
}
