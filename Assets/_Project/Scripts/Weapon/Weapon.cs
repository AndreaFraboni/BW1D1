using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public string _weaponId = "Set Weapon Id here !!!";

    [SerializeField] private float _fireRate = 3f;
    [SerializeField] protected float _fireRange = 5f;

    protected EnemiesManager _enemiesRegister;

    protected float _lastShoot = 0f;

    protected virtual void Awake()
    {
        if (_enemiesRegister == null)
        {
            _enemiesRegister = FindObjectOfType<EnemiesManager>();
            if (_enemiesRegister == null)
            {
                Debug.LogError("EnemiesManager NON sta in scena !!!");
            }
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
    public virtual void Update()
    {

        if (IfShoot())
        {
            Shoot();
        }
    }

    protected GameObject FindNearestEnemy()
    {
        GameObject NearstEnemyFounded = null;

        float nearstDistance = _fireRange;

        foreach (Enemy currentEnemy in _enemiesRegister.listEnemies)
        {
            float CurDistance = Vector2.Distance(transform.position, currentEnemy.transform.position);
            if (CurDistance < nearstDistance)
            {
                nearstDistance = CurDistance;
                NearstEnemyFounded = currentEnemy.gameObject;
            }
        }
        return NearstEnemyFounded;
    }


}
