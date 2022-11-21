using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class Turn : MonoBehaviour
{
    public static Action<bool> AllowPlayerClick;
    public static Turn I;
    public IViewable field;
    public TextMeshProUGUI scoreText;
    private int _score;
    private IUIble _ui;
    public static Action<List<ICharacter>, Turns, int> PrepareFractionForTurn;
    public enum Turns { P ,E }
    public List<ICharacter> _activeFractionList = new List<ICharacter>();
    
    [Header("Turn Settings")]
    public Turns Act;
    int _turnsCount;
    private int _playerTurnCount, _savedTurnCount;
    
    private void Awake() =>  I = this;
    
    void Start()
    {
        field = FindObjectOfType<Field>();
        _ui = FindObjectOfType<UI>();
        Enemy.EnemyActDone += ActEnemy;
        PrepareChangeTurn();
    }
    
    public void TurnButton()
    {
        if(Act == Turns.P) PrepareChangeTurn();
    }

    private void PrepareChangeTurn()
    {
        Act = Act == Turns.E? Turns.P:Turns.E;
        StartCoroutine(ChangeTurn());
    }

    IEnumerator ChangeTurn()
    {
        _ui.UpdateTurnText(Act);
        _activeFractionList.Clear();
        AllowPlayerClick.Invoke(Act == Turns.P);
        PrepareFractionForTurn.Invoke(_activeFractionList, Act, _savedTurnCount);
        if (_activeFractionList.Count == 0)
        {
            field.SpawnButton();
            PrepareChangeTurn();
        }
        yield return new WaitForSeconds(1);
        //SetTurnCount(_activeFractionList.Count);
        if(Act == Turns.E) ActEnemy();
    }
    private void ActEnemy()
    {
        if (_activeFractionList.Count > 0  && Act == Turns.E)
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
        if(_activeFractionList.Count == 0) PrepareChangeTurn();
    //    if(_activeFractionList.Count == 0 && _turnsCount > 0 && Act == Turns.E)   
       //     PrepareFractionForTurn.Invoke(_activeFractionList, Act,0);
    }
    
    public void ChangeTurnCount(ElementalResistance.Resistance result)
    {
        switch (result)
        {
            case ElementalResistance.Resistance.Weak:
                SetTurnCount(_turnsCount);
                break;
            case ElementalResistance.Resistance.Block:
                SetTurnCount(_turnsCount-2);
                break;
            case ElementalResistance.Resistance.Neutral:
                SetTurnCount(_turnsCount-1);
                break;
            case ElementalResistance.Resistance.Absorb:
                SetTurnCount(-_turnsCount);
                break;
        }

        if(_turnsCount == 0) PrepareChangeTurn();
    }

    void SetTurnCount(int i)
    {
        _savedTurnCount = _turnsCount;
        _turnsCount = i;
        _turnsCount = _turnsCount < 0 ? 0 : _turnsCount;
        _ui.ShowTurnCount(_turnsCount);
    }
    public void RemoveDeadCharacter(ICharacter character)
    {
        PrepareFractionForTurn -= character.PrepareCharacterForNewTurn;
    }
}
