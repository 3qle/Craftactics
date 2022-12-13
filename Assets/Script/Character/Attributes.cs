using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Attributes
{
    public Health health;
    public Stamina stamina;
    public Defense defense;
    public Dexterity dexterity;
    public Wisdom wisdom;
    public Intellect intellect;
    public Strenght strenght;
    public Luck luck;
    
   
    private Experience _experience;
    public List<Attribute> AttributesList;
    
    public void Initialize(Experience experience)
    {
        _experience = experience;
       SetAttributes();
    }

    public void SetAttributes()
    {
        AttributesList = new List<Attribute> {health, stamina,defense,dexterity,wisdom,intellect,strenght,luck};
        foreach (var stat in AttributesList)
        {
            stat.SetCurrentToMax();
            stat.SetName();
        }
         
    }

    public void LevelUp(int i)
    {
        _experience.SpendFreePoint();
        AttributesList[i].LevelUp();
        foreach (var stat in AttributesList) 
            stat.SetCurrentToMax();
    }
}
