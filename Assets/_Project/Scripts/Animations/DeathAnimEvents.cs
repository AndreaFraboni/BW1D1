using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimEvents : MonoBehaviour
{
    [SerializeField] private Player _playerRef;
    [SerializeField] private Enemy _enemyRef;

    private void Awake()
    {
        _playerRef = GetComponentInParent<Player>();
        _enemyRef = GetComponentInParent<Enemy>();
    }

    public void DestroygameObject(string _state)
    {
        if (_state == "player") _playerRef.DestroyGOplayer();
        if (_state == "enemy") _enemyRef.DestroyGOenemy();
    }

}
