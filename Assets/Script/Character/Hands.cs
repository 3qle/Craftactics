using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
    public class Hands
    {
        private Character _character;
    //    public Item leftItem, rightItem; 
       public Item selectedItem; 
       
       

        public void Initialize(Character character)
        {
            _character = character;
            for (int i = 0; i < _character.Bag.Items.Count; i++) 
                _character.Bag.Items[i].Initialize(character);
        }
        
        public void PrepareWeapon(Item item)
        {
            if (_character.Attributes.stamina.SP >= item.SPCost && !_character.Legs._isWalking)
            {
                selectedItem = item;
                if (_character.side == Character.Fraction.Hero)
                    _character.field.ShowAttackTiles(selectedItem);
            }
        }

        public void PrepareRandomWeapon()
        {
            PrepareWeapon(_character.Bag.Items[Random.Range(0, _character.Bag.Items.Count)]);
        }
        
       
    }