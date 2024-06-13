using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCharacterController _controller;
    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private PlayerAbility _ability;
    bool invincibility;

    // 이 메서드는 객체가 깨어날 때 호출됩니다. 초기화 작업을 수행합니다.
    // 여기서는 PlayerAbility, PlayerCharacterController, Rigidbody2D 컴포넌트를 가져옵니다.
    private void Awake()
    {
        _ability = GetComponent<PlayerAbility>();
        _controller = GetComponent<PlayerCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // 이 메서드는 게임 시작 시 처음 한 번 호출됩니다.
    // 여기서는 PlayerAbility의 Animator를 가져오고,
    // PlayerCharacterController의 OnMoveEvent 이벤트에 Move 메서드를 구독하여
    // 이벤트가 발생할 때 호출되도록 합니다.
    private void Start()
    {
        _animator = GetComponent<PlayerAbility>()._animator;
        _controller.OnMoveEvent += Move;
    }

    // 이 메서드는 FixedUpdate 주기마다 호출됩니다.
    // 여기서는 _movementDirection에 따라 물리적 움직임을 적용합니다.
    private void FixedUpdate()
    {
        ApplyMovment(_movementDirection);
    }

    // 이 메서드는 OnMoveEvent 이벤트가 발생할 때 호출됩니다.
    // 새로운 이동 방향 벡터를 받아 _movementDirection에 저장합니다.
    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    // 이 메서드는 주어진 방향 벡터에 따라 객체의 물리적 이동을 적용합니다.
    // 이동 속도와 방향에 따라 Rigidbody2D의 위치를 업데이트하고,
    // 애니메이터의 RunState 파라미터를 설정합니다.
    private void ApplyMovment(Vector2 direction)
    {
        direction = direction * _ability.Speed * Time.fixedDeltaTime;
        Vector2 moveVector = _rigidbody.position + direction;
        _rigidbody.MovePosition(moveVector);
        invincibility = _ability.invincibility;

        if (!direction.Equals(Vector2.zero) && !invincibility) _animator.SetFloat("RunState", 0.25f);

        else if (direction.Equals(Vector2.zero) && !invincibility) _animator.SetFloat("RunState", 0.0f);

        else if (!direction.Equals(Vector2.zero) && invincibility) _animator.SetFloat("RunState", 0.75f);

        else if (direction.Equals(Vector2.zero) && invincibility) _animator.SetFloat("RunState", 0.5f);

    }

    // 이 메서드는 이동 이벤트를 설정하거나 해제합니다.
    // 매개변수 set이 true이면 OnMoveEvent 이벤트에 Move 메서드를 구독하고,
    // false이면 구독을 취소합니다.
    public void SetMove(bool set)
    {
        _movementDirection = Vector2.zero;
        if (set)
            _controller.OnMoveEvent += Move;
        else
            _controller.OnMoveEvent -= Move;
    }

    // 이 메서드는 OnMoveEvent 이벤트에서 Move 메서드의 구독을 취소합니다.
    public void MoveEventClear()
    {
        _controller.OnMoveEvent -= Move;
    }
}
