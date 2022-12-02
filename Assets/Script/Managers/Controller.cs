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
    Character _selectable;
    private Turn _turn;
    private Pool _pool;
    
    public void Initialize(Turn turn,Pool pool)
    {
        _pool = pool;
        _camera = Camera.main;
        _turn = turn;
    }

    public   void WaitForInput()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
      
        if (Input.GetMouseButtonDown(0) && _turn.Act == TurnState.P)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                 if (hit.collider.CompareTag("WalkTile")) 
                    _selectable.Move(hit.collider.transform.position);
                 
                 else  if (hit.collider.CompareTag("AttackTile")) 
                    hit.collider.GetComponent<Cell>().CharOnCell.TakeDamage(_selectable.Attack());
                 
                 else  if (hit.collider.CompareTag("CharacterTile")) 
                 {
                     if(_selectable != null) 
                       _selectable.Select(false);
                   
                    _selectable = hit.collider.GetComponent<Cell>().CharOnCell; 
                    _selectable.Select(true);
                  
                 }
               
            }
        }
    }

    public void SelectFromUi(int i)
    {
        if(_selectable != null) 
            _selectable.Select(false);
        _selectable = _pool.HeroesList[i];
        _selectable.Select(true);
    }

    public void ShowSelectedCharInfo()
    {
       if(_selectable != null) _selectable.ShowInfo();
    }
}
