using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject _projectilePrefab;

    [SerializeField] private float _fireRate = 3f;
    [SerializeField] protected float _fireRange = 5f;
    
    protected float _lastShoot = 0f;
    
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
    public virtual void Update()
    {

        if (IfShoot())
        {
            Shoot();
        }
    }
}
