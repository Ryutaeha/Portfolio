using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;
using Data;
using TMPro;
using UnityEngine.UI;

public class UIPopup_SelectSkill : UI_Popup
{
    #region Enums
    private enum Objects
    {
        PopupSelectSkillScale,
    }
    private enum Buttons
    {
        Skill_1,
        Skill_2,
        Skill_3,
    }
    private enum Images
    {
        Skill1_Icon,
        Skill2_Icon,
        Skill3_Icon,
    }
    private enum Texts
    {
        Skill1_Name,
        Skill2_Name,
        Skill3_Name,
        Skill1_Desc,
        Skill2_Desc,
        Skill3_Desc,
    }

    #endregion

    #region Fields
    private GameObject scaler;
    private int rand;
    private List<TextMeshProUGUI> _skill = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> _desc = new List<TextMeshProUGUI>();
    private List<Image> _skillImage = new List<Image>();
    private List<int> selectedSkillIdx = new List<int>();

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
        BindText(typeof(Texts), true);
        BindImage(typeof(Images), true);

        //스킬명과 스킬설명 리스트로 받아두기
        _skill.Add(GetText((int)Texts.Skill1_Name));
        _skill.Add(GetText((int)Texts.Skill2_Name));
        _skill.Add(GetText((int)Texts.Skill3_Name));
        _skillImage.Add(GetImage((int)Images.Skill1_Icon));
        _skillImage.Add(GetImage((int)Images.Skill2_Icon));
        _skillImage.Add(GetImage((int)Images.Skill3_Icon));
        _desc.Add(GetText((int)Texts.Skill1_Desc));
        _desc.Add(GetText((int)Texts.Skill2_Desc));
        _desc.Add(GetText((int)Texts.Skill3_Desc));

        //선택된 스킬이 3개가 될 때 까지 랜덤으로 인덱스 생성하여 받기 (중복X)
        while (selectedSkillIdx.Count < 3)
        {
            rand = Random.Range(0, Main.DataManager.SkillDict.Count);
            if(!selectedSkillIdx.Contains(rand))
                selectedSkillIdx.Add(rand);
        }

        //스킬명과 스킬 설명 텍스트에 뽑힌 스킬정보 넣기
        for(int i = 0; i < selectedSkillIdx.Count; i++)
        {
            _skill[i].text = Main.DataManager.SkillDict.ElementAt(selectedSkillIdx[i]).Key;
            _desc[i].text = Main.DataManager.SkillDict.ElementAt(selectedSkillIdx[i]).Value.description;
            _skillImage[i].sprite = Main.DataManager.SkillDict.ElementAt(selectedSkillIdx[i]).Value.sprite;
        }

        scaler = GetObject((int)Objects.PopupSelectSkillScale);

        AddUIEvent(GetButton((int)Buttons.Skill_1).gameObject, OnButtonSkill);
        AddUIEvent(GetButton((int)Buttons.Skill_2).gameObject, OnButtonSkill);
        AddUIEvent(GetButton((int)Buttons.Skill_3).gameObject, OnButtonSkill);


        Main.UIManager.Appear(scaler);
        return true;
    }

    private void OnButtonSkill(PointerEventData data)
    {
        string selectedButton = data.selectedObject.gameObject.name;
        
        switch (selectedButton)
        {
            case "Skill_1":
                Main.GameManager.SkillType = _skill[0].text;
                break;
            case "Skill_2":
                Main.GameManager.SkillType = _skill[1].text;
                break;
            case "Skill_3":
                Main.GameManager.SkillType = _skill[2].text;
                break;
        }
        print(Main.GameManager.SkillType);
        StartCoroutine(CoButtonSkill());
    }
    IEnumerator CoButtonSkill()
    {
        Main.UIManager.Hide(scaler);
        yield return new WaitForSeconds(1f);
        Main.SceneManagerEx.LoadScene(Define.Scene.LobbyScene);
    }
}
