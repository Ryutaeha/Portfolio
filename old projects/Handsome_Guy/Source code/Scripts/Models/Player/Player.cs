using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Define;

public class Player : MonoBehaviourPunCallbacks
{
    #region Properties

    public PlayerData playerData { get; private set; }
    public string _id { get; private set; }
    public float _currentHp { get; private set; }
    public float _maxHp { get; private set; }
    public float _moveSpeed { get; private set; }
    public float _damage { get; private set; }
    public float _attackSpeed { get; private set; }
    public float _damageReduceRatio { get; private set; }
    public float _sightRange { get; private set; }
    public int _killCount { get; private set; }


    #endregion

    #region Fileds

    //private SpriteRenderer _weaponSprite;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    //protected Animator _animator;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Image HPBar;
    [SerializeField] private TMP_Text NickName;
    private PhotonView _pv;
    private bool _isInvincible;
    private float _invincibleTime = 1.0f;

    #endregion

    #region Initialize

    private void Initialize()
    {
        // _weaponSprite = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _pv = GetComponent<PhotonView>();
        //_animator = GetComponent<Animator>();
    }

    public void SetInfo()
    {
        Initialize();
        playerData = Main.DataManager.Player;
        //_weaponSprite.sprite = Main.ResourceManager.GetResource<Sprite>($"{_weaponSprite.sprite.name}.sprite");
        //_animator.runtimeAnimatorController = Main.ResourceManager.GetResource<RuntimeAnimatorController>($"{key}.animController");
        //_id = playerData.id;
        _currentHp = playerData.currrentHp;
        _maxHp = playerData.maxHp;
        _moveSpeed = playerData.moveSpeed;
        _damage = playerData.damage;
        _attackSpeed = playerData.attackSpeed;
        _damageReduceRatio = playerData.damageReduceRatio;
        _sightRange = playerData.sightRange;
        _killCount = playerData.killCount;
    }

    [PunRPC]
    public void SetSprite(string keyname)
    {
        spriteRenderer.sprite = Main.ResourceManager.GetResource<Sprite>(keyname);
    }
    [PunRPC]
    public void SetName(string name)
    {
        NickName.text = name;
    }

    #endregion

    #region MonoBehaviour
    private void Start()
    {
        GetComponent<PlayerStatusController>().dieEvent += OnDiePlayer;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy= collision.gameObject.GetComponent<Enemy>();
            if (enemy != null && !_isInvincible)
            {
                OnHit(enemy, enemy.damage);
            }
        }
    }

    #endregion

    private IEnumerator HitWithDelay(float delay)
    {
        _isInvincible = true;

        yield return new WaitForSeconds(delay);

        _isInvincible = false;
    }

    public void OnHit(Enemy enemy, int damage)
    {
        
        _currentHp -= damage;
        if(_pv.IsMine)
        {
            _pv.RPC("SetHPBar", RpcTarget.All);
            if (_currentHp > 0)
            {
                StartCoroutine(HitWithDelay(_invincibleTime));
            }
        }
    }
    [PunRPC]
    public void SetHPBar()
    {
        if (_currentHp <= 0)
        {
            GetComponent<PlayerStatusController>().CallDieEvent();
        }
        HPBar.fillAmount = _currentHp / _maxHp;
    }

    private void OnDiePlayer()
    {
        Main.GameManager.LeaveRoom();
    }

    #region ChangeMethod

    public void ChangeWeapon(string weaponName)
    {

    }

    public void ChangeId(string newId)
    {
        _id = newId;
    }

    public void ChangeCurrentHp(float newCurrentHp)
    {
        _currentHp = newCurrentHp;
    }

    public void ChangeMaxHp(float newMaxHp)
    {
        _maxHp = newMaxHp;
    }

    public void ChangeMoveSpeed(float newMoveSpeed)
    {
        _moveSpeed = newMoveSpeed;
    }

    public void ChangeDamage(float newDamage)
    {
        _damage = newDamage;
    }

    public void ChangeAttackSpeed(float newAttackSpeed)
    {
        _attackSpeed = newAttackSpeed;
    }

    public void ChangeDamageReduceRatio(float newDamageReduceRatio)
    {
        _damageReduceRatio = newDamageReduceRatio;
    }

    public void ChangeSightRange(float newSightRange)
    {
        _sightRange = newSightRange;
    }

    public void ChangeKillCount(int newKillCount)
    {
        _killCount = newKillCount;
    }
    #endregion

}
