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

    protected override void Update()
    {
        // niente CheckPlayer: inseguo sempre
        _currentState = EnemyState.PURSUIT;
    }

    protected override void PatrolBehavior() {} // NON viene usato per muoversi ma usiamo FixedUpdate nella classe madre avendo EnemyState.PURSUIT

}
