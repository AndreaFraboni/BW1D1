using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private AnimationParamHandler _animParam;
    private CircleCollider2D _collider2D;

    private Rigidbody2D _rb;

    public float h, v;
    private Vector2 _movedir;

    private bool isWalking = false;
    public bool isAlive = true;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_collider2D == null) _collider2D = GetComponent<CircleCollider2D>();
        if (_animParam == null) _animParam = GetComponent<AnimationParamHandler>();
    }

    void Update()
    {
        if (isAlive) CheckInput();
    }

    void FixedUpdate()
    {
        if (isAlive) Move();
    }

    void CheckInput()
    {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            _movedir = new Vector2(h, v);

            isWalking = _movedir != Vector2.zero;

            _animParam.SetBoolParam("isWalking", isWalking);

            if (isWalking)
            {
                _animParam.SetVerticalSpeedParam(v);
                _animParam.SetHorizontalSpeedParam(h);
            }

            if (_movedir.sqrMagnitude > 1f) _movedir = _movedir.normalized;
    }

    void Move()
    {
        _rb.MovePosition(_rb.position + _movedir * (_speed * Time.deltaTime));
    }

    public void GetItem(string itemName, int value)
    {
        LifeHandler _lifeHandler = GetComponentInParent<LifeHandler>();
        _lifeHandler.AddHp(value);
        Debug.Log($"{_lifeHandler.GetHp()}");
    }

    public void DestroyGOplayer()
    {
        Destroy(gameObject);
    }


}
