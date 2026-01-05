using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownGun : Weapon
{
    [SerializeField] private GameObject _projectilePrefab;

    protected override void Shoot()
    {
        _lastShoot = Time.time;

        Vector2 spanwPosition = transform.position;
        Vector2 fireDirection = Vector2.down;
        
        GameObject projectile = Instantiate(_projectilePrefab, spanwPosition, Quaternion.identity);
        
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
       
        
        // rb.velocity = fireDirection * _projectileSpeed;


    }

}   

