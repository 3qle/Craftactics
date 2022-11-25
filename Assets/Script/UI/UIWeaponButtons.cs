using System;
using Script.Character;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Script.UI
{
    public class UIWeaponButtons : MonoBehaviour
    {
        public WeaponHand Hand;
        public Card WeaponUi;

        private IWeapon _weapon;
        private ICharacter _character;
        private void Start() => Weapon.ShowWeaponButton += ShowButtons;
        
        public void SelectWeapon() => _character.PrepareWeapon(_weapon);
        
        public void ShowButtons(ICharacter character, IWeapon weapon,WeaponHand hand)
        {
            if (Hand == hand)
            {
                _weapon = weapon;
                _character = character;
                WeaponUi.UpdateButtonInfo(weapon, character);
            }
        }

     
        
        
    }
}