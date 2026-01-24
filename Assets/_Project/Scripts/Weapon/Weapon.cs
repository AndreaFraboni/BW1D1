using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public string _weaponId = "Set Weapon Id here !!!";
    [SerializeField] private float _fireRate = 3f;
    [SerializeField] protected float _fireRange = 5f;
    [SerializeField] protected float _fireRateUpValue = 0.1f;
    [SerializeField] protected float _fireRangeUpValue = 0.5f;

    public GameObject _projectilePrefab;
    public Transform _shootPoint;

    protected EnemiesManager _enemiesRegister;
    protected float _lastShoot = 0f;

    public Player _playerController;

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
    protected virtual void Shoot(Vector2 direction)
    {

    }
    public virtual void Update()
    {
        _playerController.OrientWeapon();

        if (IfShoot())
        {
            Shoot(_playerController._lastDirection);
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

    // setter    
    public void SetFireRate(float amount)
    {
        _fireRate = _fireRate - amount;
        if (_fireRate < 0.1f) _fireRate = 0.1f;
    }
    public void SetFireRange(float amount)
    {
        _fireRange = _fireRange + amount;
        if (_fireRange >= 10) _fireRange = 10;
    }

    public virtual void UpdateFireParams()
    {


    }


}
