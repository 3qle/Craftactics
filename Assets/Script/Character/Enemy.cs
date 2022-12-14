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
    public List<CellButton> targetList = new List<CellButton>();

     
   public override void Select(bool selected)
   {
      
        base.Select(selected);
        if (!selected || _turn.Act != TurnState.E) return;

        if (Attributes.stamina.CheckForStamina(Arms.SelectRandomWeapon(this)))
        {
            _turn.RemoveEnemy(this);
        }
        else
        {
            SearchTarget();
            ChooseAction();
            StartCoroutine(WaitForAttack());
        }
       
    }

    IEnumerator WaitForAttack()
    {
        while (Legs._isWalking) 
            yield return new WaitForEndOfFrame();
        AttackEnemyTarget();
    }

    void ChooseAction()
    {
        if(_target == null) 
           StartCoroutine( StopAction());
        else 
            Move(_pos);
    }
    
    private IEnumerator StopAction()
    {
        Debug.Log("stop");
        Select(false);
        yield return new WaitForSeconds(1);
        _turn.NextEnemyAct();
    }

    private void SearchTarget()
    {
        Arms.SelectRandomWeapon(this);
         targetList = field.GetTargetsForEnemy(this);
        foreach (var cell in targetList)
        {
            if (cell?.CheckFreeNeighbours(this) != null)
            {
                _target = cell.CurrentCharacter;
                _pos = cell.CheckFreeNeighbours(this).transform.position;
            }
        } 
    }
    
    public void AttackEnemyTarget()
    {
        if (_target != null)
        {
            UseItem(_target);
            StartCoroutine( StopAction());
        }
        else
            StartCoroutine( StopAction());
        
      
    }

   

}
