using System;
using System.Linq;
using Script.Enum;
using TMPro;
using UnityEngine;

[Serializable]
public class Shaper
{
    [HideInInspector] public Sprite Icon;
    public bool Chain;
    public float Points;
    public int Duration;
    public Trait Trait;
    public ShiftType type;
    public RangeType target;
    private float _startPoint;
    private DamageStack _stack;
    private Range _range;
    public void Initialize(Item item)
    {
        Icon = Resources.Load<Sprite>("Sprites/Traits/" + (type ==  ShiftType.Attack && Duration > 0? Trait + "Status" : Trait.ToString()));
        _stack = item.damageStack;
        _range = item.range;
    } 
    public  void UseActiveShapers(Character target,Character user,Item item, Pool pool)
    {
        if(type != ShiftType.Passive)ChooseTargets(target,user,item,pool);
    }

    public void UsePassiveShapers(Character target, Character user, Item item, Pool pool) => ChooseTargets(target,user,item,pool);
    

    void ChooseTargets(Character target, Character user, Item item, Pool pool)
    {
        switch (this.target)
        {
            case RangeType.Self: Shape(user,item); break;
            case RangeType.Enemy:
            case RangeType.Ally: Shape(target,item); break;
            case RangeType.AllEnemy: foreach (var enemy in user.entityType == EntityType.Enemy ? pool.ActiveHeroes : pool.EnemiesList) Shape(enemy,item); break;
            case RangeType.AllAlly: foreach (var hero in user.entityType == EntityType.Enemy?  pool.EnemiesList : pool.ActiveHeroes) Shape(hero,item); break;
        }
    }

    void Shape(Character target, Item item)
    {
        foreach (var attribute in target.Attributes.TraitList.Where(attribute => attribute.Trait == Trait))
            attribute.ChangeAttribute(this,item);
        
        if (!Chain) return;
        foreach (var attribute in _range.GetTargetForPostEffect(target).SelectMany(character => character.Attributes.TraitList.Where(attribute => attribute.Trait == Trait)))
            attribute.ChangeAttribute(this,item);
    }
     Color GetPointColor() 
         => target switch
         {
             RangeType.Ally => Color.blue,
             RangeType.Enemy => new Color(0.9f,0.2f,0.1f),
             RangeType.AllEnemy => Color.magenta ,
             RangeType.Self =>Color.green ,
             RangeType.AllAlly => Color.yellow,
             _ => new Color()
         };
     
    public  TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        text.text = type != ShiftType.Attack && type != ShiftType.Move ? Points >0? "+"+Points: Points.ToString(): Points.ToString();
        text.color = GetPointColor();
        return text;
    }

    public void ShiftPoints(Character user)
    {
        if (type != ShiftType.Attack ) return;
        if (_startPoint == 0) _startPoint = Points;
        Points = ShiftPoint(user);
    }


    float ShiftPoint(Character user)
    { 
        Points = 0; 
        return (float)Math.Round(Points += _stack.Calculate(user.Attributes) * (Duration != 0?0.1f:1),1);
    }
}


