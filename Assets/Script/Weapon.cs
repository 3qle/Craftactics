using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour,IWeapon
{
   [HideInInspector] public string Type;
    public int Damage;
    public int Min, Max;
    public int Cost;
    public int HitsCount;
    public enum Element { Physic, Fire, Water,Wave,Electric }

    public Element DamageType;
    void Start()
    {
        Type = name;
    }
    
    public Element GetWeaponElement()
        => DamageType;

    public int GetDamageAmount() => Damage;

    public int DecreaseAP()=> Cost;
    public int MaxRange() => Max;
    public int MinRange() => Min;
    public string GetName() => name;

    public int GetCost() => Cost;
}
