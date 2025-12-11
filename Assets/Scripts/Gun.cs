using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Shoot()
    {
        _lastShoot = Time.time;
        Vector2 spanwPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;
        GameObject projectile = Instantiate (_projectilePrefab , spanwPosition ,  spawnRotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        
    }
}
