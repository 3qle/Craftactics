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
    public class UIItem
    {
        public ItemButton[] Cards;
        private Character _character;
        private Console _console;
        public void Initialize(Console console)
        {
            _console = console;
            for (int i = 0; i < Cards.Length; i++) 
                Cards[i].Initialize(i,this);
        }

        public void SelectWeapon(int i)
        {
            DeselectButtons();
            Cards[i].HighLightButton(true);
            _character.Arms.SelectWeapon(_character.Bag.Items[i]);
            _console.ShowInfo(_character.Bag.Items[i]);
        }

        public void DeselectButtons()
        {
            foreach (var card in Cards) 
              card.HighLightButton(false);
        }

        void ClearButtons()
        {
            foreach (var card in Cards) 
                card.ClearButton();
        }
        public void ChangeItems(Character fightable)
        {
            _console.Clear();
            ClearButtons();
            _character = fightable;
        }

        public void UpdateButtons()
        {
            if(_character != null)
                for (int i = 0; i < _character.Bag.Items.Count; i++) 
                    Cards[i].UpdateButtonInfo(_character.Bag.Items[i], _character);
        }
    }
