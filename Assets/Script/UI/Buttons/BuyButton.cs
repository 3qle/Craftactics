using System;
using Script.Character;
using Script.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class BuyButton : MonoBehaviour
    {
        private Image _image;
        private TextMeshProUGUI _text;
        public Button _button;
        public bool buy;
        private void Start()
        {
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponent<Button>();
        }

        public bool CanBuy => buy;

        public void SetCost(int cost, int bank, Character character, Entity entity)
        {
            if (character.Bag.Stash.Contains(entity))
               
            {
                if (entity.TrySell())
                {
                    buy =false;
                    _text.text = $"+{cost}";
                    Show(true);
                }
                else
                {
                    Item item = (Item) entity;
                    Disable($"Sell {item.projectileType}s ");
                }
                
            }
            
            else
            {
                buy = true;
                if (cost <= bank && (character.Active || entity.entityType == EntityType.Hero))
                {
                    Show(entity.entityType == EntityType.Hero || ValidWeapon((Item) entity, character));
                    _text.text = cost.ToString();
                }
                else
                    Disable("No Money");
            }
           
        }

        void Show(bool show)
        {
            _button.enabled = show;
            _image.sprite = Resources.Load<Sprite>("Sprites/Shop/BuyButton/" + show);
        }

        private bool ValidWeapon(Item entity, Character character)
        {
            Debug.Log( $"xx {character != null && entity.itemType == ItemType.Projectile && character.Arms.selectedItem != null && character.Arms.selectedItem.projectileType == entity.projectileType || entity.itemType != ItemType.Projectile}");
            return character != null && entity.itemType == ItemType.Projectile &&
                   character.Arms.selectedItem != null &&
                   character.Arms.selectedItem.projectileType == entity.projectileType
                   || entity.itemType != ItemType.Projectile;
        }

        public void Disable(string word)
        {
            buy = false;
            _button.enabled = false;
            _image.sprite = Resources.Load<Sprite>("Sprites/Shop/BuyButton/" + false);
            _text.text = word;
        }
    }
