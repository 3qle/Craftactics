using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Script.Managers;
using UnityEngine.Events;

public class Controller
{
    private Camera _camera;
    public Character _selectable;
    private CharacterButton _characterButton;
    private Turn _turn;
    private UI _ui;
  
    public Controller (BattleStarter starter)
    {
        _ui = starter.ui;
        _camera = Camera.main;
        _turn = starter.turn;
        CharacterButton.CharacterButtonSelected += Select;
        ShopButton.CharacterButtonSelected += Select;
    }

    public   void WaitForInput()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
      
        if (Input.GetMouseButtonDown(0) && _turn.Act == TurnState.P)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

             switch (hit.collider?.tag) 
             {
                 case "WalkTile": 
                     _selectable.Move(hit.collider.transform.position);
                     break;
                 case "AttackTile":
                     AttackTarget(hit.collider.GetComponent<CellButton>().CurrentCharacter);
                     break;
                 case "CharacterTile": 
                     Select(hit.collider.GetComponent<CellButton>().CurrentCharacter);
                     break; 
             }
        }
    }

    void AttackTarget(Character target)
    {
       _selectable.UseItem(target);
    }
    public void Select(Character character)
    {
        _selectable?.Select(false); 
        _selectable = character; 
        _selectable.Select(true);
       _ui.ShowInfoOnSelect(_selectable);
    }
   
    
    
}
