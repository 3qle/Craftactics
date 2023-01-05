using System;
using Script.Character;
using Script.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemDamage : ItemProperty
{
    public Element WeaponElement;
    public float StatusDamage;
    private float bonus;
    [HideInInspector]public float raw, rawStatus;
    private Item _item;
    private Character user;
    public ItemProperty Initialize(Item item)
    {
        _item = item;
        Icon =  Duration > 0 ? Resources.Load<Sprite>("Sprites/Status/" + WeaponElement): Resources.Load<Sprite>("Sprites/Affinity/" + WeaponElement);
        raw = Points;
        rawStatus = StatusDamage;
       
        return this;
    }
    public void ModifyDamage(Character character)
    {
        user = character;
        bonus = ( character.Attributes.strength.current * _item.damageStack.StrenghtMultiplier 
                       +  character.Attributes.accuracy.current * _item.damageStack.AccuracyMultiplier 
                       +  character.Attributes.support.current * _item.damageStack.SupportMultiplier 
                       +  character.Attributes.magic.current * _item.damageStack.MagicMultiplier 
                       +  character.Attributes.flow.current * _item.damageStack.MusicMultiplier)
            / 100;
        Points = raw + bonus;
        StatusDamage = Points / 100 * rawStatus;
        StatusDamage =   (float)Math.Round(StatusDamage,1);
    }

    void UseProjectile()
    {
        
        foreach (var projectile in  user.Bag.Projectiles[user.Bag.Weapons.IndexOf(_item)])
        {
             WeaponElement = projectile.damageModifiers[0].WeaponElement;
                Duration = projectile.damageModifiers[0].Duration;
                StatusDamage = projectile.damageModifiers[0].StatusDamage;
                Points = projectile.damageModifiers[0].Points + bonus;
                Icon = projectile.damageModifiers[0].Icon;
               // projectile.QuantityInBag -= 1;
         
        }

       
    }
    public override void Use(Character target, Field field,Item item )
    {
        Debug.Log(target.name);
      target.TakeDamage(this);
      
    }

    public override TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        text.text = Duration > 0? $"{StatusDamage}": Points.ToString();
        text.color = Color.white;
        return text;
    }

    public override float StatusDamageFill()
    {
        float i = StatusDamage / Points;
        return i;
    }
}
