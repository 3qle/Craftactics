using System;
using Script.Character;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Attributes _attributes;
    
    public string Name;
    public int Damage;
    public int MinRange, MaxRange;
    public int SPCost, ShopCost;
    public int HitsCount;
    public Sprite WeaponIcon;
    [HideInInspector] public int ModDamage;
    public Element WeaponElement;
    private int _maxRange;
    private int _minRange;
    public int StrenghtMultiplier, DexterityMultiplier, WisdomMultiplier, IntellectMultiplier, DefenceMultiplier, LuckMultiplier;
    public int strBonus;
   
    private void Start()
    {
        name = Name;
    }

    public void Initialize(Character character)
    {
        _attributes = character.Attributes;
        ModifyDamage();
    }
    public void ModifyDamage()
    {
       ModDamage = Damage +( _attributes.strenght.current * StrenghtMultiplier 
                  + _attributes.dexterity.current * DexterityMultiplier 
                  + _attributes.wisdom.current * WisdomMultiplier 
                  + _attributes.intellect.current * IntellectMultiplier)
                  / 100;
       strBonus =(_attributes.strenght.current * StrenghtMultiplier)/100;
    }

   
    
}
