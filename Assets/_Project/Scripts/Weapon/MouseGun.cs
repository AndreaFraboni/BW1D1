using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class MouseGun : Weapon
{
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] public Transform _firePoint;
    protected override void Shoot(Vector2 direction)
    {
        _lastShoot = Time.time;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 spawnPosition = _firePoint.position;
        Vector2 fireDirection = (mousePosition - spawnPosition).normalized;
        GameObject projectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        //rb.velocity = fireDirection * _projectileSpeed;

    }
}
