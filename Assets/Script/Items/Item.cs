using System.Collections.Generic;
using System.Linq;
using Script.Character;
using Script.Enum;
using Script.Managers;
using UnityEngine;

public class Item : Entity
{
    private Field _field; 
    [Header("Item Settings")] 
    public ItemType itemType;
    public ProjectileType projectileType;
    public int maxCapacity, staminaCost;
    public bool instantUse, passive;// active;
    public bool consumable;
    private int _rawCost;
    [HideInInspector]public bool Selected;
    public ItemRange itemRange;
    public DamageStack damageStack;
    public PositionModifier PositionModifier;
    public List<AttributeModifier> attributeModifiers;
    public List<ResistanceModifier> resistanceModifiers;
    public List<ItemDamage> damageModifiers;
    public readonly List<ItemProperty> Properties = new List<ItemProperty>();
   public Item _weapon;
    private Character _user;
    private Console _console;
    public List<List<Item>> Projectiles = new List<List<Item>>();
 //   public int ProjectileIndex;
    public Item ActiveItem;
    private Turn _turn;
    private List<Item> ProjectileRoot = new List<Item>();



    public override bool TrySell() =>  (ActiveItem.itemType != ItemType.Projectile && Projectiles.Count == 0) || (ActiveItem.itemType == ItemType.Projectile);
       
    
    public override void Initialize(Spawner spawner, int i)
    {
        pool = spawner.pool;
        _console = spawner.ui.Console;
        IndexOfCategory = i;
        _field = spawner.field;
        _turn = spawner.turn;
   
    }
    
    protected  void Awake()
    {
        Icon = GetComponent<SpriteRenderer>();
        Icon.sprite =  Resources.Load<Sprite>("Sprites/Shop/Category/" + entityType);
        name = Name;
        SetProperties();
        transform.position = new Vector3(0, 0, -100);
        _rawCost = staminaCost;
        ActiveItem = this;
    }

    public void SetStamina()
    {
        staminaCost = _rawCost; 
        if (itemType == ItemType.Projectile)
        staminaCost +=_weapon.staminaCost;
    }
    private void SetProperties()
    {
     //   if(PositionModifier.Using) 
        //    Properties.Add(PositionModifier);
        foreach (var damage in damageModifiers.Where(property => property.Using))
            Properties.Add(damage.Initialize(this));
        foreach (var resistanceModifier in resistanceModifiers.Where(property => property.Using)) 
            Properties.Add(resistanceModifier);
        foreach (var attributeModifier in attributeModifiers.Where(property => property.Using)) 
            Properties.Add(attributeModifier);
        damageStack.Initialize();
        itemRange.Initialize();
      
       
    }

    public void ResetProjectiles()
    {
        ActiveItem = this;
        
    }
    public void AddProjectile(Item projectile)
    {
        var added=false;
        if(Projectiles.Count != 0)
            for (var index = 0; index < Projectiles.Count; index++)
            {
                if (projectile.Name == Projectiles[index][0].Name)
                {
                   
                    projectile.ProjectileRoot = Projectiles[index];
                    Debug.Log(Projectiles.IndexOf( projectile.ProjectileRoot));
                        Projectiles[index].Add(projectile);
                    added = true;
                }
            }
        projectile._weapon = this;
      
        Debug.Log(projectile._weapon);
        if ( added) return;
        Projectiles.Add(new List<Item>());
        Projectiles[Projectiles.Count-1].Add(projectile);
        projectile.ProjectileRoot = Projectiles[Projectiles.Count-1];
        Debug.Log(Projectiles.IndexOf( projectile.ProjectileRoot));

    }

    public bool ProjectilesFull(Item projectile)
    {
        return projectile.projectileType == projectileType
                ?Projectiles.Count >0? Projectiles.Sum(VARIABLE => VARIABLE.Count) >= maxCapacity
                : false: true;
        
    }

    public int GetProjectilesInWeaponAmount()
    {
        return  Projectiles.Count > 0
                ? Projectiles.Sum(VARIABLE => VARIABLE.Count)
                : 0;
    }

    public int GetProjectilesCount()
    {  
     
        return !Bought?0: _weapon.Projectiles[_weapon.Projectiles.IndexOf(ProjectileRoot)].Count;
      
    }
    public void SetProjectile(int i,Item weapon)
    {
       ActiveItem = Projectiles[i][0];
       
       ActiveItem._weapon = this;
            if(projectileType != ProjectileType.Spell)  ActiveItem.itemRange=itemRange;
            Projectiles[i][0].damageStack = damageStack;
          //  ActiveItem.ProjectileIndex = i;
            SetStamina();
        
    }
  
    public override void Buy(Character user, bool buy)
    {
        Bought = buy;
        _user = user;
        PositionModifier.Initialize(_user);
        var list = new List<Character> {user};
        if (buy)
        {
            if (itemType!= ItemType.Projectile)
                ChooseAction(list);
            
            if (user.Arms.selectedItem != null && itemType == ItemType.Projectile &&
                user.Arms.selectedItem.projectileType == projectileType && buy)
            {
                user.Arms.selectedItem.AddProjectile(this);
                ChooseAction(list);
            }

        }
        else Sell(user);
    }
    private void Sell(Character user)
    {
        if (itemType != ItemType.Projectile && Projectiles.Count == 0) 
            user.Bag.Remove(this);
        
        Debug.Log("sell");
        if(itemType == ItemType.Projectile)
        {
          
            _weapon.Projectiles[_weapon.Projectiles.IndexOf(ProjectileRoot)].Remove(this);
            user.Bag.Remove(this);
            if (_weapon.Projectiles[_weapon.Projectiles.IndexOf(ProjectileRoot)].Count == 0)
                _weapon.Projectiles.Remove(_weapon.Projectiles[_weapon.Projectiles.IndexOf(ProjectileRoot)]);
            _weapon.ActiveItem = _weapon;
            _console.ShowInfo(_weapon,user);
        }
    }

    private void ChooseAction(List<Character> users)
    {
        if (passive)
        {
            AddToBag(users);
            Use(users);
        }
        else
        {
            if (!instantUse) 
                AddToBag(users);
            else
            {
                Use(users);
            }
        }
    }

    private void AddToBag(List<Character> users)
    {
        foreach (var user in users) 
            user.Bag.AddItem(this);
    }
    public void Use(List<Character> targets)
    {
     
        var characters = targets.ToArray();
        foreach (var target in characters)
        foreach (var property in ActiveItem.Properties)
        {
         
            property.Use(target, _field, this);
        }


        if (ActiveItem.itemType == ItemType.Projectile)
        { 
            _user.Bag.Remove(ActiveItem);
           Projectiles[Projectiles.IndexOf(ActiveItem.ProjectileRoot)].Remove(ActiveItem);
            if (Projectiles[Projectiles.IndexOf(ActiveItem.ProjectileRoot)].Count == 0)
                Projectiles.Remove(Projectiles[Projectiles.IndexOf(ActiveItem.ProjectileRoot)]);
            ActiveItem = this;
           
        }
          
        _console.ShowInfo(this,_user);
    }
    
    public void Select(Character user)
    {
        if (Projectiles.Count == 0 && itemType != ItemType.Projectile)
        {
            ActiveItem = this;
            
        }
        
        _user = user;
       
        SetStamina();
        Selected = true;
        foreach (var damage in ActiveItem.damageModifiers) 
            damage.ModifyDamage(user); 
        if(user.entityType != EntityType.Enemy && _turn.Act != TurnState.Shop) ActiveItem.itemRange.Use(user,_field,this);
       
    }

    public void Deselect()
    {
        Selected = false;
    }
    public void SelectInShop(Character user)
    {
        ActiveItem = this;
        foreach (var damage in damageModifiers) 
            damage.ModifyDamage(user); 
    }
    
}