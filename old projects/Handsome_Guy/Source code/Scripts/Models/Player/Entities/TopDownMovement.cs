using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownCharacterController _controller;
    private Player player;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private PhotonView pv;

    private void Awake()
    {
        player = GetComponent<Player>();
        _controller = GetComponent<TopDownCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _controller.onMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovent(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovent(Vector2 direction)
    {
        //if (pv.IsMine)
        //{
            direction *= player._moveSpeed; //Main.ObjectManager.Player._moveSpeed;
            _rigidbody.velocity = direction;
        //}
    }
}
