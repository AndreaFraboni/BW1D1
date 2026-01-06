using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Weapon
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag(Tags.Player);
        _player = playerObj.transform;
    }

    protected override void Shoot()
    {
        _lastShoot = Time.time;

        Vector2 spawnPosition = transform.position;
        Vector2 targetPosition = _player.position;
        Vector2 direction = (targetPosition - spawnPosition).normalized;
        GameObject projectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        //rb.velocity = direction * _projectileSpeed;
    }

    public override void Update()
    {
        if (_player == null) return;
        float distanceTarget = Vector2.Distance(transform.position, _player.position);
        if (distanceTarget <= _fireRange)
        {
            base.Update();
        }
    }



}
