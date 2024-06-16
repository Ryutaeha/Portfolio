using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Fields
    private PlayerStateMachine stateMachine;
    #endregion

    #region Properties
    [field: SerializeField] public PlayerSO Data { get; private set; }
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Weapon Weapon { get; private set; }
    public Health Health { get; private set; }
    #endregion

    #region Methods

    /// 이 메서드는 필요한 컴포넌트들을 초기화하고, 상태 머신을 설정합니다.
    void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        Health = GetComponent<Health>();
        stateMachine = new(this);
    }

    /// 이 메서드는 커서를 잠그고, 상태 머신의 초기 상태를 설정하며, Health 컴포넌트의 OnDie 이벤트에 OnDie 메서드를 등록합니다.
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
        Health.OnDie += OnDie;
    }

    /// 이 메서드는 매 프레임마다 상태 머신의 입력 처리와 업데이트를 호출합니다.
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    /// 이 메서드는 물리 업데이트를 처리하기 위해 상태 머신의 PhysicsUpdate 메서드를 호출합니다.
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    /// 이 메서드는 플레이어의 사망 애니메이션을 재생하고, 이 스크립트를 비활성화합니다.
    private void OnDie()
    {
        Animator.SetTrigger("Die");
        Weapon.enabled = false;
        enabled = false;
    }
    #endregion
}
