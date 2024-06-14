using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : PlayerCharacterController
{
    private Camera _camera;
    // 주 카메라를 찾아서 _camera 변수에 할당합니다.
    private void Awake()
    {
        _camera = Camera.main;
    }

    // 이 메서드는 플레이어가 이동 입력을 할 때 호출됩니다.
    // 입력된 이동 값을 받아서 정규화한 후, CallMoveEvent 메서드를 호출하여 이동 이벤트를 전달합니다.
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    // 이 메서드는 플레이어가 조준 입력을 할 때 호출됩니다.
    // 입력된 조준 값을 화면 좌표에서 월드 좌표로 변환한 후, 새로운 조준 방향을 계산하여 CallLookEvent 메서드를 호출합니다.
    // 조준 방향의 크기가 0.9 이상일 때만 이벤트를 호출합니다.
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f) CallLookEvent(newAim);
    }

    // 이 메서드는 플레이어가 스킬 입력을 할 때 호출됩니다.
    // CallSpaceSkillEvent 메서드를 호출하여 스킬 이벤트를 전달합니다.
    public void OnSkill(InputValue value)
    {
        CallSpaceSkillEvent();
    }
}
