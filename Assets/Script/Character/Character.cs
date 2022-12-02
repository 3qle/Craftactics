using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Managers;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Character : MonoBehaviour
{
    [HideInInspector]public SpriteRenderer _sprite;
    
    private UI _ui;
    [HideInInspector] public Field field;
    private Pool _pool;
    protected Turn _turn;

    public Attributes Attributes;
    public Bag Bag;
    public ElementalResistance Resistance;
    public Legs Legs;
    public Hands Hands;
    
    public string Name;
    
    public enum Fraction { Hero, Enemy, Boss }
    public Fraction side;
    private bool _selected;
    public Vector2 Position => transform.position;
    
    public void SetPosition(Vector2 dir) => transform.position += (Vector3)dir;
    
    
    public void Initialize(Field _field, UI ui, Pool pool, Turn turn)
    {
        _sprite = GetComponent<SpriteRenderer>();
        _ui = ui;
        _turn = turn;
        _pool = pool;
        field = _field;
        InitializeBag();
        Resistance.SetResistance();
        Attributes.Initialize();
         _pool.AddCharacterToPool(this);
        field.SetTileType(this, false);
        
        name = Name;
     
        
        Hands.Initialize(this);
        
       
    }
   
   
    public  void Move(Vector2 destination)
    {
        Attributes.stamina.Loose(1);
        StartCoroutine(Legs.Walk(destination,this));
    }
    
    public void PrepareForNewTurn()
    {
        Attributes.stamina.SetMax(Attributes.Stamina);
    }
 
    public virtual void Select(bool selected)
    {
        _selected = selected;
        field.CreateHighLight(transform.position, selected);
      
        if( Attributes.stamina.SP > 0 && side == Fraction.Hero) 
           field.ShowWalkTile(this);


        if (!_selected)
        {
            _ui.Console.Clear();
           
        }
        if (side == Fraction.Hero)
            _ui.HighLightCharacterButton(_pool.HeroesList.IndexOf(this),_selected);
    }

    public void ShowInfo()
    {
       
       
  
        
        
        _ui.ShowResistances(Resistance._resistances); 
        _ui.ShowActiveItems(this); 
      
        _ui.ShowBaseInfo(this);
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
    
    public void TakeDamage(Item item)
    {
        StartCoroutine(Flash(1));
        int damage = Attributes.health.Loose(Resistance.CalculateDamage(item));
        _ui.ShowPopUp(Resistance.GetAttackResult(), transform.position, damage);
        if(Attributes.health.isOver)Kill();
    }
    public Item Attack()
    {
        field.HideTiles();
        Attributes.stamina.Loose(Hands.selectedItem.SPCost);
        return Hands.selectedItem;
    }
    public void Kill()
    {
        _pool.RemoveCharacterFromPool(this);
        field.SetTileType(this,true);
        Destroy(gameObject);
    }

    public void InitializeBag()
    {
        for (int i = 0; i < transform.childCount; i++)
            Bag.Initialize(transform.GetChild(i).GetComponent<Item>());
        
    }
}
