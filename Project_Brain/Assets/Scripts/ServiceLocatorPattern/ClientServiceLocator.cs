using ServiceInterface;
using UnityEngine;

public class ClientServiceLocator : MonoBehaviour
{
    #region Unity Methods
    // 이 메서드에서 서비스를 등록합니다.
    private void Start()
    {
        RegisterService();
    }

    // 여기서 버튼을 클릭했을 때의 동작을 정의합니다.
    private void OnGUI()
    {
        GUILayout.Label("Review output in the console");

        if (GUILayout.Button("Log Event"))
        {
            ILoggerService logger = ServiceLocator.GetService<ILoggerService>();
            logger.Log("LOG!");
        }

        if (GUILayout.Button("Send Analytics"))
        {
            IAnalyticsService analytics = ServiceLocator.GetService<IAnalyticsService>();
            analytics.SendEvent("Analytics");
        }

        if (GUILayout.Button("Display Advertisement"))
        {
            IAdvertisement advertisement = ServiceLocator.GetService<IAdvertisement>();
            advertisement.DisplayAd();
        }
    }
    #endregion

    #region Methods
    // 각종 서비스를 등록하는 메서드입니다.
    private void RegisterService()
    {
        ILoggerService logger = new Logger();
        ServiceLocator.RegisterService(logger);

        IAnalyticsService analytics = new Analytics();
        ServiceLocator.RegisterService(analytics);

        IAdvertisement advertisement = new Advertisement();
        ServiceLocator.RegisterService(advertisement);
    }
    #endregion
}
