using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    #region Fields
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;
    #endregion

    #region Constructor
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }
    #endregion

    #region Methods

    // 상태에 진입할 때 호출됩니다.
    // 입력 액션 콜백을 추가합니다.
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    // 상태를 종료할 때 호출됩니다.
    // 입력 액션 콜백을 제거합니다.
    public virtual void Exit()
    {
        RemoveInputActionsCallbakcs();
    }

    // 입력 액션 콜백을 추가합니다.
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;
        input.PlayerActions.Jump.started += OnJumpStarted;
        input.PlayerActions.Attack.performed += OnAttackPerformed;
        input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }

    // 입력 액션 콜백을 제거합니다.
    protected virtual void RemoveInputActionsCallbakcs()
    {
        PlayerController input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;
        input.PlayerActions.Jump.started -= OnJumpStarted;
        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }

    // 입력을 처리합니다.
    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    // 물리 업데이트를 처리합니다.
    public virtual void PhysicsUpdate()
    {

    }

    // 매 프레임마다 호출됩니다.
    // 플레이어의 움직임을 처리합니다.
    public virtual void Update()
    {
        Move();
    }

    // 이동 입력이 취소되었을 때 호출됩니다.
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    // 달리기 입력이 시작되었을 때 호출됩니다.
    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    // 점프 입력이 시작되었을 때 호출됩니다.
    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    // 공격 입력이 수행되었을 때 호출됩니다.
    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }

    // 공격 입력이 취소되었을 때 호출됩니다.
    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }

    // 애니메이션을 시작합니다.
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    // 애니메이션을 중지합니다.
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    // 이동 입력을 읽어옵니다.
    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    // 플레이어를 이동시킵니다.
    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);

        Rotate(movementDirection);
    }

    // 이동 방향을 계산합니다.
    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    // 플레이어를 주어진 방향으로 이동시킵니다.
    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.Controller.Move((direction * movementSpeed + stateMachine.Player.ForceReceiver.Movement) * Time.deltaTime);
    }

    // 이동 속도를 계산합니다.
    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    // 플레이어를 주어진 방향으로 회전시킵니다.
    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    // 강제로 플레이어를 이동시킵니다.
    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }

    // 애니메이터의 현재 또는 다음 상태의 정규화된 시간을 반환합니다.
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag)) return nextInfo.normalizedTime;
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag)) return currentInfo.normalizedTime;
        else return 0f;
    }
    #endregion
}
