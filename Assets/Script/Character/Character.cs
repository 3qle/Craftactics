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
    private Pool _pool;
    protected Turn _turn;
    private Wallet _wallet;

    [Header("Character Settings")]
    public Resistance Resistance;
    public Attributes Attributes;
    public WeaponMastery MasteryTypes;
    public Bag Bag;
    public Legs Legs;
    public Arms Arms;

    [HideInInspector] public bool Selected;

    public void SetPosition(Vector2 dir) => transform.position += (Vector3)dir;

    public override void Buy(Character character,bool buy)
    { 
        SetQuantity(buy);
        _pool.AddCharacterToPool(this);
        _ui.InfoUI.AddCharacterToButton(this, _pool.ActiveHeroes.IndexOf(this));
        transform.position = new Vector3(buy?_pool.ActiveHeroes.IndexOf(this):transform.position.x, transform.position.y, buy ? 1 : -100);
        field.SetTileType(this, !buy);
    }
    
    public override void Initialize(Spawner starter)
    {
        name = Name;
        Icon = GetComponent<SpriteRenderer>();
        _ui = starter.ui;
        _turn = starter.turn;
        _pool = starter.pool;
        field = starter.field;
        _wallet = starter.shop.Wallet;
        if (entityType == EntityType.Enemy)
        {
            field.SetTileType(this, false);
        }
        InitializeAttributes();
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
    }
    
    public void PrepareForNewTurn()
    {
        Attributes.stamina.SetCurrentToMax();
        Attributes.CheckActiveModifiers();
        Resistance.CheckActiveModifiers();
    }
 
    public virtual void Select(bool selected)
    {
        Selected = selected;
        if (_turn.Act != TurnState.Shop)
        {
            field.CreateHighLight(this, selected);
            field.ShowWalkTile(this, selected);
         //   Arms.DeselectWeapon(selected);
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
    
    public void TakeDamage(ItemDamage weapon)
    {
       if(entityType == EntityType.Hero) 
           _ui.ScoreUI.SetScore(0 , _wallet.Coins);
       
      
       float damage = Attributes.health.Loose( (float)Math.Round(!Attributes.accuracy.TryEvade()
            ? Resistance.CalculateDamage(weapon)
            : Resistance.EvadeHit(), 1));
       
       _ui.ShowPopUp(Resistance.attackResult, transform.position, damage); 
       
       Kill();
    }
    public ItemDamage Attack()
    {
        field.HideTiles();
        Attributes.stamina.Loose(Arms.selectedItem.SPCost);
        return Arms.selectedItem.Damage;
    }

    public void UseItem(Character target)
    {
        field.HideTiles();
        Attributes.stamina.Loose(Arms.selectedItem.SPCost);
        Arms.selectedItem.Use(target);
        }
    private void Kill()
    {
        if (Attributes.health.isOver)
        { 
            field.SetTileType(this,true);
            if(entityType == EntityType.Hero)
                Buy(this,false);
            _pool.RemoveCharacterFromPool(this);
         
            _wallet.Add(_ui.ScoreUI.SetScore(50,_wallet.Coins));
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Flash(1));
        }
    }
}
