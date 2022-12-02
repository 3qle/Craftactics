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
    public class UIItemButtons
    {
        
        public Card[] Cards;
        
        private Character _character;
        private Console _console;
       
        public void Initialize(Console console)
        {
            _console = console;
            for (int i = 0; i < Cards.Length; i++)
            {
               Cards[i].Initialize(i,this);
            }
            
        }

        public void SelectWeapon(int i)
        {
            _character.Hands.PrepareWeapon(_character.Bag.Items[i]);
           _console.ShowInfo(_character.Bag.Items[i]);
        }

        

        public void ShowButton(Character fightable)
        {
            foreach (var card in Cards)
            card.ClearButton();
            
            _character = fightable;
                for (int i = 0; i < _character.Bag.Items.Count; i++)
                {
                    Cards[i].UpdateButtonInfo(_character.Bag.Items[i], fightable);
                }
                   
        }
 
    }
