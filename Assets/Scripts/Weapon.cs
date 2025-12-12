using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //[SerializeField] private int _damage = 1; //bullet?
    [SerializeField] private float _fireRate = 3f;
    [SerializeField] protected float _projectileSpeed = 4f; //bullet?
    [SerializeField] protected float _fireRange = 5f;
    [SerializeField] protected GameObject _projectilePrefab;
    protected float _lastShoot = 0f;
    // Start is called before the first frame update
    
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
