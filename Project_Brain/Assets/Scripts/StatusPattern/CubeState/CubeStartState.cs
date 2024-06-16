using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CubeStartState 클래스는 ICubeState 인터페이스를 구현합니다. 이 클래스는 큐브의 시작 상태를 관리합니다.
public class CubeStartState : MonoBehaviour, ICubeState
{
    #region Fields
    // CubeController 인스턴스를 저장하는 필드입니다.
    private CubeController _cubeController;
    #endregion

    #region Unity Methods
    private void Update()
    {
        // _cubeController가 유효한 경우에만 아래의 코드를 실행합니다.
        if (_cubeController)
        {
            // 현재 속도가 0보다 큰 경우 큐브를 현재 속도에 따라 앞으로 이동시킵니다.
            if (_cubeController.CurrentSpeed > 0) _cubeController.transform.Translate(Vector3.forward * (_cubeController.CurrentSpeed * Time.deltaTime));
        }
    }
    #endregion

    #region Methods
    // Handle 메서드는 CubeController 인스턴스를 받아서 큐브의 초기 속도를 설정합니다.
    public void Handle(CubeController cubeController)
    {
        // _cubeController가 아직 설정되지 않은 경우에만 cubeController를 _cubeController에 할당합니다.
        if (!_cubeController) _cubeController = cubeController;

        // 큐브의 현재 속도를 최대 속도로 설정합니다.
        _cubeController.CurrentSpeed = _cubeController.maxSpeed;
    }
    #endregion
}
