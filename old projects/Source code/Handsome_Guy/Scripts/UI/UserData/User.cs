using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Vivox;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class User : MonoBehaviourPunCallbacks , IPunObservable
{
    public VivoxParticipant Participant;
    private GameData _gameData = new();
    public GameData Data => _gameData;

    [SerializeField] Image User_Img;
    [SerializeField] TMP_Text user_name;
    [SerializeField] TMP_Text skill_name;
    public TMP_Text SpriteName;
    private PhotonView pv;
    public GameObject ready;
    private void Awake()
    {
        gameObject.transform.SetParent(GameObject.FindWithTag("Users").transform);
    }
    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }
    public void SetupItem(VivoxParticipant participant /*,string skill*/)
    {
        Participant = participant;
        user_name.text = "닉네임 : " + participant.DisplayName;
        skill_name.text = "스킬 : " + Main.GameManager.SkillType;
        SpriteName.text = Main.GameManager.CharacterType;
    }
    public void ToggleReady()
    {
        if(ready.activeSelf)
            ready.SetActive(false);
        else
            ready.SetActive(true);
    }

    public void SetImage()
    {
        User_Img.sprite = Main.ResourceManager.GetResource<Sprite>($"{SpriteName.text}.sprite");
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(user_name.text);
            stream.SendNext(skill_name.text);
            stream.SendNext(SpriteName.text);
            stream.SendNext(ready.activeSelf);
        }
        else
        {
            user_name.text = (string)stream.ReceiveNext();
            skill_name.text = (string)stream.ReceiveNext();
            SpriteName.text = (string)stream.ReceiveNext();
            ready.SetActive((bool)stream.ReceiveNext());
            SetImage();
        }
    }
}
