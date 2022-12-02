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
public class Card : MonoBehaviour
{
    
    public Image Image;
    public TextMeshProUGUI  Cost;
    
    private UIItemButtons _ui;
    private int num;
    public void UpdateButtonInfo(Item item, Character fightable)
    {
      
        Cost.text = item.SPCost.ToString();
        Cost.color = fightable.Attributes.stamina.SP >= item.SPCost ? Color.green : Color.red;
        Image.enabled = true;
        Image.sprite = item.WeaponIcon;
    }

    public void ClearButton()
    {
        Cost.text = "";
        Image.enabled = false;

    }

    public void Initialize(int i, UIItemButtons ui)
    {
        _ui = ui;
        num = i;
    }
    
    public void SelectWeapon() => _ui.SelectWeapon(num);


}
