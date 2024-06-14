using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIPopup_SelectCharacter : UI_Popup
{
    #region Enums
    private enum Objects
    {
        UIPopup_SelectCharacterScale,
    }
    private enum Buttons
    {
        CharacterA,
        CharacterB,
        CharacterC,
        CharacterD,
        CharacterE,
        OKButton
    }

    #endregion

    #region Fields
    private GameObject scaler;
    private GameObject okButton;
    private string _characterType;

    #endregion
    void Start()
    {
        Init();
    }
    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons), true);
        BindObject(typeof(Objects), true);

        scaler = GetObject((int)Objects.UIPopup_SelectCharacterScale);
        okButton = GetButton((int)Buttons.OKButton).gameObject;
        okButton.SetActive(false);
        AddUIEvent(okButton, OnButtonOK);
        AddUIEvent(GetButton((int)Buttons.CharacterA).gameObject, OnButtonCharacter);
        AddUIEvent(GetButton((int)Buttons.CharacterB).gameObject, OnButtonCharacter);
        AddUIEvent(GetButton((int)Buttons.CharacterC).gameObject, OnButtonCharacter);
        AddUIEvent(GetButton((int)Buttons.CharacterD).gameObject, OnButtonCharacter);
        AddUIEvent(GetButton((int)Buttons.CharacterE).gameObject, OnButtonCharacter);

        Main.UIManager.Appear(scaler);
        return true;
    }

    private void OnButtonCharacter(PointerEventData data)
    {
        okButton.SetActive(true);

        _characterType = data.selectedObject.gameObject.name;
        print(data.selectedObject.gameObject.name);
    }

    private void OnButtonOK(PointerEventData data)
    {
        Main.GameManager.CharacterType = _characterType;
        StartCoroutine(CoButtonOK());
    }
    IEnumerator CoButtonOK()
    {
        Main.UIManager.Hide(scaler);
        yield return new WaitForSeconds(1f);
        Main.UIManager.ShowPopupUI<UIPopup_SelectSkill>();
    }
}
