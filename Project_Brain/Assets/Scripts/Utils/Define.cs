

namespace Define
{
    // GameEventType 열거형은 게임 내에서 발생할 수 있는 다양한 Event Type을 정의합니다.
    public enum GameEventType
    {
        COUNTDOWN, START, RESTART, PAUSE, STOP, CLEAR, QUIT
    }

    public enum Direction
    {
        Left = -1,
        Right = 1
    }

    public enum SaveLocation
    {
        Local,
        Cloud,
        Both
    }
}

