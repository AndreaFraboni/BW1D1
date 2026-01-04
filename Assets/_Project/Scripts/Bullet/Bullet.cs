using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int bulletDamage = 1;
    [SerializeField] public float lifeSpan = 6f;
    [SerializeField] protected float _speed = 4f;

    public void SetBullet(int _bulletdamage, float _lifespan)
    {
        bulletDamage = _bulletdamage;
        lifeSpan = _lifespan;
    }

}
