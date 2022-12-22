using System;
using System.Collections.Generic;
using Script.Character;
using Script.Enum;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;


    public class CategoryButton : MonoBehaviour
    {
      public  Image Icon;
     
       
       [HideInInspector] public Button Button;
        private UIShop _uiShop;
        private List<ShopButton> _container;
        private Spawner _spawner;
        public EntityType categoryType;
        private int index, buttonIndex;
        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        public void Initialize(List<ShopButton> container, UIShop ui,int i,Spawner spawner)
        {
            index = i;
            _spawner = spawner;
            _container = container;
            _uiShop = ui;
            Button.onClick.AddListener(OpenCategory);
        }

        public void CloseCategory()
        {
            Button.image.sprite = Resources.Load<Sprite>("Sprites/Shop/CategoryButton/" + false);
            foreach (var button in _container) 
                button.ClearButton(true);
        }
        public void OpenCategory()
        {
            _uiShop.SelectCategory(this);
                foreach (var item in _spawner.pool.EntityPool) 
                    if (item.entityType == categoryType && !item.Bought)
                    {
                        _container[buttonIndex].ShowItemInShop(item,_spawner,true);
                        buttonIndex++;
                    }
                buttonIndex = 0;
                Button.image.sprite = Resources.Load<Sprite>("Sprites/Shop/CategoryButton/" + true);
            
            
        }

        public void Refresh()
        {
            
        }
        
        public void EnableCategory(Character character, int i)
        {
            Icon.sprite = Resources.Load<Sprite>("Sprites/Shop/Category/" + character.MasteryTypes.Types[i]);
            categoryType = character.MasteryTypes.Types[i];
            Enable(true);
        }

        public void Enable(bool enable)
        {
            Button.enabled = Icon.enabled = enable;
        }
    }
