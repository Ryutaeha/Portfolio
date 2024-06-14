using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{

    #region Properties

    // 총알의 초기 위치
    private Vector2 _position;
    // 총알이 이동한 현재 거리
    private float _traveledDistance = 0;

    public int Damage { get; protected set; }

    public float Range { get; protected set; }

    #endregion

    #region fields

    private SpriteRenderer _spriter;
    private Rigidbody2D _rigidbody;

    [SerializeField] private bool _initialized;

    #endregion

    #region MonoBehaviours

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO 벽에 Wall 태그 추가해야됨
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("collision Wall");
            if (this.IsValid()) Main.ObjectManager.Despawn(this);
        }
    }

    private void Update()
    {
        // 이동한 거리 계산
        _traveledDistance = Vector2.Distance(_position, this.transform.position);

        // 총알이 이동한 거리가 사정거리를 초과하면
        if (_traveledDistance > Range)
        {
            if (this.IsValid()) Main.ObjectManager.Despawn(this);
        }
    }

    #endregion

    private void Initialize()
    {
        _position = this.transform.position;
        _spriter = this.GetComponentInChildren<SpriteRenderer>();
        _rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void SetSightRange(float range)
    {
        Range = range;
    }

    [PunRPC]
    public void SetInfo(string key, int damage)
    {
        Initialize();
        _spriter.sprite = Main.ResourceManager.GetResource<Sprite>($"{key}.sprite");
        
        Damage = damage;
    }
}