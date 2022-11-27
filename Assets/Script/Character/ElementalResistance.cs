using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;
[Serializable]
public class ElementalResistance 
{
    
    public ResistanceType physicDefence, fireDefence, waterDefence, electricDefence, waveDefence, attackResult;
    public Dictionary<Element, ResistanceType> _resistances;

  
   public void SetResistance()
    {
        _resistances = new Dictionary<Element, ResistanceType>
        {
            {Element.Physic, physicDefence},
            {Element.Water, waterDefence},
            {Element.Fire, fireDefence},
            {Element.Electric, electricDefence},
            {Element.Wave, waveDefence},
        };
    }

    public void ShowResistance()
    {
        
    }
    
  public int CalculateDamage(Weapon element)
    {
        int damage = element.Damage;
        
        attackResult = element.WeaponElement switch
        {
           Element.Electric => electricDefence,
           Element.Fire => fireDefence, 
           Element.Water => waterDefence,
           Element.Physic => physicDefence,
           Element.Wave => waveDefence,
        };
        damage = attackResult switch
        {
            ResistanceType.Weak => damage * 2,
            ResistanceType.Block => 0,
            ResistanceType.Absorb => -damage,
            ResistanceType.Neutral => damage,
        };
        return damage;
    }

    public ResistanceType GetAttackResult()=> attackResult;
    
}
