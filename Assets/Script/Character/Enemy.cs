using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : Character
{
    private Vector3 _pos;
    private ICharacter _target;
    private IResistance _targetResistance;
    public List<Cell> targetList = new List<Cell>();
    private Cell _targetCell;
    public static Action EnemyActDone;

    private void Start()
    {
        Chache();
    }
    public override void SelectCharacter(bool selected)
    {
        _target = null;
        base.SelectCharacter(selected);
        if (!selected || Turn.I.Act != Turn.Turns.E) return;
        PrepareWeapon(Random.Range(0,2));
        CheckForStamina(_pos);
        if(!_outOfAp) CreateWaypoint(); 
        StartCoroutine(_target == null ||  _outOfAp? StopAction():MoveEnemy(_pos));
        Debug.Log($" {name} {_target}");
    }


    private IEnumerator MoveEnemy(Vector3 destination)
    {
         AP -= 1;
        Field.SetTileType(this, true);
        while (transform.position != destination)
        {
            MakeSteps(destination);
            yield return new WaitForSeconds(0.15f);
        }
        FinishSteps();
        yield return new WaitForSeconds(0.5f);
        AttackEnemyTarget();
    }  
    
    private IEnumerator StopAction()
    {
        SelectCharacter(false);
        yield return new WaitForSeconds(1);
        EnemyActDone.Invoke();
    }

    private void CreateWaypoint()
    {
        targetList = Field.GetTargetsForEnemy(this);
        foreach (var t in targetList)
        {
            if (t.CheckFreeNeighbours(this) != null)
            {
                _target = t.CharOnCell;
               _pos = t.CheckFreeNeighbours(this).transform.position;
            }
        }
        
       
    }
    
    public void AttackEnemyTarget()
    {
        AP -= SelectedWeapon.GetCost();
        _target.TakeDamage(SelectedWeapon);
        _target = null;
        StartCoroutine(StopAction());
    }
}
