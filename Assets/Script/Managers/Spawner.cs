using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Enum;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public int maxEnemy; 
   
    public Transform EntityContainer;
    
    [Header("Settings")]
    public UI ui;
    public Pool pool;
    public Field field;
    public Turn turn;
    public Controller controller;
    public Shop shop;
    
    int _spawnX, _spawnY;
    private Entity[] Entities;
   public void SpawnOnBattleStart()
    {
        SpawnCells();
        SpawnEnemies(maxEnemy);
        SpawnPopUpText();
        GetEntities();
    }
    void SpawnCells()
    {
        field.CreateField(this);
        for (var x = 0; x < field.width; x++)
        for (var y = 0; y < field.height; y++)
        {
            var cell =  Instantiate(pool.tile, new Vector2(x,y), quaternion.identity, transform.GetChild(0));
            field.SetCellOnField(cell,x,y);
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

   
   public void GetEntities()
   {
       Entities = Resources.LoadAll<Entity>($"Prefab/Entities/");
       foreach (var item in Entities) 
          pool.AddShopEntities(Instantiate(item,EntityContainer),this);
   }
   
  
}
