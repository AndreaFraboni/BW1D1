using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


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

    protected AnimationParamHandler _animParam;
    private CircleCollider2D _Collider2D;
    protected Rigidbody2D _rb;
    
    protected EnemyState _currentState = EnemyState.MOVE;
    protected int _health;
    // Start is called before the first frame update

    public bool isWalking = false;
    public bool isAlive = true;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); // Qua prendo il riferimento al RigidBody
        _Collider2D = GetComponent<CircleCollider2D>();
        _animParam = GetComponent<AnimationParamHandler>();
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

    protected void MoveWayPoint(Vector2 destination)
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;

        isWalking = direction != Vector2.zero;

        _animParam.SetBoolParam("isWalking", isWalking);

        if (isWalking)
        {
            _animParam.SetDirectionalSpeedParams(direction);
        }

        _rb.velocity = direction * _moveSpeed;
    }

    private void OnDisable()
    {
        //if (_enemiesManager != null)
        //{
        //    _enemiesManager.RemoveEnemy(this);
        //}
        DropLoot();
    }

    public void DestroyGOenemy()
    {
        Destroy(gameObject);
    }

}
