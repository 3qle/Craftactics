using System;
using System.Collections.Generic;
using System.Linq;
using Script.Character;
using Script.Enum;
using UnityEngine;

[Serializable]
public class Bag
{
    public List<Entity> Stash = new List<Entity>();
    public List<Item> AllItems = new List<Item>();
    private Character _character;
    [HideInInspector]  public List<Item> Weapons,Items = new List<Item>();
    public List<List<Item>> Projectiles = new List<List<Item>>();
    public List<Item> SelectedProjectiles = new List<Item>();
    private const int WeaponSlots = 2, ItemSlots = 3;
   
    public void Initialize(Character character, Spawner starter)
    {
        _character = character;
        for (int i = 0; i < 2; i++)
        {
            Projectiles.Add(new List<Item>());
        }

        if (_character.entityType == EntityType.Enemy)
        {
            for (int i = 0; i < AllItems.Count; i++) 
                AllItems[i].Initialize(starter,i);
        }
           
    }
    
    
    public bool HasWeaponSlot => Weapons.Count < WeaponSlots;
    public bool HasItemSlot => Items.Count < ItemSlots;

    
    int RightContainerCapacity(ProjectileType type)
    {
        var capacity = 0;
        foreach (var container in Weapons.Where(container => container.projectileType != ProjectileType.No && container.projectileType == type))
            capacity = container.maxCapacity;
       
        return capacity;
    }
    
   
    public void AddItem(Item item)
    {
        if (!AllItems.Contains(item)) 
            ChooseSlot(item);
    
        if(item.itemType == ItemType.Weapon)
            _character.Arms.SelectWeapon(item,_character);
    }

    void ChooseSlot(Item item)
    {
        if (item.itemType == ItemType.Weapon)
            Weapons.Add(item);

        if (item.itemType == ItemType.Item) 
            Items.Add(item);
        
        if(item.itemType != ItemType.Projectile)
            AllItems.Add(item);
        
        Stash.Add(item);
    }

    
    public void Remove(Item item)
    {
        Stash.Remove(item);
        if (item.itemType == ItemType.Projectile) return;
        AllItems.Remove(item);
        if (item.itemType == ItemType.Weapon) Weapons.Remove(item);
        else Items.Remove(item);
        _character.Attributes.CheckPassive(item);
    }
    
    
}
