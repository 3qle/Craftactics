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
        text.text =$"{Points.ToString()}%";
        text.color = Points > 0 ? Color.green : Color.red;
        return text;
    }
}
