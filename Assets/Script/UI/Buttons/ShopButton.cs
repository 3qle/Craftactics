 using TMPro;
 using UnityEngine;
 using UnityEngine.UI;

 public class ShopButton: MonoBehaviour
    {   public Image Image, HighLight;
        public TextMeshProUGUI  Cost;
        private Spawner _spawner;
        private UIItem _ui;
        private UIShop _uiShop;
        public Item _item;
        public Character _character;
        private int num;
        private Button Button;
        private void Awake()
        {
            Button = GetComponent<Button>();
        }
       
        public void ShowItemInShop(Item item, UIShop uiShop, bool show)
        {
            _item = item;
            _uiShop = uiShop;
            
            if(item.Bought) return;
            
            Image.enabled = true;
            Button.enabled = show;
            SetShopButton();
            if (show)
            {
                Cost.text = item.ShopCost.ToString();
                Cost.color = uiShop._shop.Wallet.IsEnough(item.ShopCost)? Color.green : Color.red;
                Image.sprite = item.WeaponIcon;
            }
            else
                ClearButton();
        }

       
        
        public void SetShopButton()
        {
            Button.onClick.RemoveAllListeners();
            
            Button.onClick.AddListener(() =>
            {
                if(_item != null)
                    SelectItemInShop(true);
                else
                 SelectCharInShop(true);
                
            });
        }
        
        public void SelectItemInShop(bool show)
        {
            if (show)
            {
                _uiShop.HideButtons();
                _item.Initialize(_uiShop.SelectedCharacter);
                _uiShop.SelectItemInShop(this);
            }
       
            HighLightButton(show);
        }
        public void SelectCharInShop(bool show)
        {
            if (show)
            {
                _uiShop.HideButtons();
                _uiShop.SelectCharacterInShop(this);
                _spawner.controller.SelectFromShop(_character);
            }
       
            HighLightButton(show);
        }
        public void ShowCharInShop(Character item, UIShop uiShop, bool show,Spawner spawner)
        {
            _spawner = spawner;
            _character = item;
            _uiShop = uiShop;
            if(item.Bought) return;
            Image.enabled = true;
            Button.enabled = show;
            SetShopButton();
            if (show)
            {
                Cost.text = item.Cost.ToString();
                Cost.color = uiShop._shop.Wallet.IsEnough(item.Cost)? Color.green : Color.red;
                Image.sprite = item._sprite.sprite;
            }
            else
                ClearButton();
        }
        public void HighLightButton(bool selected) => HighLight.enabled = selected;
        
        public void ClearButton()
        {
            Cost.text = "";
            Image.enabled = false;
        
            HighLightButton(false);
        }
    }
