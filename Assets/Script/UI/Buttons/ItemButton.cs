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
    private Item _item;
    private Character _character;
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

    

    public void HighLightButton(bool selected) => HighLight.enabled = selected;
    
}
