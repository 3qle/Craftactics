 using System;
 using UnityEngine;

[Serializable]
public class Stamina  : Attribute
{
   

   [HideInInspector] public bool OutOfStamina;
  
    public void Loose(int amount)
    {
        current -= amount;
    }
    
    public bool CheckForStamina(Item item)
    {
        OutOfStamina = item.SPCost + 1 > current ;
        return OutOfStamina;
    }

    public override void SetName()
    {
        Name = "STM";
    }
}
