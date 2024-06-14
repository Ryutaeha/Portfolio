using System;
using static GameManager;
using UnityEngine;

public class PlayerAbility : MonoBehaviour, ICharacter, IPlayer
{
    private int _ad;
    private int _as;
    private int _scale;

    public int HP { get; private set; }
    public int MaxHP { get; private set; }
    public int AD { get { return _ad; } set { _ad = value; OnADEvent?.Invoke(value); } }
    public int AS { get { return _as; } set { _as = value; OnASEvent?.Invoke(value); } }
    public int Scale { get { return _scale; } set { _scale = value; OnScaleEvent?.Invoke(value); } }
    public float Speed { get; private set; }
    public int DF { get; private set; }
    public int Exp { get; private set; }
    public bool IsDead => HP <= 0;
    public int NextExp { get; private set; }
    public int SkillWaiting { get; private set; }
    public int Level { get; private set; }

    public event Action<int> OnADEvent;
    public event Action<int> OnASEvent;
    public event Action<int> OnScaleEvent;

    PlayerMovement playerMovement;
    PlayerAimRotation playerAimRotation;
    PlayerSkillAction playerSkillAction;
    public Animator _animator;

    // 무적 상태를 나타내는 변수입니다.
    public bool invincibility = false;

    // 무적 지속 시간을 나타내는 변수입니다.
    float invincibilityTimeSet = 1.0f;

    // 체력 및 마나바 업데이트를 위한 이벤트입니다.
    public event Action<int, int> UpdateHPBar;
    public event Action<int, int> UpdateEXPBar;

    // 레벨업 시 사용할 이벤트입니다.
    public event Action<int> LevelUpUI;
    public event Action<int> LevelUp;
    public event Action IsDeadUI;

    // 초기화 작업을 수행합니다.
    void Awake()
    {
        PlayerSetting();
        playerMovement = GetComponent<PlayerMovement>();
        playerAimRotation = GetComponent<PlayerAimRotation>();
        playerSkillAction = GetComponent<PlayerSkillAction>();
        _animator = GetComponent<Animator>();
    }

    // 캐릭터 생성 시 초기 설정을 합니다.
    void PlayerSetting()
    {
        JobSettings jobSettings = JsonSetting.Instance.AbilitySettings(gameObject.name);
        if (jobSettings != null)
        {
            HP = jobSettings.HP;
            MaxHP = jobSettings.MaxHP;
            AD = jobSettings.AD;
            Speed = jobSettings.Speed;
            DF = jobSettings.DF;
            Exp = jobSettings.Exp;
            AS = jobSettings.AS;
            NextExp = jobSettings.NextExp;
            SkillWaiting = jobSettings.SkillWaiting;
            Level = jobSettings.Level;
        }
    }

    // 능력치를 증가시키는 메서드입니다.
    public void AbilityUp(int select)
    {
        AbilityUpJson(select);
        UpdateHPBar?.Invoke(HP, MaxHP);
    }

    // 레벨업을 처리하는 메서드입니다.
    public void LevelUP()
    {
        Level++;
        Exp = 0;
        AddExp();
        LevelUp?.Invoke(Level);
        Managers.Sound.SoundPlay("SFX/levelup", SoundType.Effect);
    }

    // JSON 데이터를 이용해 능력치를 증가시키는 메서드입니다.
    void AbilityUpJson(int select)
    {
        JobAbility jobAbility = JsonSetting.Instance.AbilityUP(gameObject.name);
        if (jobAbility != null)
        {
            switch (select)
            {
                case 1:
                    MaxHP += jobAbility.MaxHP;
                    break;
                case 2:
                    AD += jobAbility.AD;
                    break;
                case 3:
                    Speed += jobAbility.Speed;
                    break;
                case 4:
                    AS += jobAbility.AS;
                    break;
            }
            RecoveryHP(jobAbility.MaxHP);
            DF += jobAbility.DF;
        }
    }

    // 레벨업 시 경험치를 증가시키는 메서드입니다.
    void AddExp()
    {
        JobAbility jobAbility = JsonSetting.Instance.AbilityUP(gameObject.name);
        if (jobAbility != null) NextExp += jobAbility.NextExp * Level;
    }

    // 캐릭터가 피해를 입었을 때 호출되는 메서드입니다.
    public void CharacterHit(int damage)
    {
        if (!invincibility)
        {
            HP -= damage;
            if (HP < 0) HP = 0;
            UpdateHPBar?.Invoke(HP, MaxHP);
            invincibilityTime();
        }
        if (IsDead)
        {
            PlayerSet(false);
            _animator.SetTrigger("Die");
            IsDeadUI?.Invoke();
        }
    }

    // 무적 상태를 설정하는 메서드입니다.
    void invincibilityTime()
    {
        if (invincibility)
        {
            invincibility = false;
        }
        else
        {
            invincibility = true;
            Invoke("invincibilityTime", invincibilityTimeSet);
        }
    }

    // 체력을 회복하는 메서드입니다.
    public void RecoveryHP(int recovery)
    {
        HP += recovery;
        if (HP > MaxHP) HP = MaxHP;
        UpdateHPBar?.Invoke(HP, MaxHP);
    }

    // 경험치를 획득하는 메서드입니다.
    public void ExpGet(int exp)
    {
        Exp += exp;
        if (Exp >= NextExp)
        {
            LevelUP();
            LevelUpUI?.Invoke(3);
        }
        UpdateEXPBar?.Invoke(Exp, NextExp);
    }

    // 캐릭터의 움직임을 멈추는 메서드입니다.
    public void PlayerSet(bool set)
    {
        playerMovement.SetMove(set);
        playerAimRotation.SetAim(set);
        playerSkillAction.SetSkill(set);
    }

    // 리소스를 해제하는 메서드입니다.
    public void PlayerSetClear()
    {
        playerMovement.MoveEventClear();
        playerAimRotation.AimEventClear();
        playerSkillAction.SkillEventClear();
        OnADEvent = null;
        OnASEvent = null;
        OnScaleEvent = null;
        UpdateHPBar = null;
        UpdateEXPBar = null;
        LevelUpUI = null;
        LevelUp = null;
        IsDeadUI = null;
    }
}
