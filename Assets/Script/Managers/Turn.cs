using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
   public int roundCount;
    public void Initialize(BattleStarter starter)
    {
        _spawner = starter;
        _controller = starter.controller;
        _ui = starter.ui;
        _pool = starter.pool;
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

    public void RemoveEnemy(Character enemy)
    {
        _activeFractionList.Remove(enemy);
        if(_pool.HeroesList.Count != 0)
        NextEnemyAct();
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void NextEnemyAct()
    {
        if (_activeFractionList.Count > 0 )
        {
            int i = Random.Range(0, _activeFractionList.Count);
             _controller.SelectByMouse(_activeFractionList[i]);
        }

        if (_activeFractionList.Count == 0)
        {
            if (_pool.EnemiesList.Count == 0)
                StartNewWave();
            else
                StartNewTurn();
        }
    }

    void StartNewWave()
    {
        roundCount += 1;
        _spawner.SpawnEnemies(_spawner.maxEnemy + roundCount);
        _spawner.shop.OpenShop(true);
        
    }
}
