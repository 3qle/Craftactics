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

public class ItemButton : MonoBehaviour
{
    
    public Image Image, HighLight;
    public TextMeshProUGUI  Cost;
    
    private UIItem _ui;
    private UIShop _uiShop;
    private Item _item;
    private int num;
    private Button Button;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    public void UpdateButtonInfo(Item item, Character fightable)
    {
        Cost.text = item.SPCost.ToString();
        Cost.color = fightable.Attributes.stamina.current >= item.SPCost ? Color.green : Color.red;
        Image.enabled = true;
        Image.sprite = item.WeaponIcon;
    }

    public void ShowInShop(Item item, UIShop uiShop, bool show)
    {
        _item = item;
        _uiShop = uiShop;
        Image.enabled = true;
        Button.enabled = show;
        SetShopButton();
        if (show)
        {
            Cost.text = item.ShopCost.ToString();
            Image.sprite = item.WeaponIcon;
        }
        else
         ClearButton();
    }

    public void ClearButton()
    {
        Cost.text = "";
        Image.enabled = false;
        
        HighLightButton(false);
    }

    public void Initialize(int i, UIItem ui)
    {
        _ui = ui;
        num = i;
    }
    
    public void SelectWeapon() => _ui.SelectWeapon(num);

    public void SelectInShop(bool show)
    {
        if (show)
        {
            foreach (var VARIABLE in _uiShop.ItemButtons) 
                VARIABLE.SelectInShop(false);
        
            _item.Initialize(_uiShop.SelectedCharacter);
            _uiShop.ShowConsole(_item);
        }
       
        HighLightButton(show);
    }

    public void SetShopButton()
    {
        Button.onClick.AddListener(() => SelectInShop(true));
    }

    public void HighLightButton(bool selected) => HighLight.enabled = selected;
    
}
