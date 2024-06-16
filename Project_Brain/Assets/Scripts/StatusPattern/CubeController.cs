using Define;
using UnityEngine;

// CubeController 클래스는 큐브의 상태를 관리하고, 큐브의 이동과 회전을 제어합니다.
public class CubeController : MonoBehaviour
{
    #region Fields
    // 큐브의 최대 속도, 회전 거리를 설정하는 변수입니다.
    public float maxSpeed = 2.0f;
    public float turnDistance = 2.0f;

    // 큐브의 시작 상태, 정지 상태, 회전 상태를 저장하는 변수들입니다.
    private ICubeState _startState, _stopState, _turnState;

    // 큐브의 상태 컨텍스트를 저장하는 변수입니다.
    private CubeStateContext _cubeStateContext;
    #endregion

    #region Properties
    // 큐브의 현재 속도를 설정하고 가져오는 프로퍼티입니다.
    public float CurrentSpeed { get; set; }

    // 큐브의 현재 회전 방향을 가져오는 프로퍼티입니다.
    public Direction CurrentTurnDirection { get; private set; }
    #endregion

    #region Unity Methods
    // 이 메서드에서는 큐브의 상태를 초기화합니다.
    public void Start()
    {
        // CubeStateContext 인스턴스를 생성하고, 큐브 컨트롤러를 전달합니다.
        _cubeStateContext = new CubeStateContext(this);

        // 각 상태를 CubeController에 추가하고, 해당 상태를 _startState, _stopState, _turnState에 할당합니다.
        _startState = gameObject.AddComponent<CubeStartState>();
        _stopState = gameObject.AddComponent<CubeStopState>();
        _turnState = gameObject.AddComponent<CubeTurnState>();

        // 초기 상태를 정지 상태로 설정합니다.
        _cubeStateContext.Transition(_stopState);
    }
    #endregion

    #region Methods
    // StartCube 메서드는 큐브를 시작 상태로 전환합니다.
    public void StartCube()
    {
        _cubeStateContext.Transition(_startState);
    }

    // StopCube 메서드는 큐브를 정지 상태로 전환합니다.
    public void StopCube()
    {
        _cubeStateContext.Transition(_stopState);
    }

    // TurnCube 메서드는 큐브를 회전 상태로 전환하고, 회전 방향을 설정합니다.
    public void TurnCube(Direction direction)
    {
        CurrentTurnDirection = direction;
        _cubeStateContext.Transition(_turnState);
    }
    #endregion
}
