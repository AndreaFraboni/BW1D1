using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _currentState = EnemyState.PURSUIT;
    }

    protected override void PatrolBehavior()
    {
        _currentState = EnemyState.PURSUIT;
        Vector2 direction = (_playerTarget.position - transform.position).normalized;
        _rb.velocity = direction * _moveSpeed;
    }
    
}
