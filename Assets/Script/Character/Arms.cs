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
           
            if (!character.Legs._isWalking && item && character.Attributes.stamina.current >= item.staminaCost)
            {
                selectedItem = item;
                item.Select(character);
            }
        }

        public void DeselectWeapon(bool select)
        {
            selectedItem = select?selectedItem:null;
        }
        public Item SelectRandomWeapon(Character character)
        {
            SelectWeapon(character.Bag.AllItems[Random.Range(0, character.Bag.AllItems.Count)],character);
            return selectedItem;
        }
        
       
    }