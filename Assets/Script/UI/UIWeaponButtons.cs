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
namespace Script.UI
{
    [Serializable]
    public class UIWeaponButtons
    {
        public WeaponHand Hand;
        public Button Button;
        public Card Ui;

        private IWeapon _weapon;
        private IFightable _fightable;

        public void SetButton()=>Button.onClick.AddListener(SelectWeapon);
        
        public void SelectWeapon() => _fightable.PrepareWeapon(_weapon);
        
        public void ShowButton(IFightable fightable, IWeapon weapon,WeaponHand hand)
        {
            if (Hand == hand)
            {
                _weapon = weapon;
                _fightable = fightable;
                Ui.UpdateButtonInfo(weapon, fightable);
            }
        }

        
        
        
    }
}