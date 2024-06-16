using Define;

// TurnRight 클래스는 오른쪽으로 회전하는 명령을 구현합니다.
public class TurnRight : Command
{
    #region Fields
    // 명령을 실행할 대상인 CommandTestController입니다.
    private CommandTestController _controller;
    #endregion

    #region Constructor
    // 생성자에서 CommandTestController 인스턴스를 받아 초기화합니다.
    public TurnRight(CommandTestController controller)
    {
        _controller = controller;
    }
    #endregion

    #region Methods
    // Execute 메서드는 오른쪽으로 회전 명령을 실행합니다.
    public override void Execute()
    {
        _controller.Turn(Direction.Right);
    }
    #endregion
}
