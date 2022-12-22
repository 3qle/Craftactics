using System;
using Script.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemDamage : ItemProperty
{
    public Element WeaponElement;
  
    public int StrenghtMultiplier, AccuracyMultiplier, SupportMultiplier, MagicMultiplier, MusicMultiplier;
    [HideInInspector]public float raw;
    public int[] Stack;
    public void ModifyDamage(Character character)
    {
        Icon = Resources.Load<Sprite>("Sprites/Affinity/" + WeaponElement);
        if (raw == 0) raw = Points;
        Stack = new[] { StrenghtMultiplier,AccuracyMultiplier,MagicMultiplier,SupportMultiplier,MusicMultiplier };
        Points = raw +( character.Attributes.strength.current * StrenghtMultiplier 
                        +  character.Attributes.accuracy.current * AccuracyMultiplier 
                        +  character.Attributes.support.current * SupportMultiplier 
                        +  character.Attributes.magic.current * MagicMultiplier 
                        +  character.Attributes.flow.current * MusicMultiplier)
            / 100;
      
       
    }

    public override void Use(Character target, Field field )
    {
      target.TakeDamage(this);
      
    }

    public override TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        text.text = Points.ToString();
        text.color = Color.white;
        return text;
    }
}
