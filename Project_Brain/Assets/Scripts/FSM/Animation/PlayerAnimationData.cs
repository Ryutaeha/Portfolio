using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    #region Fields
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string comboAttackParameterName = "ComboAttack";

    [SerializeField] private string baseAttackParameterName = "BaseAttack";
    #endregion

    #region Properties
    public int GroundParameterHash {  get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    public int AttackParameterHash {  get; private set; }
    public int ComboAttackParameterHash {  get; private set; }

    public int BaseAttackParameterHash { get; private set ; }

    #endregion

    #region Methods
    // Initialize 메서드는 이 클래스의 필드로 선언된 애니메이션 파라미터 이름을 해시값으로 변환하여 각 해시 속성에 할당합니다.
    // 이 메서드는 애니메이션 파라미터를 효율적으로 비교하고 사용할 수 있도록 도와줍니다. 
    public void Initialize()
    {
        // 해시 값을 사용하는 이유:
        // 1. 성능 개선: 문자열 비교보다 해시 값 비교가 더 빠릅니다.
        // 2. 메모리 사용량 감소: 해시 값은 정수형 데이터로, 문자열보다 메모리를 적게 차지합니다.
        // 3. 코드 가독성 및 유지보수성 향상: 오타와 잘못된 문자열 입력을 줄여 디버깅이 용이해집니다.
        // 4. 일관성 유지: 동일한 파라미터에 대해 항상 동일한 해시 값을 얻을 수 있습니다.

        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);

        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackParameterName);

        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);
    }
    #endregion
}
