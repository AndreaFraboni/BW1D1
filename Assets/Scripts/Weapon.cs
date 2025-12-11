using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _fireRate = 3f;
    [SerializeField] protected GameObject _projectilePrefab;
    protected float _lastShoot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IfShoot())
        {
            Shoot();
        }
    }

    protected bool IfShoot()
    {
        float shootTime = _lastShoot + _fireRate;
        if (Time.time >= shootTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected virtual void Shoot()
    {

    }
}
