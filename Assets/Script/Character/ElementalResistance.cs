using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;
[Serializable]
public class ElementalResistance 
{
    
    public ResistanceType physicDefence, fireDefence, waterDefence, electricDefence, waveDefence,mentalDefence,holyDefence,IllDefence; 
    private ResistanceType attackResult;
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
            {Element.Mental, mentalDefence},
            {Element.Holy, holyDefence},
            {Element.Ill,IllDefence},
        };
    }

    public void ShowResistance()
    {
        
    }
    
  public int CalculateDamage(Item element)
    {
        int damage = element.Damage;
        
        attackResult = element.WeaponElement switch
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
