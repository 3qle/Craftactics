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
    private Pool _pool;
    private UI _ui;
    private UIShop _uiShop;
    public void Initialize(BattleStarter starter)
    {
        _ui = starter.ui;
        _pool = starter.pool;
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

            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "WalkTile":
                        _selectable.Move(hit.collider.transform.position);
                        break;
                    case "AttackTile":
                        _selectable.Experience.Add(hit.collider.GetComponent<CellButton>().CharOnCell.TakeDamage(_selectable.Attack()));
                        Deselect();
                        Debug.Log(_selectable);
                        break;
                    case "CharacterTile":
                        SelectByMouse(hit.collider.GetComponent<CellButton>().CharOnCell);
                        break;
                }
            }
        }
    }

    public void SelectFromUi(Character character, CharacterButton button)
    {
        Deselect();
        _characterButton = button;
        _selectable = character;
        _uiShop.SelectCharacter(character,button);
        Select();
    }

   public void SelectByMouse(Character character)
    {
        Deselect();
        _selectable = character; 
        Select();
    }
   
    public void SelectEnemy(Character enemy)
    {
        Deselect();
        _selectable = enemy;
        Select();
    }
    
    void Deselect()
    {
        _characterButton?.HighLightButton(false);
        _selectable?.Select(false);
        _selectable = null;
        _ui.ShowInfoOnSelect(_selectable);
    }

    public void SelectFromShop(Character character)
    {
        _selectable = character;
    }
    void Select()
    {
        _characterButton.HighLightButton(true);
        _selectable?.Select(true);
        _ui.ShowInfoOnSelect(_selectable);
    }
}
