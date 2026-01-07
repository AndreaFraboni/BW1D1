using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 10;
    [SerializeField] private float lifeSpan = 6f;
    [SerializeField] private float speed = 5f;

    private Rigidbody2D _rb;
    private Vector2 direction;

    public void SetBullet(int _bulletdamage, float _lifespan)
    {
        bulletDamage = _bulletdamage;
        lifeSpan = _lifespan;
    }

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction * (speed * Time.deltaTime));
    }

    public void Shoot(Vector2 dir)
    {
        float sqrMagnitude = dir.sqrMagnitude;
        if (sqrMagnitude > 1)
        {
            dir /= Mathf.Sqrt(sqrMagnitude);
        }
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<LifeHandler>(out LifeHandler _LifeHandler))
        {
            _LifeHandler.TakeDamage(bulletDamage); // ok ho trovato un LifeHandler quindi gli procuro danno 
            Destroy(gameObject);                   // e poi mi distruggo !!!
        }

        Destroy(gameObject); // l'oggetto colpito non ha un Lifehandler quindi mi distruggo soltanto !!!
    }
}
