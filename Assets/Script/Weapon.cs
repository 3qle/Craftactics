using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Script.Character;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class Weapon : MonoBehaviour,IWeapon
{
   [HideInInspector] public string Type;
    public int WeaponDamage;
    public int Min, Max;
    public int Cost;
    public int HitsCount;

    public static Action<IFightable, IWeapon, WeaponHand > ShowWeaponButton;
    public Element DamageElement;
    private int _maxRange;
    private int _minRange;

    void Start()
    {
        Type = name;
    }

    public void ShowWeapon(IFightable fightable,WeaponHand hand) 
        => ShowWeaponButton.Invoke(fightable,this,hand);

    public int Damage 
        => WeaponDamage;
    
    public Element WeaponElement
        => DamageElement;
    
    public int MaxRange 
        => Max;
    
    public int MinRange 
        => Min;
    
    public int ApCost 
        => Cost;

    public string Name
        => Type;
}
