using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownCharacterController _controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 _aimDirection = Vector2.zero;
    private PhotonView _pv;
    private Player _player;

    public GameObject testPrefab;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _controller = GetComponent<TopDownCharacterController>();
        _pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _controller.onAttackEvent += OnShoot;
        _controller.onLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        if (_pv.IsMine)
        {
            _aimDirection = newAimDirection;
        }
    }

    private void OnShoot()
    {
        if (_pv.IsMine)
        {
            _pv.RPC("CreateProjectile", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void CreateProjectile()
    {
        // 발사체의 Sprite 회전
        float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Projectile projectile = Main.ObjectManager.Spawn<Projectile>("Gunbullet", projectileSpawnPosition.position);
        projectile.SetInfo("Gunbullet", (int)_player._damage);
        projectile.transform.rotation = rotation;
        projectile.SetVelocity(_aimDirection * _player._attackSpeed);
        projectile.SetSightRange(_player._sightRange);
        projectile.gameObject.tag = "PlayerProjectile";
    }
}
