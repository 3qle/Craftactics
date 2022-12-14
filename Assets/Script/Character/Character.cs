using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Character.Attributes;
using Script.Managers;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Character : MonoBehaviour
{
   public SpriteRenderer _sprite;
    [HideInInspector]public Field field;
    private UI _ui;
    private Pool _pool;
    protected Turn _turn;
    public WeaponMastery MasteryTypes;
    public Experience Experience;
    public Attributes Attributes;
    public Bag Bag;
    public Resistance Resistance;
    public Legs Legs;
    public Arms Arms;
    private Controller _controller;
    private UIShop _shop;
    public string Name;
    
    public enum Fraction { Hero, Enemy, Boss }
    public Fraction side;
    public bool Bought;
   

    public int Index,Cost;
    public void SetPosition(Vector2 dir) => transform.position += (Vector3)dir;

    public void Enable(CharacterButton button)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
       _ui.UICharacter.AddCharacterToButton(this,button);
       field.SetTileType(this, false);
       Index = _pool.HeroesList.IndexOf(this);
    }
    public void Initialize(Field _field, UI ui, Pool pool, Turn turn, Controller controller,UIShop shop)
    {
        _shop = shop;
        name = Name;
        _sprite = GetComponent<SpriteRenderer>();
        _ui = ui;
        _turn = turn;
        _pool = pool;
        field = _field;
        _controller = controller;
        
       
        _pool.AddCharacterToPool(this);
        if (side == Fraction.Enemy) 
            field.SetTileType(this, false);
        
        InitializeAtrributes(ui);
 
    }

    public void InitializeAtrributes(UI ui)
    {
        Legs = new Legs(this);
        Arms = new Arms(this);
        
        Resistance.SetResistance();
        Attributes.Initialize(Experience);
        Experience.Initialize(ui,this);
    }
    public  void Move(Vector2 destination)
    {
        Attributes.stamina.Loose(1);
        StartCoroutine(Legs.Walk(destination));
    }
    
    public void PrepareForNewTurn()
    {
        Attributes.stamina.SetCurrentToMax();
    }
 
    public virtual void Select(bool selected)
    {
        Debug.Log(transform.position);
        field.CreateHighLight(this, selected);
        field.ShowWalkTile(this, selected);
        Arms.DeselectWeapon(selected);
    }
    
    
    private IEnumerator Flash(int i)
    {
        int count = 3;
        while (count > 0)
        {
            _sprite.color = i == 0 ? Color.green : i == 1 ? Color.red : Color.gray;
            yield return new WaitForSeconds(0.1f);
            _sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            count -= 1;
        }
    }
    
    public int TakeDamage(Character character)
    {
        StartCoroutine(Flash(1));
        int damage = Attributes.health.Loose(!Attributes.dexterity.TryEvade()? Attributes.defense.Calculate(Resistance.CalculateDamage(character.Arms.selectedItem)): Resistance.EvadeHit());
        _ui.ShowPopUp(Resistance.attackResult, transform.position, damage); 
        Kill();
        return Attributes.health.isOver ?Experience.reward: 0;
    }
    public Character Attack()
    {
        
        field.HideTiles();
        Attributes.stamina.Loose(Arms.selectedItem.SPCost);
        return this;
    }

    private void Kill()
    {
        if (Attributes.health.isOver)
        {
            _pool.RemoveCharacterFromPool(this);
            field.SetTileType(this,true);
            Destroy(gameObject);
        }
    }

   
}
