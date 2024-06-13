using Photon.Pun;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Dead,
}

public class Enemy : MonoBehaviour
{
    #region Properties

    // Data.
    public EnemyData Data;

    // Status.
    public string _key;
    public int hp;
    public float speed;
    public int damage;

    // State.
    public EnemyState State
    {
        get => _state;
        set
        {
            _state = value;
            switch (value)
            {
                case EnemyState.Idle: OnStateEntered_Idle(); break;
                case EnemyState.Dead: OnStateEntered_Dead(); break;
            }
        }
    }
    public int currentHp
    {
        get => _currentHp;
        set
        {
            if (value > hp) _currentHp = hp;
            else if (value <= 0)
            {
                _currentHp = 0;
                if (State != EnemyState.Dead)
                    State = EnemyState.Dead;
            }
            else _currentHp = value;
        }
    }

    private bool isRunning = true;

    #endregion

    #region Fileds
    // State
    private EnemyState _state;
    private int _currentHp;

    // Components
    private Transform target;
    private NavMeshAgent _agent;
    private SpriteRenderer _enemySpriteRenderer;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    protected Animator _animator;

    #endregion

    #region Initialize

    private void Initialize()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            _agent = gameObject.AddComponent<NavMeshAgent>();
        }
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    [PunRPC]
    public void SetInfo(string key)
    {
        Initialize();
        _agent = GetComponent<NavMeshAgent>();
        _key = key;
        EnemyData enemy = Main.DataManager.Enemies.FirstOrDefault(e => e.Key == key).Value;
        _enemySpriteRenderer.sprite = Main.ResourceManager.GetResource<Sprite>($"{key}.sprite");
        _animator.runtimeAnimatorController = Main.ResourceManager.GetResource<RuntimeAnimatorController>($"{key}.animController");
        hp = enemy.HP;
        currentHp = hp;
        speed = enemy.Speed;
        _agent.speed = speed;
        damage = enemy.Damage;
    }

    #endregion

    #region MonoBehaviour

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
                OnHit(projectile.Damage, projectile);

            if (projectile.IsValid()) Main.ObjectManager.Despawn(projectile);
        }
    }

    private void Start()
    {
        FindTarget();

        // 일정 시간마다 가장 가까운 플레이어를 찾아감
        StartCoroutine(UpdateTarget(5.0f));
    }

    private void Update()
    {
        if (target != null && _agent != null)
        {
            _agent.SetDestination(target.position);

            bool isTargetOnLeft = target.position.x < transform.position.x;
            _enemySpriteRenderer.flipX = isTargetOnLeft;
        }
    }

    private void OnDisable()
    {
        StopTargetUpdate();
    }

    #endregion

    #region State
    protected virtual void OnStateEntered_Idle() 
    { 

    }

    protected virtual void OnStateEntered_Dead()
    {
        Main.ObjectManager.Despawn(this);
    }

    private void OnHit(int damage, Projectile projectile)
    {
        currentHp -= damage;
        StartCoroutine(Knockback(projectile.transform.position));
    }

    private IEnumerator Knockback(Vector2 origin)
    {
        float elapsed = 0;
        while (elapsed < 0.1f)
        {
            elapsed += Time.deltaTime;

            Vector2 direction = (Vector2)this.transform.position - origin;
            Vector2 deltaPosition = direction.normalized * 10f * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + deltaPosition);

            yield return null;
        }
        yield break;
    }

    #endregion

    #region Movement
    private IEnumerator UpdateTarget(float interval)
    {
        while (isRunning)
        {
            yield return new WaitForSeconds(interval);
            FindTarget();
        }
    }


    public void StopTargetUpdate()
    {
        isRunning = false;
    }

    /// <summary>
    ///  플레이어가 여러명일때 가장 가까운 플레이어를 타겟팅
    /// </summary>
    private void FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            Transform closestPlayer = players[0].transform;
            float closestDistance = Vector3.Distance(transform.position, closestPlayer.position);

            for (int i = 1; i < players.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, players[i].transform.position);

                if (distance < closestDistance)
                {
                    closestPlayer = players[i].transform;
                    closestDistance = distance;
                }
            }

            target = closestPlayer;
        }
    }
    #endregion
}
