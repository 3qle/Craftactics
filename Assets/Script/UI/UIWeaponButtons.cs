using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Managers;
using Script.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 [Serializable]
    public class UIWeaponButtons
    {
        public WeaponHand Hand;
        public Button Button;
        public Card Ui;

        private Weapon _weapon;
        private Character _fightable;

        public void SetButton()=>Button.onClick.AddListener(SelectWeapon);
        
        public void SelectWeapon() => _fightable.Hands.PrepareWeapon(_weapon);
        
        public void ShowButton(Character fightable, Weapon weapon,WeaponHand hand)
        {
            if (Hand == hand)
            {
                _weapon = weapon;
                _fightable = fightable;
                Ui.UpdateButtonInfo(weapon, fightable);
            }
        }

        
        
        
    }
