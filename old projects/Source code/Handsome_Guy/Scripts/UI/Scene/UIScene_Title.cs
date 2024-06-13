using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIScene_Title : UI_Scene
{
    #region enums
    private enum Scales
    {
        TitleScale,
    }
    private enum Buttons
    {
        StartButton,
        ExitButton
    }
    private enum Images
    {
        TitleLogo
    }
    #endregion

    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons),true);
        BindImage(typeof(Images),true);
        BindObject(typeof(Scales));

        AddUIEvent(GetButton((int)Buttons.StartButton).gameObject, OnButtonStart);
        AddUIEvent(GetButton((int)Buttons.ExitButton).gameObject, OnButtonExit);

        return true;
    }
    private void OnButtonStart(PointerEventData data)
    {
        print("시작버튼 클릭됨");

        Main.UIManager.Hide(GetObject((int)Scales.TitleScale).gameObject);
        StartCoroutine(CoDelay());
        //Main.UIManager.CloseAllPopupUI();
        //Main.SceneManager.LoadScene("MainScene");
    }
    private void OnButtonExit(PointerEventData data)
    {
        print("나가기버튼 클릭됨");
        Application.Quit();
    }
    IEnumerator CoDelay()
    {
        yield return new WaitForSeconds(1f);
        Main.UIManager.ShowPopupUI<UIPopup_Nickname>();
    }
}
