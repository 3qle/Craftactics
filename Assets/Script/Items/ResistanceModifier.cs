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
    public override void Use(Character target, Field field)
    { 
        target.Resistance.ChangeResistance(Element,(int)Points,Duration);
    }

    public override TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        Icon = Resources.Load<Sprite>("Sprites/Affinity/" + Element);
       
        text.color = Points > 0 ? Color.green : Color.red;
        text.text = (Points >0? Points: -Points).ToString()+"%";
        return text;
    }
}
