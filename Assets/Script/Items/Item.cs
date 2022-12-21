using System;
using System.Collections.Generic;
using System.Linq;
using Script.Character;
using Script.Enum;
using UnityEngine;

public class Item : Entity
{
    [Header("Item Settings")]
    public int SPCost;
    public List<AttributeModifier> AttributeModifiers;
    public List<ResistanceModifier> ResistanceModifiers;
    public ItemDamage Damage;
    public ItemRange Range;
    private Field _field;
    public List<ItemProperty> Properties = new List<ItemProperty>();
    

    protected  void Awake()
    {
        Icon = GetComponent<SpriteRenderer>();
        name = Name;
        SetProperties();
        transform.position = new Vector3(0, 0, -100);
    }
   
    void SetProperties()
    {
        if(Damage.Using) 
            Properties.Add(Damage); 
      //  if(Range.Using) 
          //  Properties.Add(Range); 
        foreach (var property in AttributeModifiers.Where(property => property.Using)) 
            Properties.Add(property);

        foreach (var resistanceModifier in ResistanceModifiers) 
            Properties.Add(resistanceModifier);
            
    }
   
    
    public override void Buy(Character user, bool buy)
    {
        if (buy) ChooseAction(user);
        else Sell(user);
    }

    void Sell(Character user)
    {
        user.Bag.Remove(this);
    }

    void ChooseAction(Character user)
    {
        if (!instantUse )
            user.Bag.AddItem(this);
        else
        {
            SetQuantity(true);
            Use(user);
        }
           
    }
    public void Use(Character target)
    {
        foreach (var property in Properties) 
            property.Use(target, _field);
    }

    public void Select(Character user)
    {
        Damage.ModifyDamage(user);
       if(user.entityType != EntityType.Enemy) Range.Use(user,_field);
    }

    public void SelectInShop(Character user)
    {
        Damage.ModifyDamage(user);
    }
    public override void Initialize(Spawner spawner)
    {
        _field = spawner.field;
       
    }
   
   


}
