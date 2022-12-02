using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Console
{
    public TextMeshProUGUI Name, Damage;
    public Image DamageElement,ElementBackground;
    
    public void ShowInfo(Item item)
    {
        DamageElement.enabled = ElementBackground.enabled = true;
        Name.text = item.Name;
        Damage.text = item.Damage.ToString();
        DamageElement.sprite = item.WeaponIcon;
    }
    
    public void Clear()
    {
        DamageElement.enabled = ElementBackground.enabled = false;
        Name.text = "";
        Damage.text = "";
      
          
    }
}
