using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Console
{
    public TextMeshProUGUI Name, Quantity;
    public TextMeshProUGUI[] Duration,Points;
    public Image[] DamageElement,ElementBackground;
    public TextMeshProUGUI[] Stack;
    public void ShowInfo(Item item)
    {
        Clear();
        ShowElement(item);
        ShowText(item);
    
    }

    void ShowElement(Item item)
    {
        for (int i = 0; i < item.Properties.Count; i++)
        { 
            Points[i].enabled =  DamageElement[i].enabled = ElementBackground[i].enabled = true; 
            DamageElement[i].sprite = item.Properties[i].Icon;

            Points[i] = item.Properties[i].Text(Points[i]);
            
           
            Duration[i].text = item.Properties[i].Duration > 0
                ? item.Properties[i].Duration.ToString():
                "";
            ShowDamageStack(item);
          
              
        }
    }

    void ShowText(Item item)
    {
        Quantity.text = item.QuantityInBag > 1 ? item.QuantityInBag.ToString() : "";
        Name.text = item.Name;
    }
    public void Clear()
    {
        for (int i = 0; i < DamageElement.Length; i++)
        {
           DamageElement[i].enabled = ElementBackground[i].enabled = false;
            Points[i].enabled = false;
            foreach (var stack in Stack)
                stack.gameObject.SetActive(false);
        }

        foreach (var VARIABLE in Duration)
        {
            VARIABLE.text = "";
        }
        Name.text = Quantity.text  = "";
    }

    public void ShowDamageStack(Item item)
    {
        if (item.Damage.Using)
        {
            for (int i = 0; i < item.Damage.Stack.Length; i++)
            {
                if (item.Damage.Stack[i] > 0)
                {
                    Stack[i].gameObject.SetActive(true);
                    Stack[i].text = $"{item.Damage.Stack[i].ToString()}%";
                }
               
            }
           
        }
     }
}
