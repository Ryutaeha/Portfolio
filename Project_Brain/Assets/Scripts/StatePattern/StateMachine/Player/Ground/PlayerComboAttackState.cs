using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    #region Fields
    private bool alreadyAppliedCombo;
    private bool alreadyApplyForce;
    private bool alreadyAppliedDealing;

    AttackInfoData attackInfoData;
    #endregion

    #region Constructor
    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    #endregion

    #region Methods
    // 상태가 시작될 때 호출되는 메서드입니다.
    // 콤보 공격 애니메이션을 시작하고, 관련 변수를 초기화합니다.
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        alreadyAppliedCombo = false;
        alreadyApplyForce = false;
        alreadyAppliedDealing = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfoData(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    // 상태가 종료될 때 호출되는 메서드입니다.
    // 콤보 공격 애니메이션을 중지하고, 콤보가 적용되지 않았을 경우 콤보 인덱스를 초기화합니다.
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if (!alreadyAppliedCombo) stateMachine.ComboIndex = 0;
    }

    // 상태가 업데이트될 때 호출되는 메서드입니다.
    // 공격 애니메이션의 정상화된 시간을 체크하고, 힘 적용 및 콤보 공격 시도를 처리합니다.
    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
            {
                TryApplyForce();
            }
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
            {
                TryComboAttack();
            }

            if (!alreadyAppliedDealing && normalizedTime >= attackInfoData.Dealing_Start_TransitionTime)
            {
                stateMachine.Player.Weapon.SetAttack(attackInfoData.Damage, attackInfoData.Force);
                stateMachine.Player.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }

            if (alreadyAppliedDealing && normalizedTime >= attackInfoData.Dealing_End_TransitionTime)
            {
                stateMachine.Player.Weapon.gameObject.SetActive(value: false);
            }
        }
        else
        {
            if (alreadyAppliedCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    // 콤보 공격을 시도하는 메서드입니다.
    // 이미 콤보가 적용되었거나, 콤보 상태 인덱스가 유효하지 않거나, 플레이어가 공격 중이 아닐 경우 콤보를 적용하지 않습니다.
    private void TryComboAttack()
    {
        if (alreadyAppliedCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;

        alreadyAppliedCombo = true;
    }

    // 힘을 적용하는 메서드입니다.
    // 이미 힘이 적용되었을 경우 다시 적용하지 않습니다.
    private void TryApplyForce()
    {
        if (alreadyApplyForce) return;
        alreadyApplyForce = true;

        stateMachine.Player.ForceReceiver.Reset();

        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }
    #endregion
}
