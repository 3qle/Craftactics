using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalResistance : MonoBehaviour, IResistance
{ 
    public enum Resistance { Absorb, Block, Neutral, Weak }
    public Resistance physicDefence, fireDefence, waterDefence, electricDefence, waveDefence, attackResult;
    
    public int CalculateDamage(IWeapon element)
    {
        int damage = element.GetDamageAmount();
        
        attackResult = element.GetWeaponElement() switch
        {
            Weapon.Element.Electric => electricDefence,
            Weapon.Element.Fire => fireDefence,
            Weapon.Element.Water => waterDefence,
            Weapon.Element.Physic => physicDefence,
            Weapon.Element.Wave => waveDefence,
        };
        damage = attackResult switch
        {
            Resistance.Weak => damage * 2,
            Resistance.Block => 0,
            Resistance.Absorb => -damage,
            Resistance.Neutral => damage,
        };
        return damage;
    }

    public Resistance GetAttackResult()=> attackResult;
    
    public Resistance GetResistance(int num)
        => num switch
        {
            0 => physicDefence,
            2 => fireDefence,
            1 => waterDefence,
            3 => electricDefence,
            4 => waveDefence,
        };
}
