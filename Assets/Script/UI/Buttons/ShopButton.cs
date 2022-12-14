 using System;
 using System.Collections.Generic;
 using Script.Enum;
 using TMPro;
 using UnityEngine;
 using UnityEngine.UI;

 public class ShopButton: MonoBehaviour
 {
        public Image ItemImage, Image;
        Sprite[] HighLight;
        public TextMeshProUGUI  Cost, Quantity;
        public Entity _item;
        public Button Button;
        public bool Selected;
        public static Action<Character> CharacterButtonSelected;
        private Character _selectedCharacter;
        private UIShop _uiShop;
        private List<ShopButton> _shopButtons;
        private void Awake()
        {
            Button = GetComponent<Button>();
            Image = GetComponent<Image>();
            Button.onClick.AddListener(() => { SelectItem(true); });
            HighLight = Resources.LoadAll<Sprite>("Sprites/Shop/CategoryButton/");
        }
        
        public void SetItem(Entity entity, UIShop uiShop,List<ShopButton> list)
        {
            _uiShop = uiShop;
            _item = entity;
            _shopButtons = list;
            Image.enabled = ItemImage.enabled = Button.enabled = true;
            ItemImage.sprite = _item.Icon.sprite;
            Cost.text = _item.ShopCost.ToString();
            if (_item.QuantityInShop > 0)
            {
                Image.sprite = Selected ? HighLight[1] : HighLight[0];
                Cost.color = uiShop._shop.Wallet.Coins >_item.ShopCost? Color.green : Color.red; 
                Quantity.text = _item.QuantityInShop > 1 ? _item.QuantityInShop.ToString() : "";
            }
            else
                ClearButton();
        }
        public void SelectItem(bool show)
        { 
            if (show)
            {
              _uiShop.SelectEntity(_item);
                foreach (var button in _shopButtons) 
                    button.SetHighlight(false);
                ChooseEntity();
                SetHighlight(true);
            }
        }
        
       public void SetHighlight(bool show)=>Image.sprite = show ? HighLight[1] : HighLight[0];
        
        void ChooseEntity()
        {
            if (_item.entityType != EntityType.Hero)
            {  
                _selectedCharacter =  _uiShop.SelectedCharacter;
                var item = (Item) _item;
                item.SelectInShop(_selectedCharacter);
            }
            else CharacterButtonSelected.Invoke((Character)_item);
        }
        
        public void ClearButton()
        {
            Cost.text = Quantity.text = "";
            ItemImage.enabled = Image.enabled = Button.enabled = false;
        }
 }
