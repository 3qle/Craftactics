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
    public List<TextMeshProUGUI> Duration, Points = new List<TextMeshProUGUI>();
    public List<Image> DamageElement = new List<Image>();
    public List<Image> Stack = new List<Image>();
    public Image SPCost, ProjectilesAmount, ProjectileMax;
    private UI _ui;
    public void Initialize(UI ui)
    {
        
        _ui = ui;
        for (int i = 0; i <Windows.childCount ; i++)
        {
            var childCount = Windows.childCount;
            Points.Add(Windows.GetChild(childCount - 1 - i).GetChild(0).GetComponent<TextMeshProUGUI>());
            DamageElement.Add(Windows.GetChild(childCount - 1 - i).GetChild(1).GetComponent<Image>());
            Duration.Add(Windows.GetChild(childCount - 1 - i).GetChild(2).GetComponent<TextMeshProUGUI>()); ;
      
        } 
       
    }
    public void ShowInfo(Item item)
    {
        Clear();
       
        ShowText(item.ActiveItem);
        ShowDamageStack(item.ActiveItem);
        ShowElement(item.ActiveItem);
        _ui.ShowSwitches(item.Projectiles.Count > 0);
         ProjectileMax.fillAmount = item.projectileType != ProjectileType.No && item.ActiveItem == item ? (float)item.maxCapacity/20 : 0;
         ProjectilesAmount.fillAmount = item.ActiveItem.itemType == ItemType.Projectile ?(float)item.ActiveItem.GetProjectilesCount()/20:(float) item.GetProjectilesInWeaponAmount()/20;
    }

    void ShowElement(Item item)
    {
        for (int i = 0; i < item.Shapers.Count; i++)
        {
            Points[i].enabled = true;
            DamageElement[i].enabled = true; 
            Points[i] = item.Shapers[i].Text(Points[i]);
            DamageElement[i].sprite = item.Shapers[i].Icon;
            Duration[i].text = item.Shapers[i].Duration > 0
                ? item.Shapers[i].Duration.ToString():
                "";
        }
    }

    void ShowText(Item item)
    {
        Name.text = $"{item.Name}";
        SPCost.fillAmount =(float) item.staminaCost / 10;
    }
    public void Clear()
    {
        for (int i = 0; i < DamageElement.Count; i++)
        {
          
          
                Points[i].enabled = false;
                DamageElement[i].enabled = false;
        }
        
        ProjectilesAmount.fillAmount = SPCost.fillAmount = ProjectileMax.fillAmount =  0;
        foreach (var stack in Stack)
            stack.gameObject.SetActive(false);
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
            if (item.damageStack.Stack[i] > 0) 
            {
                Stack[i].gameObject.SetActive(true);
                Stack[i].transform.GetChild(0).GetComponent<Image>().fillAmount =(float)item.damageStack.Stack[i]/100;
            }
    }
}
