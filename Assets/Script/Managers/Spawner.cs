using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public UI ui;
    public Pool pool;
    public Field field;
    public Turn turn;
    public Controller controller;
    public Shop shop;
    
    [Header("Spawner Settings")]
    public int maxEnemy;
    int _spawnX, _spawnY;


    public void SpawnOnBattleStart()
    {
        SpawnCells();
      SpawnHeroes();
        SpawnEnemies(maxEnemy);
        SpawnPopUpText();
    }
    void SpawnCells()
    {
        field.CreateField();
        for (var x = 0; x < field.width; x++)
        for (var y = 0; y < field.height; y++)
        {
            var cell =  Instantiate(pool.tile, new Vector2(x,y), quaternion.identity, transform.GetChild(0));
            field.SetCellOnField(cell,x,y);
        }
    }
   

    public void SpawnHeroes()
    {
        for (int i = 0; i < pool.heroes.Length; i++)
        {
            var  hero =Instantiate(pool.heroes[i], new Vector3(i,0,-10), Quaternion.identity, transform.GetChild(2));
            hero.Initialize(this);
            pool.AddCharacterToPool(hero);
        }
    }

   public void SpawnEnemies(int max)
    {
        for (int i = 0; i < max; i++)
        {
            var  enemy =Instantiate(pool.enemies[Random.Range(0, pool.enemies.Length)], field.CreateSpawnPoint(), Quaternion.identity, transform.GetChild(2));
            enemy.Initialize(this);
            pool.AddCharacterToPool(enemy);
        }
   }

   public void SpawnPopUpText()
   {
       for (int i = 0; i < 30; i++) 
           pool.PopUpList.Add(Instantiate(pool.PopUpText,transform.position,Quaternion.identity,ui.PopUpContainer.transform));
   }
   
}
