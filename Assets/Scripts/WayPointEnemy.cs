using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointEnemy : Enemy
{
    [SerializeField] private Transform[] _wayPoints;
    private int _currentWayPoint = 0;
    private Vector2 _currentDestination;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (_wayPoints != null && _wayPoints.Length > 0)
        {
            _currentDestination = _wayPoints[0].position;
        }
    }
    protected override void PatrolBehavior()
    {
        if (_wayPoints == null || _wayPoints.Length == 0)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        MoveWayPoint(_currentDestination);
        float distancePoint = Vector2.Distance(transform.position, _currentDestination);
        if (distancePoint < _stopDistance)
        {
            _currentWayPoint++;
            if (_currentWayPoint >= _wayPoints.Length)
            {
                _currentWayPoint = 0;
            }
            _currentDestination = _wayPoints[_currentWayPoint].position;
        }
    }

   

    
}
