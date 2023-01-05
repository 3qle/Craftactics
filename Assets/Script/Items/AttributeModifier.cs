
using System;
using TMPro;
using UnityEngine;

[Serializable]
public class AttributeModifier : ItemProperty
{
    public AtrributeTypes attributeType;

    public enum BuffType
    {
        Upgrade,Buff, Restore, Passive
    }

    public BuffType type;
    public override void Use(Character target, Field field, Item item)
    {
        foreach (var attribute in target.Attributes.AttributesList)
        {
            if(attribute.attributeType == attributeType)
                attribute.ChangeAttribute((int)Points, Duration, type,target,item);
        }
    }

    public override TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        Icon = Resources.Load<Sprite>("Sprites/Stats/" + attributeType);
        text.text = Points > 0 ? Points.ToString() : (-Points).ToString();
        text.color = Points > 0 ? Color.green : Color.red;
        return text;
    }

    public override float StatusDamageFill()
    {
        return 0;
    }
}


