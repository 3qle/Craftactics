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
    public static Action<bool> AllowPlayerClick;
  //  public static Turn I;
    public IViewable _field;
    private Pool _pool;
   
    private IUIble _ui;
        //public static Action<List<IFightable>, TurnState, int> PrepareFractionForTurn;
    public TurnState Act;
    public List<Character> _activeFractionList = new List<Character>();
    
    [Header("Turn Settings")]
   
    int _turnsCount;
    private int _playerTurnCount, _savedTurnCount;
    
  //  private void Awake() =>  I = this;
    
    void Start()
    {
       // field = FindObjectOfType<Field>();
       // _ui = FindObjectOfType<UI>();
       
      
    }

    public void InjectDependency(Field field, UI ui, Pool pool)
    {
        _field = field;
        _ui = ui;
        _pool = pool;
        Act = TurnState.E;
        StartNewTurn();
        Enemy.EnemyActDone += ActEnemy;
    }
    public void TurnButton()
    {
        StartNewTurn();
    }
    
    void StartNewTurn()
    {
        Act = Act == TurnState.E? TurnState.P:TurnState.E;
        AddActiveFractionToList();
        AllowPlayerClick.Invoke(Act == TurnState.P);
        if(Act == TurnState.E)ActEnemy();
        _ui.UpdateTurnText(Act);
    }

    void AddActiveFractionToList()
    {
        _activeFractionList.Clear();
      _activeFractionList = Act == TurnState.P? _pool.HeroesList : _pool.EnemiesList;
    }
   
    private void ActEnemy()
    {
        if (_activeFractionList.Count > 0  && Act == TurnState.E)
        {
            int i = Random.Range(0, _activeFractionList.Count);
            
            if (_activeFractionList[i].OutOfAP)
            {
                _activeFractionList.Remove(_activeFractionList[i]);
                ActEnemy();
            }
           else
            _activeFractionList[i].SelectCharacter(true);
        }
        if(_activeFractionList.Count == 0) StartNewTurn();
    //    if(_activeFractionList.Count == 0 && _turnsCount > 0 && Act == Turns.E)   
       //     PrepareFractionForTurn.Invoke(_activeFractionList, Act,0);
    }
    
    public void ChangeTurnCount(ResistanceType result)
    {
        switch (result)
        {
            case ResistanceType.Weak:
                SetTurnCount(_turnsCount);
                break;
            case ResistanceType.Block:
                SetTurnCount(_turnsCount-2);
                break;
            case ResistanceType.Neutral:
                SetTurnCount(_turnsCount-1);
                break;
            case ResistanceType.Absorb:
                SetTurnCount(-_turnsCount);
                break;
        }

        if(_turnsCount == 0) StartNewTurn();
    }

    void SetTurnCount(int i)
    {
        _savedTurnCount = _turnsCount;
        _turnsCount = i;
        _turnsCount = _turnsCount < 0 ? 0 : _turnsCount;
        _ui.ShowTurnCount(_turnsCount);
    }
    
}
