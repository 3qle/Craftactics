using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Character;
using Script.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Console
{
    public Transform Windows;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI[] Duration, Points;
    public Image[] DamageElement;
    public Image[] Stack;
    public Image RangeImage;
    public Image SPCost, ProjectilesAmount, ProjectileMax;
    private UI _ui;
    public void Initialize(UI ui)
    {
        _ui = ui;
        for (int i = 0; i <Windows.childCount ; i++)
        {
            Points[Windows.childCount - 1 - i] =  Windows.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            DamageElement[Windows.childCount - 1 - i] = Windows.GetChild(i).GetChild(1).GetComponent<Image>();
            Duration[Windows.childCount - 1 - i] = Windows.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>();
        }
    }
    public void ShowInfo(Item item,Character character)
    {
        Clear();
       
        ShowText(item.ActiveItem,character);
        ShowDamageStack(item.ActiveItem);
        ShowElement(item.ActiveItem);
        _ui.ShowSwitches(item.Projectiles.Count > 0);
         ProjectileMax.fillAmount = item.projectileType != ProjectileType.No && item.ActiveItem == item ? (float)item.maxCapacity/20 : 0;
         ProjectilesAmount.fillAmount = item.ActiveItem.itemType == ItemType.Projectile ?(float)item.ActiveItem.GetProjectilesCount()/20:(float) item.GetProjectilesInWeaponAmount()/20;
    }

    void ShowElement(Item item)
    {
        for (int i = 0; i < item.Properties.Count; i++)
        { 
            Points[i].enabled = DamageElement[i].enabled = true; 
            Points[i] = item.Properties[i].Text(Points[i]);
            DamageElement[i].sprite = item.Properties[i].Icon;
            Duration[i].text = item.Properties[i].Duration > 0
                ? item.Properties[i].Duration.ToString():
                "";
        }
    }

    void ShowText(Item item, Character character)
    {
        Name.text = $"{item.Name}";
        SPCost.fillAmount =(float) item.staminaCost / 10;
        RangeImage.sprite = item.itemRange.Icon; 
    }
    public void Clear()
    {
        for (int i = 0; i < DamageElement.Length; i++)
        {
            
            DamageElement[i].enabled = false;
            Points[i].enabled = false;
            ProjectilesAmount.fillAmount = SPCost.fillAmount = ProjectileMax.fillAmount =  0;
            foreach (var stack in Stack)
                stack.gameObject.SetActive(false);
        }

        foreach (var VARIABLE in Duration)
        {
            VARIABLE.text = "";
        }
        Name.text =  "";
        SPCost.fillAmount = 0;
    }

    public void ShowDamageStack(Item item)
    {
        for (int i = 0; i < item.damageStack.Stack.Length; i++)
        {
                
            if (item.damageStack.Stack[i] > 0)
            {
                Stack[i].gameObject.SetActive(true);
                Stack[i].transform.GetChild(0).GetComponent<Image>().fillAmount =(float)item.damageStack.Stack[i]/100;
            }
               
        }
    }

   
}
