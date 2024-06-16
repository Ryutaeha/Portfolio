using UnityEngine;
using Define;

// ClientState 클래스는 사용자가 큐브를 조작할 수 있도록 UI를 제공합니다.
public class ClientState : MonoBehaviour
{
    #region Fields
    // CubeController 인스턴스를 저장하는 필드입니다.
    private CubeController _cubeController;
    #endregion

    #region Unity Methods
    // 이 메서드에서는 CubeController 인스턴스를 찾습니다.
    private void Start()
    {
        // 씬에서 CubeController 타입의 객체를 찾아 _cubeController에 할당합니다.
        _cubeController = (CubeController)FindObjectOfType(typeof(CubeController));
    }

    // 이 메서드에서는 큐브를 조작할 수 있는 버튼을 생성합니다.
    private void OnGUI()
    {
        if (GUILayout.Button("Start Cube")) _cubeController.StartCube();
        if (GUILayout.Button("Turn Left")) _cubeController.TurnCube(Direction.Left);
        if (GUILayout.Button("Turn Right")) _cubeController.TurnCube(Direction.Right);
        if (GUILayout.Button("Stop Cube")) _cubeController.StopCube();
    }
    #endregion
}
