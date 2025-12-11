using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHandler : MonoBehaviour
{
    private string name;
    public int _hp;
    private int _maxHp = 100;

    private string GetName() => name;
    public void SetName(string nome) => name = nome;
    public int GetHp() => _hp;
    public void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHp);

        if (hp != _hp)
        {
            _hp = hp;

            if (hp <= 0)
            {
                Defeated();
            }
        }
    }
    public void AddHp(int amount) => SetHp(_hp + amount);
    public void TakeDamage(int damage)
    {
        AddHp(-damage);
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

    }
}
