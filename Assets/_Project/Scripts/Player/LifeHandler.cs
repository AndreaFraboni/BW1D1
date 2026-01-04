using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHandler : MonoBehaviour
{
    //private string name;
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp = 100;
    [SerializeField] private int _lives = 3;

    private Rigidbody2D _rb;

    private Enemy _enemyController;
    private Player _playerController;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_collider2D == null) _collider2D = GetComponent<CircleCollider2D>();
        if (_animParam == null) _animParam = GetComponent<AnimationParamHandler>();

        if (TryGetComponent<Player>(out _playerController))
        {
            return;
        }
        else if (TryGetComponent<Enemy>(out _enemyController))
        {
            return;
        }
        else
        {
            Debug.LogError("LifeController è montato su un oggetto che non è un Player o un Enemy !!!");
        }
    }

    private string GetName() => name;
    public void SetName(string nome) => name = nome;
    public int GetHp() => _hp;
    public int GetLives() => _lives;
    public void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHp);

        if (hp != _hp)
        {
            _hp = hp;

            if (_hp == 0)
            {
                _lives--;

                if (_lives == 0)
                {
                    Defeated();
                }
                else
                {
                    _hp = 100;
                }
            }
        }
    }
    public void AddHp(int amount) => SetHp(_hp + amount);

    public void TakeDamage(int damage)
    {
        AddHp(-damage);
    }

    public void TakeHealth(int amount)
    {
        AddHp(amount);
    }

    public bool IsAlive()
    {
        if (_hp > 0)
        {
            return true;
        }
        return false;
    }

    private void Defeated()
    {
        if (this.CompareTag("Enemy"))
        {
  
            if (_enemyController != null) _enemyController.EnemyDeath();
            return;
        }

        if (this.CompareTag("Player"))
        {
            if (_playerController != null) _playerController.PlayerDeath();
            return;
        }
    }
}
