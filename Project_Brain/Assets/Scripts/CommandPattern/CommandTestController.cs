using Define;
using UnityEngine;

// CommandTestController 클래스는 명령을 테스트하는 데 사용됩니다.
public class CommandTestController : MonoBehaviour
{
    #region Fields
    // 충전 상태를 나타내는 변수입니다.
    private bool _isChargingOn;
    // 이동할 거리를 나타내는 변수입니다.
    private float _distance = 1f;
    #endregion

    #region Methods
    // 충전 상태를 토글하는 메서드입니다.
    public void ToggleCharging()
    {
        _isChargingOn = !_isChargingOn;
        Debug.Log($"Charging Active : {_isChargingOn}");
    }

    // 방향에 따라 오브젝트를 이동시키는 메서드입니다.
    public void Turn(Direction direction)
    {
        if (direction == Direction.Left) transform.Translate(Vector3.left * _distance);
        if (direction == Direction.Right) transform.Translate(Vector3.right * _distance);
    }

    // 오브젝트의 위치를 초기화하는 메서드입니다.
    public void ResetPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    #endregion
}
