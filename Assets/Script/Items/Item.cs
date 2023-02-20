using System.Collections.Generic;
using System.Linq;
using Script.Character;
using Script.Enum;
using Script.Managers;
using UnityEngine;

public class Item : Entity
{
    private Console _console;
    private Turn _turn;
    public Character _user;
    [HideInInspector] public Item ActiveItem,_weapon;
    [Header("Item Settings")] 
    public ItemType itemType;
    public ProjectileType projectileType;
    public int maxCapacity, staminaCost;
    public bool instantUse,  projectileRange, consumable;
    public Range range;
    public DamageStack damageStack;
    private int _rawCost;
    public List<Shaper> Shapers;
    private List<Item> _projectileRoot = new List<Item>();
    public List<List<Item>> Projectiles = new List<List<Item>>();
    
    public override bool TrySell() 
        =>  ActiveItem.itemType != ItemType.Projectile && Projectiles.Count == 0 || ActiveItem.itemType == ItemType.Projectile;
    
    public override void Initialize(Spawner spawner, int i)
    {
        pool = spawner.pool;
        _console = spawner.ui.Console;
        IndexOfCategory = i;
        _turn = spawner.turn;
        foreach (var shaper in Shapers) 
            shaper.Initialize(this);
        range.Initialize(spawner.field);
    }
    
    protected  void Awake()
    {
        Icon = GetComponent<SpriteRenderer>();
        Icon.sprite =  Resources.Load<Sprite>(entityType == EntityType.EnemyWeapon? "Sprites/Traits/" + Shapers[0].Trait : "Sprites/Shop/Category/" + entityType);
        name = Name;
        transform.position = new Vector3(0, 0, -100);
        _rawCost = staminaCost;
        ActiveItem = this;
        damageStack.Initialize();
    }

    private void SetStamina()
    {

        if (ActiveItem.itemType == ItemType.Projectile)
        {
            ActiveItem.staminaCost = ActiveItem._rawCost; 
            ActiveItem.staminaCost =+staminaCost;
        }
           
    }

    public void AddProjectile(Item projectile)
    {
        var added=false;
        if(Projectiles.Count != 0)
            foreach (var list in Projectiles)
            {
                if (projectile.Name == list[0].Name)
                {
                    projectile._projectileRoot = list;
                    list.Add(projectile);
                    added = true;
                    foreach (var item in Shapers.Where(item => !list[0].Shapers.Contains(item)))
                        list[0].Shapers.Add(item);
                }
            }
        projectile._weapon = this;
        if ( added) return;
        Projectiles.Add(new List<Item>());
        Projectiles[Projectiles.Count-1].Add(projectile);
        projectile._projectileRoot = Projectiles[Projectiles.Count-1];
        foreach (var item in Shapers)
            Projectiles[Projectiles.Count-1][0].Shapers.Add(item);
    }

    public bool ProjectilesFull(Item projectile)
        => projectile.projectileType == projectileType
                ? Projectiles.Count >0
                ? Projectiles.Sum(VARIABLE => VARIABLE.Count) >= maxCapacity 
                : false: true;
    

    public int GetProjectilesInWeaponAmount() => Projectiles.Count > 0 ? Projectiles.Sum(VARIABLE => VARIABLE.Count) : 0;
    
    public int GetProjectilesCount() => !Bought?0: _weapon.Projectiles[_weapon.Projectiles.IndexOf(_projectileRoot)].Count;
    
    public void SetProjectile(int i,Character user)
    {
        
       ActiveItem = Projectiles[i][0];
       ActiveItem._weapon = this;
       SetStamina();
      
       if(!projectileRange)  
            ActiveItem.range=range;
        
        Projectiles[i][0].damageStack.Stack= damageStack.Stack;
        foreach (var shaper in  Projectiles[i][0].Shapers) 
            shaper.Initialize(this);
        
        Debug.Log($"{user.Attributes.Get(Trait.Stamina).current < ActiveItem.staminaCost}  {user.Attributes.Get(Trait.Stamina).current} {ActiveItem.staminaCost} ffff");
        Select(user,true);
    }
  
    public override void Buy(Character user, bool buy)
    {
        Bought = buy;
        _user = user;
        if (buy)
        {
            ChooseAction(user);
            SetPassives();
            if (user.Arms.selectedItem == null || itemType != ItemType.Projectile ||
                user.Arms.selectedItem.projectileType != projectileType || !buy) return;
            
            user.Arms.selectedItem.AddProjectile(this);
        }
        else Sell(user);
    }
    private void Sell(Character user)
    {
        if (itemType != ItemType.Projectile && Projectiles.Count == 0) 
            user.Bag.Remove(this);
        
        if (itemType != ItemType.Projectile) return;
        foreach (var shaper in Shapers.Where(shaper => _weapon.Shapers.Contains(shaper)))
         Shapers.Remove(shaper);
        
        _weapon.Projectiles[_weapon.Projectiles.IndexOf(_projectileRoot)].Remove(this);
        user.Bag.Remove(this);
        
        if (_weapon.Projectiles[_weapon.Projectiles.IndexOf(_projectileRoot)].Count == 0)
            _weapon.Projectiles.Remove(_weapon.Projectiles[_weapon.Projectiles.IndexOf(_projectileRoot)]);
        _weapon.ActiveItem = _weapon;
        _console.ShowInfo(_weapon);
    }

    private void ChooseAction(Character user)
    {
        if(instantUse)  Use(user);
        else user.Bag.AddItem(this);
    }

    void SetPassives()
    {
        foreach (var shaper in Shapers.Where(shaper => shaper.type == ShiftType.Passive))
            shaper.UsePassiveShapers(_user,_user,this,pool);
    }
    public void Use(Character target)
    {
        foreach (var property in ActiveItem.Shapers) 
            property.UseActiveShapers(target, _user,this, pool);
        
        if(consumable) _user.Bag.Remove(this);
        
        if (ActiveItem.itemType == ItemType.Projectile)
        { 
            _user.Bag.Remove(ActiveItem);
            int i = Projectiles.IndexOf(ActiveItem._projectileRoot);
           Projectiles[i].Remove(ActiveItem);
            if (Projectiles[i].Count == 0)
                Projectiles.Remove(Projectiles[i]);
            ActiveItem = this;
        }
        _console.ShowInfo(this);
    }

   
    public void Select(Character user, bool projectile)
    { 
        if(!projectile) ActiveItem = this;
        _user = user;
        SetStamina();
    
        foreach (var damage in ActiveItem.Shapers.Where(item => item.type == ShiftType.Attack)) 
                damage.ShiftPoints(user); 
        if(user.Attributes.Get(Trait.Stamina).current < ActiveItem.staminaCost) return;
        if(user.entityType != EntityType.Enemy && _turn.Act != TurnState.Shop && !(projectileType != ProjectileType.No && projectileType != ProjectileType.Oil && ActiveItem == this)) 
            ActiveItem.range.Use(user);
    }
    
    public void SelectInShop(Character user)
    {
        ActiveItem = this;
        foreach (var damage in ActiveItem.Shapers.Where(item => item.type == ShiftType.Attack)) 
            damage.ShiftPoints(user); 
    }
    
}