using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public enum EnemyState {IDLE , MOVE , PURSUIT}
    [SerializeField] private float _rangeEnemy = 7f;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected float _stopDistance = 0.1f;
    [SerializeField] protected int _maxHealth = 1;
    [SerializeField] protected float _dropChance = 0.15f;
    [SerializeField] protected GameObject _lootWeapon;
    [SerializeField] protected Transform _playerTarget;
    protected Rigidbody2D _rb;
    protected EnemyState _currentState = EnemyState.MOVE;
    protected int _health;
    // Start is called before the first frame update
    
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); // Qua prendo il riferimento al RigidBody
    }
    protected virtual void Start()
    {
        //Qua trovo la posizion del Player
        if ( _playerTarget == null )
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if ( playerObject != null )
            {
                _playerTarget = playerObject.transform;
            }
        }
        _health = _maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_playerTarget == null ) return;
        CheckPlayer();
    }
    protected virtual void FixedUpdate()
    {
        if (_playerTarget == null) return;
        switch (_currentState)
        {
            case EnemyState.PURSUIT:
                Vector2 direction = (_playerTarget.position - transform.position).normalized;
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

    protected void TakeDamage(int damage)
    {
        if (_health <= 0) return;
        
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
        
    }
    protected void CheckPlayer()
    {
        float distanceTarget = Vector2.Distance (transform.position, _playerTarget.position);
        if ( distanceTarget <= _rangeEnemy )
        {
            _currentState = EnemyState.PURSUIT;
        }
        else if (_currentState == EnemyState.PURSUIT)
        {
            _currentState = EnemyState.MOVE;
        }
    } 
    protected virtual void PatrolBehavior()
    {
        _rb.velocity = Vector2.zero;
    }

        
  
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);// L'ho messo qui, ma creo che volendo lo si può distruggere direttamente dallo Script Projectile
            TakeDamage(1);
        }
    }
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "è morto");
        Destroy(gameObject);
    }

    protected void DropLoot()
    {
        if (Random.value <= _dropChance)
        {
            Instantiate (_lootWeapon , transform.position, Quaternion.identity) ;
        }
    }

    protected void MoveWayPoint(Vector2 destination)
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        _rb.velocity = direction * _moveSpeed;
    }
}
