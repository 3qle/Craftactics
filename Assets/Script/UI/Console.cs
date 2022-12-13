using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Console
{
    public TextMeshProUGUI Name, Damage, DamageStack;
    public Image DamageElement,ElementBackground;
    
    public void ShowInfo(Item item)
    {
        DamageElement.enabled = ElementBackground.enabled = true;
        Name.text = item.Name;
        Damage.text = item.ModDamage.ToString();
        DamageElement.sprite = item.WeaponIcon;
        ShowDamageStack(item);
    }
    
    public void Clear()
    {
        DamageElement.enabled = ElementBackground.enabled = false;
        Name.text = 
        Damage.text =
        DamageStack.text = "";
    }

    public void ShowDamageStack(Item item)
    {
        DamageStack.text =
            $"{item.Damage} + STR {item.StrenghtMultiplier}%{(item.strBonus)} + DEX {item.DexterityMultiplier}% + INT {item.IntellectMultiplier}% + WIS {item.WisdomMultiplier}%";
    }
}
