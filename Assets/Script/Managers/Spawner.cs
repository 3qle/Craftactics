using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")] 
    public int maxParty;
    public int maxEnemy;
    int _spawnX, _spawnY;
    public Character[] heroes;
    public Character[] enemies;

    public int height,width;

    public void SpawnHeroes(IViewable field)
    {
        for (int i = 0; i < maxParty; i++)
        { 
            var  hero =Instantiate(heroes[i], new Vector3(i, 0, 0), Quaternion.identity, transform.GetChild(2));
            hero.SetField(field);
        }
    }

   public void SpawnEnemy(Vector3 spawnPos, IViewable field)
    { 
        var  enemy =Instantiate(enemies[Random.Range(0, 4)], spawnPos, Quaternion.identity, transform.GetChild(2)); 
        enemy.SetField(field);
    }
   
}
