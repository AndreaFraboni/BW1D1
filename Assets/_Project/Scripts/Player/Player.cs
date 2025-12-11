using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private AnimationParamHandler _animParam;
    private CircleCollider2D _Collider2D;

    private Rigidbody2D _rb;

    public float h, v;
    private Vector2 _movedir;

    private bool isWalking = false;

    private void Awake()
    {
        _animParam = GetComponent<AnimationParamHandler>();
        _Collider2D = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void GetItem(string itemName, int value)
    {
        LifeHandler _lifeHandler = GetComponentInParent<LifeHandler>();
        _lifeHandler.AddHp(value);
        Debug.Log($"{_lifeHandler.GetHp()}");
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
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
    }

    void FixedUpdate()
    {
        float sqrLenght = _movedir.sqrMagnitude;

        if (sqrLenght > 1)
        {
            _movedir = _movedir / Mathf.Sqrt(sqrLenght);
        }

        _rb.MovePosition(_rb.position + _movedir * (_speed * Time.deltaTime));
    }

    public void DestroyGOplayer()
    {
        Destroy(gameObject);
    }


}
