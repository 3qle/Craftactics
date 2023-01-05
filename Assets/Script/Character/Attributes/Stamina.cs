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
        var i = item.staminaCost;
        OutOfStamina = i > current ;
        return OutOfStamina;
    }

    public override void SetName()
    {
        Name = "Stamina";
        start = max;
        SetCurrentToMax();
        attributeType = AtrributeTypes.Stamina;
        notShow = true;
    }
}
