using Define;

// TurnLeft 클래스는 왼쪽으로 회전하는 명령을 구현합니다.
public class TurnLeft : Command
{
    #region Fields
    // 명령을 실행할 대상인 CommandTestController입니다.
    private CommandTestController _controller;
    #endregion

    #region Constructor
    // 생성자에서 CommandTestController 인스턴스를 받아 초기화합니다.
    public TurnLeft(CommandTestController controller)
    {
        _controller = controller;
    }
    #endregion

    #region Methods
    // Execute 메서드는 왼쪽으로 회전 명령을 실행합니다.
    public override void Execute()
    {
        _controller.Turn(Direction.Left);
    }
    #endregion
}
