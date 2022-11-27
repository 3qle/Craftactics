using System;
using System.Collections;
using System.Collections.Generic;
using Script.Managers;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : Character
{
    private Vector2 _pos;
    private Character _target;
    private ElementalResistance _targetResistance;
    public List<Cell> targetList = new List<Cell>();
    private Cell _targetCell;
    
    public override void Select(bool selected)
    {
        base.Select(selected);
        if (!selected || _turn.Act != TurnState.E) return;
        
        Hands.PrepareRandomWeapon();
        SearchTarget();
        ChooseAction();
        StartCoroutine(WaitForAttack());
    }

    IEnumerator WaitForAttack()
    {
        while (Legs._isWalking) 
            yield return new WaitForEndOfFrame();
        AttackEnemyTarget();
    }

    void ChooseAction()
    {
        if(_target == null ||  Stamina.OutOfStamina) 
           StartCoroutine( StopAction());
        else 
            Move(_pos);
    }
    
    private IEnumerator StopAction()
    {
        _target = null;
        Select(false);
        yield return new WaitForSeconds(1);
        _turn.NextEnemyAct();
    }

    private void SearchTarget()
    {
        if (!Stamina.CheckForStamina(Hands.SelectedWeapon))
        {
            targetList = field.GetTargetsForEnemy(this);
            foreach (var t in targetList)
            {
                if (t.CheckFreeNeighbours(this) != null)
                {
                    _target = t.CharOnCell;
                    _pos = t.CheckFreeNeighbours(this).transform.position;
                }
            }
        }
    }
    
    public void AttackEnemyTarget()
    {
        _target.TakeDamage(Attack());
        StartCoroutine( StopAction());
    }
}
