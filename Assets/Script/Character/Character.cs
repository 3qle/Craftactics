using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Character.Attributes;
using Script.Enum;
using Script.Managers;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Character : Entity
{
    [HideInInspector]public Field field;
    protected UI _ui;
    public Sprite Face;
    protected Turn _turn;
    private Wallet _wallet;

    [Header("Character Settings")] public Sprite[] Faces;
    public Resistance Resistance;
    public Attributes Attributes;
    public WeaponMastery MasteryTypes;
    public Bag Bag;
    public Legs Legs;
    public Arms Arms;
    public static Action CharacterActed;
    [HideInInspector] public bool Selected;
    public List<Status> DamageStatusList = new List<Status>();
    public bool Active;
    public void SetPosition(Vector2 dir) => transform.position += (Vector3)dir;

    public override void Buy(Character character,bool buy)
    {
        Bought = true;
       // SetQuantity(buy);
       Active = buy;
        pool.AddCharacterToPool(this);
        _ui.InfoUI.AddCharacterToButton(this, pool.ActiveHeroes.IndexOf(this));
        transform.position = new Vector3(buy?pool.ActiveHeroes.IndexOf(this):transform.position.x, transform.position.y, buy ? 1 : -100);
        field.SetTileType(this, !buy);
    }
    
    public override void Initialize(Spawner starter, int i)
    {
        IndexOfCategory = i;
        name = Name;
        Icon = GetComponent<SpriteRenderer>();
        _ui = starter.ui;
        _turn = starter.turn;
        pool = starter.pool;
        field = starter.field;
        _wallet = starter.shop.Wallet;
       
        if (entityType == EntityType.Enemy)
        {
            
            field.SetTileType(this, false);
            GetWeapon();
            
        }
        Bag.Initialize(this,starter);
        InitializeAttributes();
       
    }

    void GetWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
            Bag.AllItems.Add(transform.GetChild(i).GetComponent<Item>());
    }

    public void InitializeAttributes()
    {
        Legs = new Legs(this);
        Arms = new Arms(this);
        
        Resistance.SetResistance();
        Attributes.Initialize();
       
    }
    public  void Move(Vector2 destination)
    {
        Attributes.stamina.Loose(1);
        StartCoroutine(Legs.Walk(destination));
        CharacterActed.Invoke();
    }
    
    public void PrepareForNewTurn()
    {
        Attributes.stamina.SetCurrentToMax();
       
    }

    public void CheckModifiers()
    {
     
      Attributes.CheckActiveModifiers();
      TakePassiveDamage();
    }
    public virtual void Select(bool selected)
    {
        Selected = selected;
        if (_turn.Act != TurnState.Shop)
        {
            field.CreateHighLight(this, selected);
            field.ShowWalkTile(this, selected);
        }
    }
    
    
    private IEnumerator Flash(int i)
    {
        int count = 3;
        while (count > 0)
        {
            Icon.color = i == 0 ? Color.green : i == 1 ? Color.red : Color.gray;
            yield return new WaitForSeconds(0.1f);
            Icon.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            count -= 1;
        }
    }
    
    public void TakeDamage(ItemDamage damage)
    {
       if(entityType == EntityType.Hero) 
           _ui.ScoreUI.SetScore(0 , _wallet.Coins);

       if (damage.Duration > 0)
       {
           DamageStatusList.Add(new Status(damage.Duration,damage.Icon, damage.StatusDamage,damage.WeaponElement));
           Attributes.SetStatus(DamageStatusList[DamageStatusList.Count - 1]);
       }
       SetDamage(damage.Points,damage.WeaponElement);
    }

    void SetDamage(float dam, Element element)
    {
        
        float damage = Attributes.health.Loose( (float)Math.Round(Resistance.CalculateDamage(dam,element)));
        
        _ui.ShowPopUp(Resistance.attackResult, transform.position, damage);
        
        Kill();
    }

    void TakePassiveDamage()
    {
        foreach (var damage in DamageStatusList)
            if (damage._duration > 0)
              SetDamage(damage._damage,damage.Element);
    }
    
    public void UseItem(List<Character> targets)
    {
        CharacterActed.Invoke();
        field.HideTiles();
        Attributes.stamina.Loose(Arms.selectedItem.staminaCost);
        if (!Arms.selectedItem.passive)
        {
            Arms.selectedItem.Use(targets);
        }
       
    }
    public virtual void Kill()
    {
        if (Attributes.health.isOver)
        { 
            field.SetTileType(this,true);
            if(entityType == EntityType.Hero)
                Buy(this,false);
            else 
                _turn.RemoveEnemy(this);
           
                
            pool.RemoveCharacterFromPool(this);
         
            _wallet.Add(_ui.ScoreUI.SetScore(1,_wallet.Coins));
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Flash(1));
        }
    }
}
