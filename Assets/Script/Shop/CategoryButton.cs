using System;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;


    public class CategoryButton : MonoBehaviour
    {
      public  Image Icon;
      public ItemType Type;
        public Item[] ItemList;
        public Character[] CharacterList;
        public List<Item> Items;
        public List<Character> Characters;
       [HideInInspector] public Button Button;
        private UIShop _uiShop;
        private List<ShopButton> _container;
        private Spawner _spawner;
        private void Awake()
        {
           if(Type != ItemType.Hero) GetItems();
           else 
               GetCharacters();
           
            Button = GetComponent<Button>();
        }

        public void Initialize(List<ShopButton> container, UIShop ui,int i,Spawner spawner)
        {
            _spawner = spawner;
            _container = container;
            _uiShop = ui;
            Button.onClick.AddListener(() => OpenCategory(true));
        }
        
        public void OpenCategory(bool show)
        {
            if(show)
                _uiShop.SelectCategory(this);

            for (int i = 0; i < Items.Count; i++) 
                _container[i].ShowItemInShop(Items[i],_uiShop,show);
            for (int i = 0; i < Characters.Count; i++) 
                _container[i].ShowCharInShop(_spawner.pool.HeroesList[i],_uiShop,show,_spawner);
            Button.image.sprite = Resources.Load<Sprite>("Sprites/UI/CharacterButton/" + show);
        }
        
        public void GetItems()
        {
            ItemList = Resources.LoadAll<Item>($"Prefab/Weapons/Hero Weapons/{name}/");
            foreach (var item in ItemList) 
                Items.Add(Instantiate(item,transform));
        }

        public void GetCharacters()
        {
            CharacterList = Resources.LoadAll<Character>($"Prefab/Weapons/Hero Weapons/{name}/");
            foreach (var item in CharacterList) 
                Characters.Add(Instantiate(item,transform));
        }

        public void EnableCategory(Character character)
        {
            if (character != null)
            {
                foreach (var type in character.MasteryTypes.Types)
                    if (type == Type)
                    {
                        Enable(true); break;
                    }
                    else Enable(false);
            }
            else
            {
                if (Type == ItemType.Hero)
                    Enable(true);
            }

            if (character?.side == Character.Fraction.Enemy)
                Enable(false);
        }

        public void Enable(bool enable)
        {
            Button.enabled = Icon.enabled = enable;
        }
    }
