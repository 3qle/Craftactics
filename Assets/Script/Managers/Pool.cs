using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
    public class Pool
    {
        public Character[] heroes;
        public Character[] enemies;
        
        public Cell tile;

      [HideInInspector]  public List<Character> HeroesList = new List<Character>();
      [HideInInspector]  public List<Character> EnemiesList = new List<Character>();

      
        public void AddCharacterToPool(Character character)
        {
            if(character.side == Character.Fraction.Hero && !HeroesList.Contains(character))
                HeroesList.Add(character);
            if(character.side == Character.Fraction.Enemy && !EnemiesList.Contains(character))
                EnemiesList.Add(character);
        }
      
        public void RemoveCharacterFromPool(Character character)
        {
            if(EnemiesList.Contains(character)) EnemiesList.Remove(character);
            if(HeroesList.Contains(character)) HeroesList.Remove(character);
        }
    }
