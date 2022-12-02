using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Attributes
{
    [HideInInspector]public Health health;
    [HideInInspector]public Stamina stamina;
    [HideInInspector]public Defense defense;
    [HideInInspector]public Dexterity dexterity;
    [HideInInspector]public Wisdom wisdom;
    [HideInInspector]public Intellect intellect;
    [HideInInspector]public Strenght strenght;
    [HideInInspector]public Luck luck;
    
    [Range(0,25)] 
    public int Health, Stamina, Defence, Dexterity, Wisdom, Intellect, Strenght, Luck;


    public void Initialize()
    {
        health.HP = Health;
        stamina.SP = Stamina;
        defense.DEF = Defence;
    }
}
