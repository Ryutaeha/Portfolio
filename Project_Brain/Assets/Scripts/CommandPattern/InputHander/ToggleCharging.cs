// ToggleCharging 클래스는 충전 상태를 토글하는 명령을 구현합니다.
public class ToggleCharging : Command
{
    #region Fields
    // 명령을 실행할 대상인 CommandTestController입니다.
    private CommandTestController _controller;
    #endregion

    #region Constructor
    // 생성자에서 CommandTestController 인스턴스를 받아 초기화합니다.
    public ToggleCharging(CommandTestController controller)
    {
        _controller = controller;
    }
    #endregion

    #region Methods
    // Execute 메서드는 충전 상태를 토글하는 명령을 실행합니다.
    public override void Execute()
    {
        _controller.ToggleCharging();
    }
    #endregion
}
