 using Script.Enum;
 using TMPro;
 using UnityEngine;
 using UnityEngine.UI;

 public class ShopButton: MonoBehaviour
    {  
        public Image Image, HighLight;
        public TextMeshProUGUI  Cost, Quantity;
        private Spawner _spawner;
        private UIItem _ui;
        private UIShop _uiShop; 
        private Entity _item;
        private Wallet _wallet;
        private int num;
        private Button Button;
        private void Awake()
        {
            Button = GetComponent<Button>();
        }
       
        public void ShowItemInShop(Entity item, Spawner starter, bool show)
        {
            _item = item;
            _uiShop = starter.shop.UIShop;
            _wallet = starter.shop.Wallet;
            if(item.Bought ) return;
            
            Image.enabled = true;
            Button.enabled = show;
            SetShopButton();
            SetContent(show);
        }

        void SetContent(bool show)
        {
            if (show)
            {
                Cost.text = _item.ShopCost.ToString();
                Cost.color = _wallet.IsEnough(_item.ShopCost)? Color.green : Color.red;
                Quantity.text = _item.QuantityInShop > 1 ? _item.QuantityInShop.ToString() : "";
                Image.sprite = _item.Icon.sprite;
            }
            else
                ClearButton(true);
        }
       
        public void SetShopButton()
        {
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(() => { SelectItemInShop(true); });
        }
        
        public void SelectItemInShop(bool show)
        {
            
            if (show)
            {
                _uiShop.HideShopButtons();

                if (_item.entityType != EntityType.Hero)
                {
                    Item item = (Item) _item;
                    item.SelectInShop(_uiShop.SelectedCharacter);
                }
                  
              
                _uiShop.SelectItemInShop(this,_item);
            }
       
            HighLightButton(show);
        }
     
        
        public void HighLightButton(bool selected) => HighLight.enabled = selected;
        
        public void ClearButton(bool clear)
        {
            if(!clear) SetContent(true);
            else
            {
                Cost.text = Quantity.text = "";
                Image.enabled = false;
        
                HighLightButton(false);
            }
           
        }
    }
