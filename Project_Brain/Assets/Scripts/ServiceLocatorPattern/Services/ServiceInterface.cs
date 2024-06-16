namespace ServiceInterface
{
    #region Interface
    // 로그를 기록하는 서비스를 위한 인터페이스입니다.
    public interface ILoggerService
    {
        public void Log(string message);
    }

    // 분석 이벤트를 전송하는 서비스를 위한 인터페이스입니다.
    public interface IAnalyticsService
    {
        public void SendEvent(string eventName);
    }

    // 광고를 표시하는 서비스를 위한 인터페이스입니다.
    public interface IAdvertisement
    {
        public void DisplayAd();
    }
    #endregion
}
