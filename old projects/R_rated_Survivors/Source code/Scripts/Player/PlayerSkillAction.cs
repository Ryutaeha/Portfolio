using System.Collections;
using UnityEngine;

public enum ESkillName
{
    MageSpaceSkill,
    PaladinSkill,
    Max
}

public enum ESkillPosition
{
    Player,
    NullPosition
}

public class PlayerSkillAction : MonoBehaviour
{
    private PlayerCharacterController _controller;
    [SerializeField] private ESkillName spaceSkill = ESkillName.Max;
    [SerializeField] private ESkillPosition spaceSkillPosition = ESkillPosition.NullPosition;
    internal bool _isCoolDown = false;        //스킬이 쿨타임 상태인지  
    private float _coolDownTimer = 0f;       //스킬 쿨타임 타이머

    // 이 메서드는 객체가 깨어날 때 호출됩니다. 초기화 작업을 수행합니다.
    // 여기서는 PlayerCharacterController 컴포넌트를 가져옵니다.
    private void Awake()
    {
        _controller = GetComponent<PlayerCharacterController>();
    }

    // 이 메서드는 게임 시작 시 처음 한 번 호출됩니다.
    // 여기서는 OnSpaceSkillEvent 이벤트에 OnSpaceSkill 메서드를 구독하여
    // 이벤트가 발생할 때 호출되도록 합니다.
    void Start()
    {
        if (_controller != null) _controller.OnSpaceSkillEvent += OnSpaceSkill;
    }

    // 이 메서드는 스페이스 키 스킬 이벤트가 발생할 때 호출됩니다.
    // 스킬이 쿨타임 상태가 아니고 유효한 스킬이 설정되어 있을 경우,
    // 스킬을 사용하고 쿨타임 코루틴을 시작합니다.
    private void OnSpaceSkill()
    {
        if (spaceSkill != ESkillName.Max && !_isCoolDown)
        {
            StartCoroutine(SpaceCoolDown());
            GameObject skill = Managers.Resource.Instantiate(spaceSkill.ToString(), spaceSkillPosition == (ESkillPosition)1 ? null : gameObject.transform);
            if (skill.GetComponent<SpaceSkill>() != null) skill.GetComponent<SpaceSkill>().UseSkill(GetComponent<Transform>().position);
            GameObject.Find("GameUI").GetComponent<GameUI>().SkillCool();
        }
        else Debug.Log($"cooldown{_coolDownTimer}");
    }

    // 이 코루틴은 스페이스 키 스킬의 쿨타임을 처리합니다.
    // 쿨타임 동안 _isCoolDown을 true로 설정하고, 쿨타임이 끝나면 false로 설정합니다.
    private IEnumerator SpaceCoolDown()
    {
        _isCoolDown = true;
        _coolDownTimer = gameObject.GetComponent<PlayerAbility>().SkillWaiting;
        while (_coolDownTimer >= 0f)
        {
            _coolDownTimer -= Time.deltaTime;
            yield return null;
        }
        _coolDownTimer = 0f;
        _isCoolDown = false;
    }

    // 이 메서드는 스킬 이벤트를 설정하거나 해제합니다.
    // 매개변수 set이 true이면 OnSpaceSkillEvent 이벤트에 OnSpaceSkill 메서드를 구독하고,
    // false이면 구독을 취소합니다.
    public void SetSkill(bool set)
    {
        if (set) _controller.OnSpaceSkillEvent += OnSpaceSkill;
        else _controller.OnSpaceSkillEvent -= OnSpaceSkill;
    }

    // 이 메서드는 OnSpaceSkillEvent 이벤트에서 OnSpaceSkill 메서드의 구독을 취소합니다.
    public void SkillEventClear()
    {
        _controller.OnSpaceSkillEvent -= OnSpaceSkill;
    }
}
