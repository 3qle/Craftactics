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
    private Controller _controller;
    public TurnState Act; 
    public List<Character> _activeFractionList = new List<Character>();
    private Spawner _spawner;
    private int roundCount;
    public void Initialize(Field field, UI ui, Pool pool, Controller controller, Spawner spawner)
    {
        _spawner = spawner;
        _controller = controller;
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
        if(Act == TurnState.P)
            foreach (var character in _pool.HeroesList) 
                _activeFractionList.Add(character);
        
        if(Act == TurnState.E)
            foreach (var character in _pool.EnemiesList) 
                _activeFractionList.Add(character);
        
        foreach (var character in _activeFractionList) 
            character.PrepareForNewTurn();
    }
   
    public void NextEnemyAct()
    {
        if (_activeFractionList.Count > 0 )
        {
            int i = Random.Range(0, _activeFractionList.Count);
            if (_activeFractionList[i].Attributes.stamina.CheckForStamina(_activeFractionList[i].Arms.SelectRandomWeapon()))
            {
                _activeFractionList.Remove(_activeFractionList[i]);
                NextEnemyAct();
            }
            else _controller.SelectEnemy(_activeFractionList[i]);
        }

        if (_activeFractionList.Count == 0)
        {
            StartNewWave();
            StartNewTurn();
        }
    }

    void StartNewWave()
    {
        if (_pool.EnemiesList.Count == 0)
        {
            roundCount += 1;
            _spawner.SpawnEnemies(_spawner.maxEnemy + roundCount);
        }
       
    }
}
