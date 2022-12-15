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
        public Transform Container;
        List<ItemButton> Items = new List<ItemButton>();
        private Character _character;
        private Console _console;
        public void Initialize(Console console)
        {
            _console = console;

            for (int i = 0; i < Container.childCount; i++)
            {
                Items.Add(Container.GetChild(i).GetComponent<ItemButton>());
                Items[i].Initialize(i,this);
            }
        }

        public void SelectWeapon(int i)
        {
            DeselectButtons();
            if (i > _character.Bag.Items.Count) return;
            Items[i].HighLightButton(true);
            _character.Arms.SelectWeapon(_character.Bag.Items[i]);
            _console.ShowInfo(_character.Bag.Items[i]);
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
        public void ChangeItems(Character fightable)
        {
            _console.Clear();
            ClearButtons();
            _character = fightable;
        }
 
        public void UpdateButtons(Character character)
        {
            if(character != null)
                for (int i = 0; i < character.Bag.Items.Count; i++) 
                    Items[i].UpdateButtonInfo(character.Bag.Items[i], character);
        }
    }
