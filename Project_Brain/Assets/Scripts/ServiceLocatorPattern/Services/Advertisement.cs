using ServiceInterface;
using UnityEngine;

public class Advertisement : IAdvertisement
{
    #region Methods
    // IAdvertisement 인터페이스의 DisplayAd 메서드를 구현한 것입니다.
    // 광고를 표시하는 기능을 Unity의 디버그 로그에 출력합니다.
    public void DisplayAd()
    {
        Debug.Log("Displaying video advertisement");
    }
    #endregion
}
