using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
   protected override void Shoot()
    {
        _lastShoot = Time.time;
        
        Vector2 spanwPosition = transform.position;
        
        Vector2 fireDirection = Vector2.up;
        
        GameObject projectile = Instantiate (_projectilePrefab , spanwPosition ,  Quaternion.identity);
    }

}   
    

