using System;
using System.Collections;
using Script.Character.Attributes;
using Script.Enum;
using Script.Managers;
using UnityEngine;
public abstract class Character : Entity
{
    [HideInInspector]public Field field;
    protected UI _ui;
    public Sprite Face;
    protected Turn _turn;
    private Wallet _wallet;

    [Header("Character Settings")] public Sprite[] Faces;
    public Attributes Attributes;
    public WeaponMastery MasteryTypes;
    public Bag Bag;
    public Legs Legs;
    public Arms Arms;
    [HideInInspector] public bool Selected;
    public bool Active;

    private void Awake()
    {
      //  CharacterActed = null;
    }

    public void SetPosition(Vector2 dir) => transform.position += (Vector3)dir;

    public override void Buy(Character character,bool buy)
    {
        Bought = buy; 
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

        if (entityType == EntityType.Hero)
        {
            Faces = Resources.LoadAll<Sprite>("Sprites/Faces/" + Name);
        }
          

    }

    private void GetWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
            Bag.AllItems.Add(transform.GetChild(i).GetComponent<Item>());
    }

    private void InitializeAttributes()
    {
        Legs = new Legs(this);
        Arms = new Arms(this);
        Attributes.Initialize(this);
    }
    public  void Move(Vector2 destination)
    {
        Attributes.Get(Trait.Stamina).Loose(1);
        StartCoroutine(Legs.Walk(destination));
       _turn.CheckModifiers();
    }
    
    public void PrepareForNewTurn() => Attributes.Get(Trait.Stamina).SetCurrentToStart();
    public void CheckModifiers() =>StartCoroutine( Attributes.CheckActiveModifiers(transform.position));
    
    public virtual void Select(bool selected)
    {
        Selected = selected;
        if (_turn.Act ==TurnState.P &&  Attributes.Get(Trait.Health).IsMore(0) && Active)
        {
            Debug.Log($" turn.Act {_turn.Act} Health > 0 {Attributes.Get(Trait.Health).current} Active {Active} {Name}");
            field.CreateHighLight(this, selected);
            field.ShowWalkTile(this, selected);
        }
       
    }
    
    public void UseItem(Character target)
    {
        field.HideTiles();
        Attributes.Get(Trait.Stamina).Loose(Arms.selectedItem.staminaCost);
        Arms.selectedItem.Use(target);
       _turn.CheckModifiers();
    }

    public  void TryKill()
    {
        if (!Attributes.Get(Trait.Health).IsLowerZero) return;
        field.SetTileType(this,true);
        if(entityType == EntityType.Hero) Buy(this,false);
        else _turn.RemoveEnemy(this);
        pool.RemoveCharacterFromPool(this);
        _wallet.Add(_ui.ScoreUI.SetScore(1,_wallet.Coins));
        Destroy(gameObject);
    }
}
