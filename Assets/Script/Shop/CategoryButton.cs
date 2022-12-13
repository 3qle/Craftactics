using System;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Script.Shop
{
    public class CategoryButton : MonoBehaviour
    {
      public  Image Icon;
      public ItemType Type;
        public Item[] List;
        public List<Item> Items;
       [HideInInspector] public Button Button;
        private UIShop _uiShop;
        private List<ItemButton> _container;
        private void Awake()
        {
            GetItems();
            Button = GetComponent<Button>();
        }

        public void Initialize(List<ItemButton> container, UIShop ui,int i)
        {
            _container = container;
            _uiShop = ui;
            Button.onClick.AddListener(() => ShowCategory(true));
        }
        
        public void ShowCategory(bool show)
        {
            if(show)
                _uiShop.SetSelectedCategory(this);

            for (int i = 0; i < Items.Count; i++) 
                _container[i].ShowInShop(Items[i],_uiShop,show);
            
            Button.image.sprite = Resources.Load<Sprite>("Sprites/UI/CharacterButton/" + show);
        }
        
        public void GetItems()
        {
            List = Resources.LoadAll<Item>($"Prefab/Weapons/Hero Weapons/{name}/");
            foreach (var item in List) 
                Items.Add(Instantiate(item,transform));
        }

        public void EnableButton(global::Character character)
        {
            foreach (var type in character.WeaponMastery.Types)
                if (type == Type)
                {
                    Button.enabled = Icon.enabled = true; break;
                }
                else Button.enabled = Icon.enabled = false;
        }
    }
}