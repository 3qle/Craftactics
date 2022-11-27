using System;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
    public class Hands
    {
        private Character _character;
        public Weapon _leftWeapon, _rightWeapon; 
        [HideInInspector] public Weapon SelectedWeapon; 
        [HideInInspector] public Weapon[] WeaponsArray;


        public void Initialize(Character character)
        {
            WeaponsArray = new[] {_leftWeapon, _rightWeapon};
            _character = character;
        }
        
        public void PrepareWeapon(Weapon weapon)
        {
            if (_character.Stamina.SP >= weapon.SPCost && !_character.Legs._isWalking)
            {
                SelectedWeapon = weapon;
                if (_character.side == Character.Fraction.Hero)
                    _character.field.ShowAttackTiles(SelectedWeapon);
            }
        }

        public void PrepareRandomWeapon()
        {
            int i = Random.Range(0, WeaponsArray.Length);
            PrepareWeapon(WeaponsArray[i]);
        }
        
       
    }