using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using TMPro;
using UnityEngine;
[Serializable]
public class ResistanceModifier :ItemProperty
{
    public Element Element;
    public AttributeModifier.BuffType BuffType;
    public override void Use(Character target, Field field, Item item)
    { 
        foreach (var attribute in target.Resistance.ResistanceClasses)
        {
            if(attribute.Element == Element)
                attribute.ChangeAttribute((int)Points, Duration, BuffType,target,item);
        }
    }

    public override TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        Icon = Resources.Load<Sprite>("Sprites/Affinity/" + Element);
       
        text.color = Points > 0 ? Color.green : Color.red;
        text.text = (Points >0? Points: -Points)+"%";
        return text;
    }

    public override float StatusDamageFill()
    {
        return 0;
    }
}
