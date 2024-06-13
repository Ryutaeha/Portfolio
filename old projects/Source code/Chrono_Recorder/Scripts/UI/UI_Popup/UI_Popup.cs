using UI;

/// UI_Popup은 UIBase를 상속받는 클래스입니다. 이 클래스는 팝업 UI 요소를 초기화하고 설정하는 데 사용됩니다.
public class UI_Popup : UI_Base
{
    /// 이 메서드는 UI_Popup을 초기화합니다. 부모 클래스의 Init 메서드를 호출한 후, 
    /// Main.UI.SetCanvas 메서드를 사용하여 팝업의 캔버스를 활성화합니다.
    /// 초기화가 성공하면 true, 실패하면 false를 반환합니다.
    protected override bool Init()
    {
        if (!base.Init()) return false;

        Main.UI.SetCanvas(gameObject, true);

        return true;
    }

    /// 버튼 이벤트를 설정하는 가상 메서드입니다. 
    /// 자식 클래스에서 이 메서드를 오버라이드하여 버튼 이벤트를 설정합니다.
    protected virtual void SetBtnEvent() { }
}