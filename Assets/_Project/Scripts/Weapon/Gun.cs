using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    protected override void Shoot(Vector2 direction)
    {
        _lastShoot = Time.time;

        //GameObject Target = FindNearestEnemy();
        //if (Target == null) return;

        //Vector2 targetPos = Target.GetComponent<Rigidbody2D>().position;
        //Vector2 spawnPosition = transform.position;        
        //Vector2 direction = (targetPos - spawnPosition).normalized;

        //if (_projectilePrefab != null)
        //{
        //    GameObject cloneBullet = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);  
        //    cloneBullet.gameObject.GetComponent<Bullet>().Shoot(direction);        
        //}
        //else
        //{
        //    Debug.Log("Non hai assegnato il Prefab del Bullet !!!");
        //    return;
        //}

        Vector2 _spawnPoint = _shootPoint.transform.position;
        GameObject cloneBullet = Instantiate(_projectilePrefab, _spawnPoint, Quaternion.identity);
        cloneBullet.gameObject.GetComponent<Bullet>().Shoot(direction);
    }

    public override void UpdateFireParams()
    {
        SetFireRange(_fireRangeUpValue);
        SetFireRate(_fireRateUpValue);
    }

}


