using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CubeTurnState 클래스는 ICubeState 인터페이스를 구현합니다. 이 클래스는 큐브의 회전 상태를 관리합니다.
public class CubeTurnState : MonoBehaviour, ICubeState
{
    #region Fields
    // 회전 방향을 저장하는 벡터입니다.
    private Vector3 _turnDirection;
    // CubeController 인스턴스를 저장하는 필드입니다.
    private CubeController _cubeController;
    #endregion

    #region Methods
    // Handle 메서드는 CubeController 인스턴스를 받아서 큐브의 회전 방향을 설정하고, 큐브의 이동을 처리합니다.
    public void Handle(CubeController cubeController)
    {
        // _cubeController가 아직 설정되지 않은 경우에만 cubeController를 _cubeController에 할당합니다.
        if (!_cubeController) _cubeController = cubeController;

        // 현재 회전 방향을 _turnDirection의 x축에 설정합니다.
        _turnDirection.x = (float)_cubeController.CurrentTurnDirection;

        // 현재 속도가 0보다 큰 경우에만 큐브를 회전 방향에 따라 회전시킵니다.
        if (_cubeController.CurrentSpeed > 0) transform.Translate(_turnDirection * _cubeController.turnDistance);
    }
    #endregion
}
