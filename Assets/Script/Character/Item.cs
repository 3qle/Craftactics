using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Script.Character;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public class Item : MonoBehaviour
{
    private Attributes _attributes;
    
    [HideInInspector] public string Name;
    [HideInInspector]public int Damage;
    public int MinRange, MaxRange;
    public int SPCost;
    public int HitsCount;
    public Sprite WeaponIcon;
    
    public Element WeaponElement;
    private int _maxRange;
    private int _minRange;
    public int StrenghtMultiplier, DexterityMultiplier, WisdomMultiplier, IntellectMultiplier, DefenceMultiplier, LuckMultiplier;
    
    void Start()
    {
        Name = name;
    }

 
    public void Initialize(Character character)
    {
        _attributes = character.Attributes;
        ModifyDamage();
    }
    public void ModifyDamage()
    {
        Damage =  _attributes.Strenght * StrenghtMultiplier 
                  + _attributes.Dexterity * DexterityMultiplier 
                  + _attributes.Wisdom * WisdomMultiplier 
                  + _attributes.Intellect * IntellectMultiplier;
    }

   
    
}
