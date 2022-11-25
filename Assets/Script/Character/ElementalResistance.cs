using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;

public class ElementalResistance : MonoBehaviour, IResistance
{
    public ResistanceType physicDefence, fireDefence, waterDefence, electricDefence, waveDefence, attackResult;
    public static Action<Dictionary<Element,ResistanceType>> ShowOnUI;
    private Dictionary<Element, ResistanceType> _resistances;

    private void Start()
    {
        SetResistance();
    }

    void SetResistance()
    {
        _resistances = new Dictionary<Element, ResistanceType>
        {
            {Element.Physic, physicDefence},
            {Element.Fire, fireDefence},
            {Element.Water, waterDefence},
            {Element.Electric, electricDefence},
            {Element.Wave, waveDefence},
        };
    }
    public void ShowResistance() => ShowOnUI.Invoke(_resistances);
    
  public int CalculateDamage(IWeapon element)
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
