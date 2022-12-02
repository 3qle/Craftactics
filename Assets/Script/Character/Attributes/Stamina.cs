 using System;
 using UnityEngine;

 [Serializable]
public class Stamina
{
   
  [HideInInspector]  public int SP;
    public bool OutOfStamina;
    public void SetMax(int sp)
    {
        SP = sp;
    }

    public void Loose(int amount)
    {
        SP -= amount;
    }
    public bool CheckForStamina(Item item)
    {
        OutOfStamina = item.SPCost > SP + 1;
        return OutOfStamina;
    }
}
