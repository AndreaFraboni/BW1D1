using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        IDLE,
        MOVE,
        PURSUIT
    }

    [SerializeField] private float _rangeEnemy = 7f;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected float _stopDistance = 0.1f;
    [SerializeField] protected int _maxHealth = 1;
    [SerializeField] protected float _dropChance = 0.15f;
    [SerializeField] protected GameObject _lootWeapon;
    [SerializeField] protected Transform _playerTarget;

    protected EnemiesManager _enemiesManager;

    protected AnimationParamHandler _animParam;

    protected Rigidbody2D _rb;
    private CircleCollider2D _Collider2D;

    protected EnemyState _currentState = EnemyState.MOVE;
    protected int _health;

    public bool isWalking = false;
    public bool isAlive = true;

    public Vector2 direction;

    protected virtual void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_Collider2D == null) _Collider2D = GetComponent<CircleCollider2D>();
        if (_animParam == null) _animParam = GetComponent<AnimationParamHandler>();

        _enemiesManager = FindObjectOfType<EnemiesManager>(); // prendo riferimento all'Enemies Manager in scena
        if (!_enemiesManager) Debug.LogError("EnemiesManager is not present in Scene !!!!");
    }

    private void OnEnable()
    {
        if (_enemiesManager != null)
        {
            _enemiesManager.RegistEnemy(this);
        }
        else
        {
            Debug.LogError("EnemiesManager is not present in scene !!!");
        }
    }

    private void OnDisable()
    {
        if (_enemiesManager != null)
        {
            _enemiesManager.RemoveEnemy(this);
        }
        // DropLoot();
    }

    protected virtual void Start()
    {
        if (_playerTarget == null)
        {
            GameObject playerObject = GameObject.FindWithTag(Tags.Player);
            if (playerObject != null)
            {
                _playerTarget = playerObject.transform;
            }
        }

        _health = _maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_playerTarget == null)
        {
            return;
        }

        CheckPlayer();

    }

    protected void CheckPlayer()
    {
        float distanceTarget = Vector2.Distance(transform.position, _playerTarget.position);

        if (distanceTarget <= _rangeEnemy)
        {
            _currentState = EnemyState.PURSUIT;
        }
        else if (_currentState == EnemyState.PURSUIT)
        {
            _currentState = EnemyState.MOVE;
        }
    }

    protected void MoveWayPoint(Vector2 destination)
    {
        direction = (destination - (Vector2)transform.position).normalized;
        isWalking = direction != Vector2.zero;
        _animParam.SetBoolParam("isWalking", isWalking);
        if (isWalking)
        {
            _animParam.SetDirectionalSpeedParams(direction);
        }
        _rb.velocity = direction * _moveSpeed;
    }

    protected virtual void FixedUpdate()
    {
        if (_playerTarget == null) return;

        switch (_currentState)
        {
            case EnemyState.PURSUIT:

                direction = (_playerTarget.position - transform.position).normalized;
                isWalking = direction != Vector2.zero;
                _animParam.SetBoolParam("isWalking", isWalking);
                if (isWalking)
                {
                    _animParam.SetDirectionalSpeedParams(direction);
                }
                _rb.velocity = direction * _moveSpeed;

                break;
            case EnemyState.MOVE:

                PatrolBehavior();

                break;
            case EnemyState.IDLE:

                _rb.velocity = Vector2.zero;

                break;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }

    }

    protected virtual void PatrolBehavior()
    {
        _rb.velocity = Vector2.zero;
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "è morto");

        if (_Collider2D != null) _Collider2D.enabled = false;
        if (_rb != null) _rb.simulated = false;

        _animParam.SetBoolParam("isDying", true);
    }

    protected void DropLoot()
    {
        int randomnumber = Random.Range(0, 100);

        if (randomnumber > 0 && randomnumber < 10)
        {
            // TO DO : aggiungere drop arMA !
            //if (Random.value <= _dropChance)
            //{
            //    Instantiate(_lootWeapon, transform.position, Quaternion.identity);
            //}
        }
        else
        {
            // TO DO : aggiungere drop energia
        }
    }

    public void EnemyDeath()
    {
        isAlive = false;

        if (_Collider2D != null) _Collider2D.enabled = false;
        if (_rb != null) _rb.simulated = false;

       _animParam.SetBoolParam("isDying", true);
    }

    public void DestroyGOenemy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag(Tags.Player))
            {
                _playerTarget.gameObject.GetComponent<Player>().PlayerDeath();
                DestroyGOenemy();
            }
        }
    }


}
