using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Character : MonoBehaviour, ICharacter
{
    private SpriteRenderer _sprite;
    protected IViewable Field;
    private IUIble _uIble;
    public IHealth Health;
    public IResistance Resistance;
    private IWeapon _leftWeapon, _rightWeapon;
    public IWeapon SelectedWeapon;
    protected IWeapon[] WeaponsArray;
    public static Action<int, int, string> ShowBaseInfo;
    protected bool _isWalking, _outOfAp;

    private int _stepDirX, _stepDirY;
    public string Name;
    public int MaxHP;
    public int MaxAP, AP;

    public enum Fraction { Hero, Enemy, Boss }
    public Fraction side;
    
    public Vector3 Pos() => transform.position;
    
    public void SetField(IViewable field) => Field = field;
    
    public Character GetStats() => this;
    
    public virtual void MoveCharacter(Vector3 destination) { }
    
    public bool IsDead => Health.isOver;
    
    public void PrepareWeapon(IWeapon weapon)
    {
        if (AP >= weapon.ApCost && !_isWalking)
        {
            SelectedWeapon = weapon;
            if (side == Fraction.Hero)
                Field.ShowAttackTiles(SelectedWeapon);
        }
    }
    
    protected void Chache()
    {
        Resistance = GetComponent<IResistance>();
        Health = new Health(MaxHP);
        _sprite = GetComponent<SpriteRenderer>();
        _uIble = FindObjectOfType<UI>();
        SetStats(gameObject);
        Field.SetTileType(this, false); 
        Turn.PrepareFractionForTurn += PrepareCharacterForNewTurn;
        SetWeapons();
   }

    public void SetWeapons()
    {
        _leftWeapon = transform.GetChild(0).GetComponent<IWeapon>();
        _rightWeapon = transform.GetChild(1).GetComponent<IWeapon>();
        WeaponsArray = new[] {_leftWeapon, _rightWeapon};
    }
    public void SetStats(GameObject obj)
    {
        obj.name = Name;
        AP = MaxAP;
    }

    public int Stamina => AP;
    public bool OutOfAP => _outOfAp;
    public bool isDamaged { get; }

    public void PrepareCharacterForNewTurn(List<ICharacter> list, Turn.Turns act,int AdditionalSP)
    {
        if (act == Turn.Turns.E && side == Fraction.Enemy
            || act == Turn.Turns.P && side == Fraction.Hero)
        {
            list.Add(this);
            AP = MaxAP;
        }
    }

 
    public virtual void SelectCharacter(bool selected)
    {
        Resistance.ShowResistance();
        ShowBaseInfo.Invoke(Health.HealthPoints,AP,Name);
        _leftWeapon.ShowWeapon(this,WeaponHand.Left);
        _rightWeapon.ShowWeapon(this,WeaponHand.Right);
        Field.ShowWalkTile(this);
      //  Field.CreateHighLight(transform.position, selected);
    }

    protected void MakeSteps(Vector3 destination)
    {
        _isWalking = true;
        Field.HideTiles();

        Vector3 pos = transform.position;
        _stepDirX = destination.x == pos.x ? 0 : destination.x > pos.x ? 1 : -1;
        _stepDirY = destination.y == pos.y ? 0 : destination.y > pos.y ? 1 : -1;
     //  if (Field.GetField()[(int)pos.x + _stepDirX][(int)pos.y + _stepDirY].Type == Cell.CellType.Free)
        transform.position += new Vector3(_stepDirX, _stepDirY);
     
    }

    protected void FinishSteps()
    {
        ShowBaseInfo.Invoke(Health.HealthPoints,AP,Name);
        Field.SetTileType(this, false);
        _isWalking = false;
        Field.CreateHighLight(transform.position, true);
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
        ShowBaseInfo.Invoke(Health.HealthPoints,AP,Name);
     //   Turn.I.ChangeTurnCount(Resistance.GetAttackResult());
        _uIble.ShowPopUp(Resistance.GetAttackResult(), transform.position, damage);
      
        if(IsDead)KillCharacter();
    }

    public void KillCharacter()
    {
        Turn.I.RemoveDeadCharacter(this);
        Field.SetTileType(this,true);
        Destroy(gameObject);
    }

    public void Visualize(ICharacterView view)
    {
        
    }

    public IWeapon Attack()
    {
        Field.HideTiles();
        AP -= SelectedWeapon.ApCost;
        return SelectedWeapon;
    }

  protected void CheckForStamina()
    {
        _outOfAp = SelectedWeapon.ApCost > AP;
    }
}
