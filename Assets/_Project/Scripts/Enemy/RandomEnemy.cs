using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : Enemy
{
    [SerializeField] private float _moveRange = 4f;
    [SerializeField] private float _moveTime = 4f;
    private Vector2 _randomDestination;
    private float _randomTimer;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected override void PatrolBehavior()
    {
        MoveWayPoint(_randomDestination);
        _randomTimer = Time.deltaTime;
        float distance = Vector2.Distance(transform.position, _randomDestination);

        if (distance < _stopDistance || _randomTimer <= 0)
        {
            SetDestination();
        }
    }

    private void SetDestination()
    {
        float randomX = Random.Range (-_moveRange, _moveRange);
        float randomY = Random.Range(-_moveRange, _moveRange);

        Vector2 randomOffset = new Vector2(randomX, randomY);
        _randomDestination = (Vector2)transform.position + randomOffset;
        _randomTimer = _moveTime;
    }
}
    
