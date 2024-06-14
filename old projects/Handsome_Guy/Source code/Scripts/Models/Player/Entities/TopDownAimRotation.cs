using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private Transform armPivot;

    private TopDownCharacterController _controller;
    private PhotonView _pv;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _controller.onLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        if (_pv.IsMine)
        {
            RotateArm(newAimDirection);
        }
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
