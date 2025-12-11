using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoubleGun : Weapon
{
   
    // Start is called before the first frame update
   protected override void Shoot()
    {
        _lastShoot = Time.time;
        Vector2 spawnPosition = transform.position;
        FireShoot( spawnPosition, Vector2.right);
        FireShoot( spawnPosition, Vector2.left);
    }
    private void FireShoot(Vector2 spawnPosition , Vector2 direction)
    {

        GameObject projectile = Instantiate (_projectilePrefab,spawnPosition,Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * _projectileSpeed;
    }
}
