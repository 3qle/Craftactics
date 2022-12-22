using System;
using System.Collections.Generic;
using Script.Character;
using Script.Character.Attributes;
using Script.Enum;
using UnityEngine;

[Serializable]
    public class Pool
    {
        public Character[] heroes;
        public Character[] enemies;
        
        public CellButton tile;
        public PopUp PopUpText;
        
        [HideInInspector]  public List<PopUp> PopUpList;
        [HideInInspector]  public List<Character> ActiveHeroes = new List<Character>();
        public List<Character> EnemiesList = new List<Character>();
     
        public List<Entity> EntityPool = new List<Entity>();

        public void AddCharacterToPool(Character character)
        {
           
            if(character.entityType == EntityType.Hero && !ActiveHeroes.Contains(character))
                ActiveHeroes.Add(character);
            if(character.entityType == EntityType.Enemy && !EnemiesList.Contains(character))
                EnemiesList.Add(character);
        }
      
        public void RemoveCharacterFromPool(Character character)
        {
            if(EnemiesList.Contains(character)) EnemiesList.Remove(character);
            if(ActiveHeroes.Contains(character)) ActiveHeroes.Remove(character);
        }

        public void AddShopEntities(Entity item, Spawner starter)
        {
            EntityPool.Add(item);
            item.Initialize(starter);
        }
    }
