
using UI;
using UnityEngine;

public class GameSceneUI : UI_Base
{


    #region Fields
    [SerializeField] GameObject clear;
    [SerializeField] GameObject over;
    private int brokenMachines;
    private int repairMachines=0;
    private bool repairCheck;



    public int _repairmachines { get { return repairMachines; } set { repairMachines = value; } }
    #endregion


    #region enums


    private enum Texts
    {
        Mission,
        MissionNum,
        Progress,
    }
    private enum Machines
    {
        Machine
    }
    private enum Buttons
    {
        ExitBtn1,
        ExitBtn2
    }
    #endregion

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindText(typeof(Texts), true);
        BindButton(typeof(Buttons), true);
        Main.GameManager.UISetEvent += UIset;
        Main.GameManager.RepairView += Repair;
        Main.GameManager.RepairCompleteEvent += RepairComplete;
        clear.SetActive(false);
        over.SetActive(false); 
        return true;
    }

    private void RepairComplete()
    {
        repairMachines++;
        UIset();
    }
 
    private void UIset()
    {
        brokenMachines = FindObjectsOfType<BrokenMachine>().Length;
        GetText((int)Texts.MissionNum).text = $"{brokenMachines} / {repairMachines}";
        if(brokenMachines == repairMachines)
        {
            GameOver(true);
        }
    }

    
    public void Repair(bool view, float repairBar)
    {
        repairCheck = view;
        
        if(!repairCheck)GetText((int)Texts.Progress).text = "";
    }

    private void Update()
    {
        if (repairCheck && Main.GameManager.RePairBar < 1) GetText((int)Texts.Progress).text = $"Progress : {(Main.GameManager.RePairBar * 100).ToString("N2")}%\r\nRefair - Press Hold E";
        else if (repairCheck && Main.GameManager.RePairBar >= 1) GetText((int)Texts.Progress).text = $"Progress :Complete";
    }
    private void GameOver(bool set)
    {
        if (set)
        {
            clear.SetActive(true);
            GetButton((int)Buttons.ExitBtn1).onClick.AddListener(BackLobby);
        }
        else
        {
            over.SetActive(true);
            GetButton((int)Buttons.ExitBtn2).onClick.AddListener(BackLobby);
        }

    }
    private void BackLobby()
    {
        Main.SceneManagerEx.LoadScene(Define.Scene.StartScene);
    }
}

