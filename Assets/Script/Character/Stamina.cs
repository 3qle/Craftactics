 using System;
 using UnityEngine;

 [Serializable]
public class Stamina
{
    public int MaxSP;
  [HideInInspector]  public int SP;
    public bool OutOfStamina;
    public void SetMaxStamina()
    {
        SP = MaxSP;
    }

    public void Loose(int amount)
    {
        SP -= amount;
    }
    public bool CheckForStamina(Weapon weapon)
    {
        OutOfStamina = weapon.SPCost > SP + 1;
        return OutOfStamina;
    }
}
