using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;
[Serializable]
public class Resistance 
{
 
    [HideInInspector]public AttackResult attackResult;
   
    private int resistAmount;
    private int _duration;
   public ResistanceClass physicDefence, fireDefence, waterDefence, electricDefence, waveDefence,mentalDefence,holyDefence,IllDefence;
   public ResistanceClass[] ResistanceClasses;
   public void SetResistance()
   {
       ResistanceClasses = new[]
       {
           physicDefence, fireDefence, waterDefence, electricDefence, waveDefence, mentalDefence, holyDefence,
           IllDefence
       };
       foreach (var VARIABLE in ResistanceClasses) 
           VARIABLE.Set();
   }

   public void ChangeResistance(Element key,int amount,int duration)
   {
       foreach (var res in ResistanceClasses) 
           if(res.Element == key)res.Add(amount,duration);
   }

   public void CheckActiveModifiers()
   {
       foreach (var VARIABLE in ResistanceClasses) 
           VARIABLE.CheckDuration();
   }
  public float CalculateDamage(ItemDamage item)
  {
      foreach (var resistance in ResistanceClasses) 
          if (resistance.Element == item.WeaponElement)
              resistAmount = resistance.amount;
      
       float damage = item.Points - item.Points * resistAmount/100;
        attackResult = 
            resistAmount < 0 ? AttackResult.Weak :
            resistAmount == 100 ? AttackResult.Absorb :
            resistAmount > 0 ? AttackResult.Block :
            AttackResult.Neutral;
        
      return damage;
      
    }

  public float EvadeHit()
  {
      attackResult = AttackResult.Miss;
      return 0;
  }
    
}
