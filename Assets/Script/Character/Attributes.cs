using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Attributes
{
    [HideInInspector]public Health health;
    [HideInInspector]public Stamina stamina;
    [HideInInspector]public Defence defence;
    [HideInInspector]public Dexterity dexterity;
    [HideInInspector]public Wisdom wisdon;
    [HideInInspector]public Intellect intellect;
    [HideInInspector]public Strenght strenght;
    [HideInInspector]public Luck luck;
    
    public int Health, Stamina, Defence, Dexterity, Wisdom, Intellect, Strenght, Luck;


    public void Initialize()
    {
        health.HP = Health;
        stamina.SP = Stamina;
    }
}
