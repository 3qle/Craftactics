using System.Collections.Generic;
using Script.Character;
using Script.Enum;
using UnityEngine;
using UnityEngine.UI;

public class UIItem
{
        public List<Button> Switches = new List<Button>();
        public Transform Container;
        public List<ItemButton> Items = new List<ItemButton>();
        private Character _character;
        private Console _console;
        private UIShop _uiShop;
        private Item _item;
        private int projectileIndex;
        private Transform SwitchesTransform;
        public UIItem(Spawner starter, Transform container, Transform switches)
        {
            _console = starter.ui.Console;
            _uiShop = starter.shop.UIShop;
            Container = container;
            SwitchesTransform = switches;
            for (int i = 0; i < Container.childCount; i++)
            {
                Items.Add(Container.GetChild(i).GetComponent<ItemButton>());
                Items[i].Initialize(i,this);
            }

            for (int i = 0; i < switches.childCount; i++)
            {
                Switches.Add(switches.GetChild(i).GetComponent<Button>());
                Switches[i].onClick.AddListener(() =>SwitchProjectile(i == 0));
            }
        }

       
        public void SwitchProjectile(bool left)
        {
            if (_item.projectileType == ProjectileType.No && _item.Projectiles.Count == 0)
            {
                _item.ActiveItem = _item;
            }
            else
            {
                if ( left)
                 projectileIndex =  projectileIndex == 0 ? _item.Projectiles.Count - 1 : projectileIndex -= 1;
                else
                    projectileIndex =  projectileIndex < _item.Projectiles.Count - 1 ?   projectileIndex += 1: 0;

              
                _item.SetProjectile(projectileIndex,_item);
            }
              
            _character.Arms.SelectWeapon(_item,_character);
            _uiShop.SelectCharacterItem(_item.ActiveItem) ;
            _console.ShowInfo(_item,_character );
          
        }

       
        public void SelectWeapon(int i)
        {
            DeselectButtons();
          
            if (_character == null || i >= _character.Bag.AllItems.Count ) return;
            _item =  _character.Bag.AllItems[i] ;
            
            _character.Arms.SelectWeapon(_item,_character);
            _console.ShowInfo(_item,_character );
       
            if (_character.entityType == EntityType.Enemy) return;
            _uiShop.SelectCharacterItem(_item.ActiveItem) ;
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
