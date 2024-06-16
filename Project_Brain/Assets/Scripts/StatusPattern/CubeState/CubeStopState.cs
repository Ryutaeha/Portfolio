using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CubeStopState 클래스는 ICubeState 인터페이스를 구현합니다. 이 클래스는 큐브의 정지 상태를 관리합니다.
public class CubeStopState : MonoBehaviour, ICubeState
{
    #region Fields
    // CubeController 인스턴스를 저장하는 필드입니다.
    private CubeController _cubeController;
    #endregion

    #region Methods
    // Handle 메서드는 CubeController 인스턴스를 받아서 큐브의 속도를 0으로 설정합니다.
    public void Handle(CubeController cubeController)
    {
        // _cubeController가 아직 설정되지 않은 경우에만 cubeController를 _cubeController에 할당합니다.
        if (!_cubeController) _cubeController = cubeController;

        // 큐브의 현재 속도를 0으로 설정하여 큐브를 정지시킵니다.
        _cubeController.CurrentSpeed = 0;
    }
    #endregion
}
