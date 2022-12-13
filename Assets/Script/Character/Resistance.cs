using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;
[Serializable]
public class Resistance 
{
    [Range(-100,100)]
    public int physicDefence, fireDefence, waterDefence, electricDefence, waveDefence,mentalDefence,holyDefence,IllDefence; 
    [HideInInspector]public AttackResult attackResult;
    public Dictionary<Element, int> _resistances;
    private int resistAmount;
    
   public void SetResistance()
    {
        _resistances = new Dictionary<Element, int>
        {
            {Element.Physic, physicDefence},
            {Element.Water, waterDefence},
            {Element.Fire, fireDefence},
            {Element.Electric, electricDefence},
            {Element.Wave, waveDefence},
            {Element.Mental, mentalDefence},
            {Element.Holy, holyDefence},
            {Element.Ill,IllDefence},
        };
    }
   
    
  public int CalculateDamage(Item item)
  {
      float damage;

        resistAmount = item.WeaponElement switch
        {
           Element.Electric => electricDefence,
           Element.Fire => fireDefence, 
           Element.Water => waterDefence,
           Element.Physic => physicDefence,
           Element.Wave => waveDefence,
           Element.Mental => mentalDefence,
           Element.Holy => holyDefence,
           Element.Ill => IllDefence,
           
        };
        damage = item.ModDamage - ((float)item.ModDamage * resistAmount/100);
        attackResult = 
            resistAmount < 0 ? AttackResult.Weak :
            resistAmount == 100 ? AttackResult.Absorb :
            resistAmount > 0 ? AttackResult.Block :
            AttackResult.Neutral;
        
      return (int)damage;
      
    }

  public int EvadeHit()
  {
      attackResult = AttackResult.Miss;
      return 0;
  }
    
}
