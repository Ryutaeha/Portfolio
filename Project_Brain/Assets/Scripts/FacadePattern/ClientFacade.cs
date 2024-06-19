
using UnityEngine;

#region Advantages
// 장점
// - 객체를 생성하는 복잡한 과정을 캡슐화할 수 있습니다. 클라이언트는 객체 생성의 세부사항을 몰라도 됩니다.
// - 코드의 가독성과 유지보수성이 향상됩니다. 객체 생성 로직이 한 곳에 모여 있어 수정이 용이합니다.
// - 객체 생성 시 필요에 따라 다양한 설정을 할 수 있습니다. 설정에 따라 다른 종류의 객체를 생성할 수 있습니다.
// - 객체 생성 과정에서의 일관성을 유지할 수 있습니다. 동일한 방식으로 객체를 생성하여 일관성을 확보할 수 있습니다.
// - 객체 생성 시 추가적인 로직을 포함할 수 있습니다. 예를 들어, 객체 생성 시 로깅이나 검증 작업을 수행할 수 있습니다.
#endregion

#region Disadvantages
// 단점
// - 패턴을 남발하면 코드가 복잡해질 수 있습니다. 간단한 객체 생성에도 퍼샤드 패턴을 사용하면 오히려 코드가 복잡해질 수 있습니다.
// - 객체 생성 로직이 퍼샤드 클래스에 집중되면, 퍼샤드 클래스가 너무 비대해질 수 있습니다.
// - 모든 객체 생성에 퍼샤드 패턴을 적용하는 것은 비효율적일 수 있습니다. 특정 경우에만 사용하는 것이 바람직합니다.
// - 퍼샤드 클래스가 객체 생성의 모든 세부사항을 알아야 하므로, 변경에 민감할 수 있습니다. 객체 생성 로직이 변경되면 퍼샤드 클래스도 함께 수정해야 합니다.
#endregion

#region When to Use
// 퍼샤드 패턴을 사용할 만한 경우
// - 객체 생성 과정이 복잡하고 여러 단계를 거쳐야 하는 경우: 생성 과정이 복잡해 클라이언트 코드에서 다루기 어려울 때.
// - 객체 생성 시 여러 구성 옵션이 필요한 경우: 다양한 설정에 따라 객체를 생성해야 할 때.
// - 동일한 방식으로 여러 객체를 생성해야 하는 경우: 객체 생성의 일관성을 유지하고 싶을 때.
// - 객체 생성과 관련된 코드의 중복을 줄이고 싶은 경우: 객체 생성 로직이 여러 곳에 분산되어 중복이 발생할 때.
// - 객체 생성 시 추가적인 작업이 필요한 경우: 객체 생성 시 부가적인 작업(예: 로깅, 검증 등)을 함께 수행해야 할 때.
#endregion

public class ClientFacade : MonoBehaviour
{
    #region Fields
    private Engine _engine;
    #endregion

    #region Unity Methods
    // 게임 오브젝트에 엔진 컴포넌트를 추가합니다.
    private void Start()
    {
        _engine = gameObject.AddComponent<Engine>();
    }

    // "Turn On", "Turn Off", "Toggle Turbo" 버튼을 생성합니다.
    private void OnGUI()
    {
        if (GUILayout.Button("Turn On")) _engine.TurnOn();

        if (GUILayout.Button("Turn Off")) _engine.TurnOff();

        if (GUILayout.Button("Toggle Turbo")) _engine.ToggleTurbo();
    }
    #endregion
}

