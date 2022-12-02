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
    public Image CardShell;
    public Image Image;
    public TextMeshProUGUI Name, Cost, Dam;
    public Button Button;
    private UIWeaponButtons _ui;
    private int num;
    public void UpdateButtonInfo(Item item, Character fightable)
    {
       // Name.text = weapon.Name;
        Cost.text = item.SPCost.ToString();
        Cost.color = fightable.Attributes.stamina.SP >= item.SPCost ? Color.green : Color.red;
     //   Dam.color = GetDamageColor(weapon.WeaponElement);
       // Dam.text = weapon.Damage.ToString();
        Image.sprite = item.WeaponIcon;
    }

    public void Initialize(int i, UIWeaponButtons ui)
    {
        _ui = ui;
        num = i;
    }
    
    public void SelectWeapon() => _ui.SelectWeapon(num);


}
