
using System;
using TMPro;
using UnityEngine;

[Serializable]
public class AttributeModifier : ItemProperty
{
    public AtrributeTypes attributeType;
  
    public bool TemporaryEffect;

    public override void Use(Character target, Field field)
    {
        foreach (var attribute in target.Attributes.AttributesList)
        {
            if(attribute.attributeType == attributeType)
                attribute.ChangeAttribute((int)Points, Duration);
        }
    }

    public override TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        text.text = Points > 0 ? Points.ToString() : (-Points).ToString();
        text.color = Points > 0 ? Color.green : Color.red;
        return text;
    } 
}


