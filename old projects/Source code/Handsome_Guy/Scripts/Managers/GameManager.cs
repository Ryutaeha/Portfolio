using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    
    public GameData Data => _data;
    public string NickName
    {
        get => _data.nickName;
        set => _data.nickName = value;
    }
    public string CharacterType
    {
        get => _data.characterType;
        set => _data.characterType = value;
    }

    public string WeaponType
    {
        get => _data.weaponType;
        set => _data.weaponType = value;
    }

    public string SkillType
    {
        get => _data.skillType;
        set => _data.skillType = value;
    }

    private GameData _data = new();

    #region Events

    public event Action<bool, float> RepairView;
    public event Action RepairCompleteEvent;
    public event Action UISetEvent;
    private float repairBar;

    public float RePairBar { get { return repairBar; } }


    
    #endregion



    public void RepairUI(bool view, float repairBar)
    {
        RepairView?.Invoke(view, repairBar);
    }
    public void RepairComplete()
    {
        Debug.Log("수리 완료");
        RepairCompleteEvent?.Invoke();
    }
    public void Repair(float repair)
    {
        this.repairBar = repair;
    }

    internal void UISet()
    {
        UISetEvent?.Invoke();
    }

    public void LeaveRoom()
    {
        Main.SceneManagerEx.LoadScene(Define.Scene.StartScene);
        PhotonNetwork.LeaveRoom();
    }

}
