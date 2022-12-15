using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Script.Managers;
using UnityEngine.Events;
[Serializable]
public class Controller
{
    private Camera _camera;
    [HideInInspector] public Character _selectable;
    private CharacterButton _characterButton;
    private Turn _turn;
    private UI _ui;
    private UIShop _uiShop;
    public void Initialize(BattleStarter starter)
    {
        _ui = starter.ui;
        _camera = Camera.main;
        _turn = starter.turn;
        _uiShop = starter.shop.UIShop;
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
                     SelectByMouse(hit.collider.GetComponent<CellButton>().CurrentCharacter);
                     break; 
             }
        }
    }

    void AttackTarget(Character character)
    {
        _selectable.Experience.Add(character.TakeDamage(_selectable.Attack()));
        Select(false);
    }
    void Select(bool select)
    {
        _characterButton?.HighLightButton(select, _selectable);
        _selectable?.Select(select); 
       _ui.ShowInfoOnSelect(_selectable);
    }
    public void SelectCharacterButton(Character character, CharacterButton button)
    {
        Select(false);
        _characterButton = button;
        _selectable = character;
        _uiShop.SelectCharacter(character,button);
        Select(true);
    }

   public void SelectByMouse(Character character)
    {
        Select(false);
        _selectable = character; 
        Select(true);
    }
   
   public void SelectFromShop(Character character)
    {
        _selectable = character;
        _ui.ShowInfoOnSelect(_selectable);
    }
   
}
