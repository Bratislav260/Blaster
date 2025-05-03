using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance { get; private set; }

    public List<List<Enemy>> enemies = new List<List<Enemy>>()
    {
        new List<Enemy>(),
        new List<Enemy>(),
        new List<Enemy>()
    };

    public List<Enemy> currentEnemiesList;

    public void Initialize()
    {
        Instance = this;
        currentEnemiesList = enemies[0];
    }

    public void SetCurrentList(int index)
    {
        currentEnemiesList = enemies[index];
    }

    public void Add(Enemy enemy)
    {
        currentEnemiesList.Add(enemy);
    }

    public void Remove(Enemy enemy)
    {
        currentEnemiesList.Remove(enemy);
    }

    public void EnemiesUpdate()
    {
        for (int i = 0; i < currentEnemiesList.Count; i++)
        {
            if (currentEnemiesList[i] == null)
            {
                currentEnemiesList.Remove(currentEnemiesList[i]);
                continue;
            }

            currentEnemiesList[i].EnemyUpdate();
        }
    }

    public void EnemiesFixedUpdate()
    {
        for (int i = 0; i < currentEnemiesList.Count; i++)
        {
            currentEnemiesList[i].EnemyFixedUpdate();
        }
    }
}
