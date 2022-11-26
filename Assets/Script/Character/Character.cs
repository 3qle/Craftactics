using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Managers;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Character : MonoBehaviour, IFightable
{
    private UI _ui;
    private SpriteRenderer _sprite;
    protected IViewable _field;
    private Pool _pool;
    public Health Health;
    public ElementalResistance Resistance;
    private Weapon _leftWeapon, _rightWeapon;
    public IWeapon SelectedWeapon;
    protected IWeapon[] WeaponsArray;

    protected bool _isWalking, _outOfAp;

    private int _stepDirX, _stepDirY;
    public string Name;
    
    public int MaxAP, AP;

    public enum Fraction { Hero, Enemy, Boss }
    public Fraction side;
    
    public Vector3 Pos() => transform.position;
    
    public void Inject(IViewable field, UI ui, Pool pool)
    {
        _ui = ui;
        _field = field;
        _pool = pool;
        _pool.AddCharacterToPool(this);
    }

    public Character GetStats() => this;
    
    public virtual void MoveCharacter(Vector3 destination) { }
    
    public bool IsDead => Health.isOver;

   

    public void PrepareWeapon(IWeapon weapon)
    {
        if (AP >= weapon.ApCost && !_isWalking)
        {
            SelectedWeapon = weapon;
            if (side == Fraction.Hero)
                _field.ShowAttackTiles(SelectedWeapon);
        }
    }

   

    protected void Chache()
    {
        _sprite = GetComponent<SpriteRenderer>();
        SetStats(gameObject);
        _field.SetTileType(this, false);
        SetWeapons();
        Resistance.SetResistance();
        Health.SetMaxHP();
    }

    public void SetWeapons()
    {
        _leftWeapon = transform.GetChild(0).GetComponent<Weapon>();
        _rightWeapon = transform.GetChild(1).GetComponent<Weapon>();
        WeaponsArray = new IWeapon[] {_leftWeapon, _rightWeapon};
    }
    public void SetStats(GameObject obj)
    {
        obj.name = Name;
        AP = MaxAP;
    }

    public int Stamina => AP;
    public bool OutOfAP => _outOfAp;
    public bool isDamaged { get; }

    public void PrepareCharacterForNewTurn(List<IFightable> list, TurnState act,int AdditionalSP)
    {
        if (act == Script.Managers.TurnState.E && side == Fraction.Enemy
            || act == Script.Managers.TurnState.P && side == Fraction.Hero)
        {
            list.Add(this);
            AP = MaxAP;
        }
    }

 
    public virtual void SelectCharacter(bool selected)
    {
       ShowInfo();
        _field.ShowWalkTile(this);
      //  Field.CreateHighLight(transform.position, selected);
    }

    public void ShowInfo()
    {
        _ui.ShowResistances(Resistance._resistances); 
        _ui.ShowLeftWeapon(this,_leftWeapon,WeaponHand.Left); 
        _ui.ShowRightWeapon(this,_rightWeapon,WeaponHand.Right);
        _ui.ShowBaseInfo(Health.HealthPoints,AP,Name);
    }

    protected void MakeSteps(Vector3 destination)
    {
        _isWalking = true;
        _field.HideTiles();

        Vector3 pos = transform.position;
        _stepDirX = destination.x == pos.x ? 0 : destination.x > pos.x ? 1 : -1;
        _stepDirY = destination.y == pos.y ? 0 : destination.y > pos.y ? 1 : -1;
     //  if (Field.GetField()[(int)pos.x + _stepDirX][(int)pos.y + _stepDirY].Type == Cell.CellType.Free)
        transform.position += new Vector3(_stepDirX, _stepDirY);
     
    }

    protected void FinishSteps()
    {
        _field.SetTileType(this, false);
        _isWalking = false;
        _field.CreateHighLight(transform.position, true);
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
    
    public void TakeDamage(IWeapon weapon)
    {
        StartCoroutine(Flash(0));
        int damage = Health.DecreaseHealth(Resistance.CalculateDamage(weapon));
      
     //   Turn.I.ChangeTurnCount(Resistance.GetAttackResult());
        _ui.ShowPopUp(Resistance.GetAttackResult(), transform.position, damage);
      
        if(IsDead)KillCharacter();
    }

    public void KillCharacter()
    {
        _pool.RemoveCharacterFromPool(this);
        _field.SetTileType(this,true);
        Destroy(gameObject);
    }

  

    public IWeapon Attack()
    {
        _field.HideTiles();
        AP -= SelectedWeapon.ApCost;
        return SelectedWeapon;
    }

  protected void CheckForStamina()
    {
        _outOfAp = SelectedWeapon.ApCost > AP;
    }
}
