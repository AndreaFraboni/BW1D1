using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _weaponMountPoint;

    [SerializeField] GameObject _initialWeaponPrefab;

    private Weapon _currentWeapon;
    private GameObject _gameObjectWeapon;

    public float h, v;

    public Vector2 direction;
    public Vector2 _lastDirection = Vector2.down; // start with down orientation

    private Rigidbody2D _rb;
    private CircleCollider2D _collider2D;
    private AnimationParamHandler _animParam;

    private bool isWalking = false;
    public bool isAlive = true;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_collider2D == null) _collider2D = GetComponent<CircleCollider2D>();
        if (_animParam == null) _animParam = GetComponent<AnimationParamHandler>();

        //if (_initialWeaponPrefab) MountWeapon(_initialWeaponPrefab);
    }

    void Update()
    {
        if (isAlive) CheckInput();
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            Move();
            OrientWeapon();
        }
    }

    void CheckInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        direction = new Vector2(h, v);

        isWalking = direction != Vector2.zero;

        _animParam.SetBoolParam("isWalking", isWalking);

        if (isWalking)
        {
            _animParam.SetVerticalSpeedParam(v);
            _animParam.SetHorizontalSpeedParam(h);

            _lastDirection = direction.normalized;
        }

        if (direction.sqrMagnitude > 1f) direction = direction.normalized;
    }

    void Move()
    {
        _rb.MovePosition(_rb.position + direction * (_speed * Time.deltaTime));
    }

    public void OrientWeapon()
    {
        if (_gameObjectWeapon == null) return;
        _gameObjectWeapon.transform.up = _lastDirection;
    }

    public void GetItem(string itemName, int value)
    {
        LifeHandler _lifeHandler = GetComponentInParent<LifeHandler>();
        _lifeHandler.AddHp(value);
        Debug.Log($"{_lifeHandler.GetHp()}");
    }

    public void PlayerDeath()
    {
        isAlive = false;

        if (_collider2D != null) _collider2D.enabled = false;
        if (_rb != null) _rb.simulated = false;

        _animParam.SetBoolParam("isDying", true);
    }

    public void DestroyGOplayer()
    {
        Destroy(gameObject);
    }


    public void MountWeapon(GameObject _weaponPrefab)
    {
        if (_weaponPrefab == null)
        {
            Debug.LogError("Errore la weaponPrefab risulta essere null !!!!");
            return;
        }

        Weapon _weapon = _weaponPrefab.GetComponent<Weapon>();
        if (_weapon == null)
        {
            Debug.LogError("Il weaponPrefab del pickup NON risulta essere una Weapon !!!!");
            return;
        }

        if (_currentWeapon != null && _currentWeapon._weaponId == _weapon._weaponId)
        {
            Debug.Log("Stiamo montando la stessa arma che abbiamo adesso !!!!");
            //_currentWeapon increase weapon parameters
            return;
        }
        else
        {
            if (_gameObjectWeapon != null) Destroy(_gameObjectWeapon);
            _gameObjectWeapon = Instantiate(_weaponPrefab);
            _gameObjectWeapon.transform.SetParent(_weaponMountPoint, false);
            _currentWeapon = _gameObjectWeapon.GetComponent<Weapon>();
            _currentWeapon._playerController = this;
            OrientWeapon();
        }
    }

    public void GetCoins()
    {
        // TO DO 
    }

}
