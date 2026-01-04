using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public List<Enemy> listEnemies = new List<Enemy>();

    public void RegistEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        if (!listEnemies.Contains(enemy))
        {
            listEnemies.Add(enemy);
        }
        else
        {
            Debug.LogWarning("Enemy not added because Enemy is just added to the list of enemies !!!");
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        if (listEnemies.Remove(enemy))
        {
            //Debug.Log("Ho rimosso un enemy!!!");
        }
        else
        {
            Debug.LogError("I was try to remove enemy but is not present in the list !!!!");
        }

        if (listEnemies.Count <= 0)
            Debug.Log("PLAYER WIN !!!!");
    }
}
