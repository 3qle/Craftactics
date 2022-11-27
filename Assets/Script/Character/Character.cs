using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Managers;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Character : MonoBehaviour
{
    private SpriteRenderer _sprite;
    
    private UI _ui;
    [HideInInspector] public Field field;
    private Pool _pool;
    protected Turn _turn;
    
    public Health Health;
    public Stamina Stamina;
    public ElementalResistance Resistance;
    public Legs Legs;
    public Hands Hands;
    
    public string Name;
    
    public enum Fraction { Hero, Enemy, Boss }
    public Fraction side;
    
    public Vector2 Position => transform.position;
    
    public void SetPosition(Vector2 dir) => transform.position += (Vector3)dir;
    
    public void Initialize(Field field, UI ui, Pool pool, Turn turn)
    {
        _sprite = GetComponent<SpriteRenderer>();
        _ui = ui;
        this.field = field;
        _turn = turn;
        _pool = pool;
        _pool.AddCharacterToPool(this);
        Resistance.SetResistance();
        Health.SetMax();
        Hands.Initialize(this);
        SetStats();
        field.SetTileType(this, false);
    }
   
    public void SetStats()
    {
        name = Name;
        Stamina.SetMaxStamina();
    }
    
    public  void Move(Vector2 destination)
    {
        Stamina.Loose(1);
        StartCoroutine(Legs.Walk(destination,this));
    }
    
    public void PrepareForNewTurn()
    {
       Stamina.SetMaxStamina();
    }
 
    public virtual void Select(bool selected)
    {
        field.CreateHighLight(transform.position, selected);
      
        if(Stamina.SP > 0) 
           field.ShowWalkTile(this); 
      
       ShowInfo();
    }

    public void ShowInfo()
    {
        _ui.ShowResistances(Resistance._resistances); 
        _ui.ShowLeftWeapon(this,Hands._leftWeapon,WeaponHand.Left); 
        _ui.ShowRightWeapon(this,Hands._rightWeapon,WeaponHand.Right);
        _ui.ShowBaseInfo(Health.HP,Stamina.SP,Name);
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
    
    public void TakeDamage(Weapon weapon)
    {
        StartCoroutine(Flash(1));
        int damage = Health.Loose(Resistance.CalculateDamage(weapon));
        _ui.ShowPopUp(Resistance.GetAttackResult(), transform.position, damage);
        if(Health.isOver)Kill();
    }
    public Weapon Attack()
    {
        field.HideTiles();
        Stamina.Loose(Hands.SelectedWeapon.SPCost);
        return Hands.SelectedWeapon;
    }
    public void Kill()
    {
        _pool.RemoveCharacterFromPool(this);
        field.SetTileType(this,true);
        Destroy(gameObject);
    }
}
