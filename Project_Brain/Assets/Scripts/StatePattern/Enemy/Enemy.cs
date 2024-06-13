using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Fields
    private EnemyStateMachine stateMachine;
    #endregion

    #region Properties
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Weapon Weapon { get; private set; }
    public Health Health { get; private set; }
    #endregion

    #region Methods
    /// 애니메이션 데이터를 초기화하고, Animator, CharacterController, ForceReceiver 컴포넌트를 가져옵니다.
    /// 또한 상태 기계를 초기화합니다.
    void Awake() 
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        Health = GetComponent<Health>();
        stateMachine = new(this);
    }

    /// 커서를 잠금 상태로 설정하고, 상태 기계를 Idle 상태로 전환합니다.
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
        Health.OnDie += OnDie;
    }

    /// 상태 기계의 입력 처리를 수행하고, 상태 기계를 업데이트합니다.
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    /// 상태 기계의 물리 업데이트를 수행합니다.
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    private void OnDie()
    {
        Animator.SetTrigger("Die");
        Weapon.enabled = false;
        enabled = false;
    }
    #endregion
}
