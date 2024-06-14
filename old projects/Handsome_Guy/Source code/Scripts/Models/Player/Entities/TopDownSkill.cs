using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownSkill : MonoBehaviour
{
    private TopDownCharacterController _controller;
    
    private PhotonView _pv;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        _controller.onSkillEvent += OnSkill;
    }

    private void OnSkill()
    {
        BaseActive baseActive = GetComponent<BaseActive>();
        if (baseActive == null) return;

        if (_pv.IsMine)
        {
            baseActive.UseSkill();
        }
    }
}
