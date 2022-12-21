using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public class Attributes
{
    public Health health;
    public Stamina stamina;
    public Accuracy accuracy;
    public Support support;
    public Magic magic;
    public Strenght strength;
    public Flow flow;
    
   
    public List<Attribute> AttributesList;
    
    public void Initialize()
    {
        SetAttributes();
    }

    public void SetAttributes()
    {
        AttributesList = new List<Attribute> {strength,accuracy,magic,support,flow, health, stamina};
        foreach (var stat in AttributesList)
        {
            stat.SetCurrentToMax();
            stat.SetName();
        } 
        stamina.SetName(); 
        health.SetName();
    }

    public void CheckActiveModifiers()
    {
        foreach (var attribute in AttributesList) 
            attribute.DecreaseDuration();
    }
}
