using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIPopup_Nickname : UI_Popup
{

    #region Enums
    private enum Objects
    {
        PopupNicknameScale,
        InputField,
    }
    private enum Buttons
    {
        OKButton,
    }

    #endregion

    #region Fields

    private TMP_InputField _inputField;
    private string _nickname;
    GameObject scaler;
    #endregion
    private void Start()
    {
        Init();
    }
    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons),true);
        BindObject(typeof(Objects),true);

        _inputField = GetObject((int)Objects.InputField).GetComponent<TMP_InputField>();
        scaler = GetObject((int)Objects.PopupNicknameScale);

        AddUIEvent(GetButton((int)Buttons.OKButton).gameObject, OnButtonOK);

        Main.UIManager.Appear(scaler);
        return true;
    }
    private void OnButtonOK(PointerEventData data)
    {
        _nickname = _inputField.text;
        if(_nickname == "") return;
        
        Main.GameManager.NickName = _nickname;
        print($"닉네임 : {_nickname}");

        StartCoroutine(CoDelay());
    }

    IEnumerator CoDelay()
    {
        Main.UIManager.Hide(scaler);
        yield return new WaitForSeconds(1f);
        Main.UIManager.ShowPopupUI<UIPopup_SelectCharacter>();
    }
}
