using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Cursor : MonoBehaviour
{
    private Camera _camera;
    private ICharacter _selectable;

    private bool _canClick;
    void Start()
    {
        _camera = Camera.main;
        Turn.AllowPlayerClick += SetCursorLock;
    }

    void SetCursorLock(bool can) => _canClick = can;
    
    void Update() => Click(); 
    
    void Click()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
      
        if (Input.GetMouseButtonDown(0) && _canClick)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                 if (hit.collider.CompareTag("WalkTile")) 
                    _selectable.MoveCharacter(hit.collider.transform.position);

                else  if (hit.collider.CompareTag("AttackTile")) 
                    hit.collider.GetComponent<Cell>().CharOnCell.TakeDamage(_selectable.Attack());

                else  if (hit.collider.CompareTag("CharacterTile"))
                {
                    _selectable = hit.collider.GetComponent<Cell>().CharOnCell; 
                    _selectable.SelectCharacter(true);
                }
               
            }
        }
    }
   
  
}
