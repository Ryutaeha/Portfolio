using UnityEngine;

public class PlayerAimRotation : MonoBehaviour
{
    float flipX;
    private PlayerCharacterController _controller;

    // 이 메서드는 객체가 깨어날 때 호출됩니다. 초기화 작업을 수행합니다.
    // 여기서는 flipX 변수를 객체의 초기 x축 스케일로 설정하고, PlayerCharacterController 컴포넌트를 가져옵니다.
    private void Awake()
    {
        flipX = transform.localScale.x;
        _controller = GetComponent<PlayerCharacterController>();
    }

    // 이 메서드는 게임 시작 시 처음 한 번 호출됩니다.
    // 여기서는 OnLookEvent 이벤트에 OnAim 메서드를 구독합니다.
    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    // 이 메서드는 OnLookEvent 이벤트가 발생할 때 호출됩니다.
    // 새로운 조준 방향이 전달되며, RotationAim 메서드를 호출합니다.
    void OnAim(Vector2 newAimDirection)
    {
        RotationAim(newAimDirection);
    }

    // 이 메서드는 전달된 방향 벡터에 따라 객체의 회전을 처리합니다.
    // 방향의 x값이 양수이면 객체를 좌우 반전시키고, 음수이면 원래 방향으로 설정합니다.
    void RotationAim(Vector2 direction)
    {
        if (direction.x > 0) transform.localScale = new Vector3(-flipX, transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(flipX, transform.localScale.y, transform.localScale.z);
    }

    // 이 메서드는 조준 이벤트를 설정하거나 해제합니다.
    // set 매개변수가 true이면 OnLookEvent 이벤트에 OnAim 메서드를 구독하고, false이면 구독을 취소합니다.
    public void SetAim(bool set)
    {
        if (set) _controller.OnLookEvent += OnAim;
        else _controller.OnLookEvent -= OnAim;
    }

    // 이 메서드는 OnLookEvent 이벤트에서 OnAim 메서드의 구독을 취소합니다.
    public void AimEventClear()
    {
        _controller.OnLookEvent -= OnAim;
    }
}
