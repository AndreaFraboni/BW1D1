using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] protected float _spawnTime = 2f;
    private float _nextSpawn;
    private int _currentEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        _nextSpawn = _spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        _nextSpawn -= Time.deltaTime;
        if (_nextSpawn <= 0f)
        {
            SpawnEnemy();
            _nextSpawn = _spawnTime;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = transform.position;
        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        _currentEnemy++;
        
    }
}
