using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


    public class Arms
    {
        private Character _character;
        public Item selectedItem; 
       
        
        public Arms (Character character)
        {
            _character = character;
            foreach (var item in _character.Bag.Items)
                item.Initialize(character);
        }
        
        public void SelectWeapon(Item item)
        {
            if (!_character.Legs._isWalking)
            {
                selectedItem = item;
                item.ModifyDamage();
                if (_character.side == Character.Fraction.Hero)
                    _character.field.ShowAttackTiles(selectedItem);
            }
        }

        public void DeselectWeapon(bool select)
        {
            selectedItem = select?selectedItem:null;
        }
        public Item SelectRandomWeapon()
        {
            SelectWeapon(_character.Bag.Items[Random.Range(0, _character.Bag.Items.Count)]);
            return selectedItem;
        }
        
       
    }