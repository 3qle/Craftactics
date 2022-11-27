using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Managers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Turn 
{
    [Header("Turn Settings")]
    private Pool _pool;
    private UI _ui;
     
    public TurnState Act;
    [HideInInspector] public List<Character> _activeFractionList = new List<Character>();
    
    public void Initialize(Field field, UI ui, Pool pool)
    {
        
        _ui = ui;
        _pool = pool;
        Act = TurnState.E;
        StartNewTurn();
    }
    
   public void StartNewTurn()
    {
        Act = Act == TurnState.E? TurnState.P:TurnState.E;
        AddActiveFractionToList();
        if(Act == TurnState.E)NextEnemyAct();
        _ui.UpdateTurnText(Act);
    }

    void AddActiveFractionToList()
    {
        _activeFractionList.Clear(); 
        _activeFractionList = Act == TurnState.P? _pool.HeroesList : _pool.EnemiesList;
        
        foreach (var character in _activeFractionList) 
            character.PrepareForNewTurn();
    }
   
    public void NextEnemyAct()
    {
        if (_activeFractionList.Count > 0 )
        {
            int i = Random.Range(0, _activeFractionList.Count);
            
            if (_activeFractionList[i].Stamina.OutOfStamina)
            {
                _activeFractionList.Remove(_activeFractionList[i]);
                NextEnemyAct();
            }
            else 
                _activeFractionList[i].Select(true);
        }
        if(_activeFractionList.Count == 0) StartNewTurn();
    }
}
