using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

    public class Arms
    {
        public Item selectedItem;

        public Arms (Character character )
        {
            
        }
        
        
        public void SelectWeapon(Item item, Character character)
        {
            selectedItem = item;
            if (!character.Legs._isWalking && item && character.Attributes.Get(Trait.Stamina).current >= item.staminaCost)
            {
                
                item.Select(character,false);
            }
        }

        public Item SelectRandomWeapon(Character character)
        {
            SelectWeapon(character.Bag.AllItems[Random.Range(0, character.Bag.AllItems.Count)],character);
            return selectedItem;
        }
        
       
    }