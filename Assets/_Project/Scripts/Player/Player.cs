using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private Rigidbody2D _rb;

    public float h, v;

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
    }

    void FixedUpdate()
    {
        Vector2 dir = new Vector2(h, v);

        float sqrLenght = dir.sqrMagnitude;

        if (sqrLenght > 1)
        {
            dir = dir / Mathf.Sqrt(sqrLenght);
        }

        _rb.MovePosition(_rb.position + dir * (_speed * Time.deltaTime));
    }
}
