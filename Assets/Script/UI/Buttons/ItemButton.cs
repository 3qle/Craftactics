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
    private Spawner _spawner;
    private UIItem _ui;
    private UIShop _uiShop;
   public Item _item;
    private Character _character;
    private int num;
    private Button Button;
  
    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    public void UpdateButtonInfo( Character fightable, int i)
    {
        if (i < fightable.Bag.Items.Count)
        {
            _item = fightable.Bag.Items[i];
            Cost.text = _item.SPCost.ToString();
            Cost.color = fightable.Attributes.stamina.current >= _item.SPCost ? Color.green : Color.red;
            Image.enabled = true;
            Image.sprite =_item.Icon.sprite;
        }
        else
        {
            ClearButton();
        }
      
    }

   
    
    public void ClearButton()
    {
        _item = null;
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

    

    public void HighLightButton(bool selected) => HighLight.enabled = selected;
    
}
