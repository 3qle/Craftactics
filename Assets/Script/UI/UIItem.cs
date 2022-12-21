using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Character;
using Script.Enum;
using Script.Managers;
using Script.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class UIItem
    {
        public Transform Container;
        List<ItemButton> Items = new List<ItemButton>();
        private Character _character;
        private Console _console;
        private UIShop _uiShop;
        public UIItem(Spawner starter, Transform container)
        {
            _console = starter.ui.Console;
            _uiShop = starter.shop.UIShop;
            Container = container;

            for (int i = 0; i < Container.childCount; i++)
            {
                Items.Add(Container.GetChild(i).GetComponent<ItemButton>());
                Items[i].Initialize(i,this);
            }
        }

        public void SelectWeapon(int i)
        {
            DeselectButtons();
            if (_character == null || i >= _character.Bag.Items.Count) return;
            Items[i].HighLightButton(true);
          
            _console.ShowInfo(_character.Bag.Items[i]);
            if (_character.entityType == EntityType.Enemy) return;
           
            _character.Arms.SelectWeapon(_character.Bag.Items[i],_character);
            _uiShop.SelectCharacterItem(_character.Bag.Items[i]) ;
        }

        public void DeselectButtons()
        {
            foreach (var card in Items) 
              card.HighLightButton(false);
        }

        void ClearButtons()
        {
            foreach (var card in Items) 
                card.ClearButton();
        }
        public void ChangeCharacter(Character fightable)
        {
            _console.Clear();
            ClearButtons();
            _character = fightable;
        }
 
        public void UpdateButtons(Character character)
        {
            if(character != null)
                foreach (var button in Items)
                  button.UpdateButtonInfo(character, Items.IndexOf(button));
        }
    }
