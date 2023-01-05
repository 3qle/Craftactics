using System.Collections.Generic;
using System.Linq;
using Script.Enum;
using UnityEngine;
using UnityEngine.UI;

    public class CategoryButton : MonoBehaviour
    {
       private Image Icon;
       private Sprite[]  HighLight;
       private int buttonIndex;
       private bool Selected;
        private List<ShopButton> ShopButtons;
       [HideInInspector]public List<Entity> _itemList, Entities;
       [HideInInspector]public Button Button; 
       public EntityType categoryType;
       private UIShop _uiShop;
       private List<CategoryButton> _categoryButtons;
       private void Awake()
       {
           Button = GetComponent<Button>();
           Icon = transform.GetChild(0).GetComponent<Image>();
         //  HighLight = Resources.LoadAll<Sprite>("Sprites/Shop/CategoryButton/");
       }
       
       public void Initialize(List<ShopButton> container, List<Entity> list,UIShop  uiShop, List<CategoryButton> categoryButtons)
       {
           _categoryButtons = categoryButtons;
           _uiShop = uiShop;
            Entities = list;
            ShopButtons = container;
            Button.onClick.AddListener( Open);
        }

        public void Change(EntityType type)
        {
            Clear();
            categoryType = type;
            Icon.sprite = Resources.Load<Sprite>("Sprites/Shop/Category/" + categoryType);
            Button.enabled =  Icon.enabled = true;
        }
        
        public void Clear() => Button.enabled =   Icon.enabled = false;

        public void SetHighlight(bool set)
        {
            Icon.color = Color.HSVToRGB(0,0,set?1:0.5f);
        }
        
        
        public void Open( )
        {
            foreach (var button in _categoryButtons) 
                button.SetHighlight(false);
            
            _uiShop.SelectCategory(this);
            SetHighlight(true);
            
            foreach (var item in ShopButtons)
                item.ClearButton(); 
            SetItems();
        }

        void SetItems()
        {
            _itemList.Clear();
            foreach (var entity in Entities.Where(entity => entity.entityType == categoryType && entity.GetAmount() > 0))
                _itemList.Add(entity); 
            for (int i = 0; i < _itemList.Count; i++) 
                ShopButtons[i].SetItem(_itemList[i],_uiShop,ShopButtons);
        }

        public void RemoveItems()
        {
            for (int i = 0; i < _itemList.Count; i++) 
                ShopButtons[i].ClearButton();
        }
    }
