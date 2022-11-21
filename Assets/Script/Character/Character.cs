using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Character : MonoBehaviour, ICharacter
{
    private SpriteRenderer _sprite;
    protected IViewable Field;
    private IUIble _uIble;
    public IHealth Health;
    public IResistance Resistance;
    public Weapon[] weapons;
    public IWeapon SelectedWeapon;
    protected bool _isWalking, _outOfAp;

    private int _stepDirX, _stepDirY;
    public string Name;
    public int MaxHP;
    public int MaxAP, AP;

    public enum Fraction { Hero, Enemy, Boss }
    public Fraction side;
    
    public Vector3 Pos() => transform.position;
    
    public IWeapon GetWeapon(int i) => weapons[i];
    
    public void SetField(IViewable field) => Field = field;
    public IResistance GetResistanse  => Resistance;
    
    public IHealth GetHealth() => Health;
    
    public Character GetStats() => this;
    
    public virtual void MoveCharacter(Vector3 destination) { }
    
    public bool IsDead => Health.isOver;
    
    public IWeapon PrepareWeapon(int i)
    {
        if (AP >= weapons[i].Cost && !_isWalking)
        {
            SelectedWeapon = weapons[i];
            if (side == Fraction.Hero)
                Field.ShowAttackTiles(weapons[i]);
        }
        return SelectedWeapon;
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
    }

    public void SetStats(GameObject obj)
    {
        obj.name = Name;
        AP = MaxAP;
    }

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
        Field.ShowWalkTile(this);
        _uIble.ShowInfo(this);
        Field.CreateHighLight(transform.position, selected);
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
        _uIble.ShowInfo(this);
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
        _uIble.ShowInfo(this);
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
        AP -= SelectedWeapon.GetCost();
        return SelectedWeapon;
    }

  protected void CheckForStamina(Vector3 destination)
    {
        _outOfAp = SelectedWeapon.GetCost()> AP;
      
        
    }
}
