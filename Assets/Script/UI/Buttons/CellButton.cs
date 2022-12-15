using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellButton : MonoBehaviour
{
   public SpriteRenderer _spriteRenderer;
   private Animator _animator;
   public Character CurrentCharacter;
   public CellButton Up,Left,Right,Down;
   private Field _field;
   public enum CellType { Free, Hero, Enemy, }

   public CellType Type;
   private int weaponRange;
   
   private void Start()
   {
       _spriteRenderer = GetComponent<SpriteRenderer>();
       _animator = GetComponent<Animator>();
   }

   public void Initialize(Field field) => _field = field;
   
   public void AddFreeNeighbours(int range)
   {
       int x = (int)transform.position.x;
       int y = (int)transform.position.y;
       
       Left = CheckBorders(x - range, y, _field._tilePool) && _field._tilePool[x - range][y].Type == CellType.Free
               ? _field._tilePool[x - range][y]
               : null;
       
       Right = CheckBorders(x + range, y, _field._tilePool) && _field._tilePool[x + range][y].Type == CellType.Free
               ? _field._tilePool[x + range][y]
               : null;
       
       Down = CheckBorders(x, y - range, _field._tilePool) && _field._tilePool[x][y - range].Type == CellType.Free
               ? _field._tilePool[x][y - range]
               : null;
       
       Up = CheckBorders(x, y + range, _field._tilePool) && _field._tilePool[x][y + range].Type == CellType.Free
               ? _field._tilePool[x][y + range]
               : null;
   }

   public void CreateFreeWalkCells()
   {
       AddFreeNeighbours(1);
        if(Left != null && Left._spriteRenderer.color.a == 0 ) 
               Left.CreateWalkCell(); 
        if(Right != null && Right._spriteRenderer.color.a == 0 ) 
               Right.CreateWalkCell();
        if(Down != null && Down._spriteRenderer.color.a == 0 ) 
               Down.CreateWalkCell(); 
        if(Up != null && Up._spriteRenderer.color.a == 0) 
               Up.CreateWalkCell();
   }

   public CellButton CheckFreeNeighbours(Character character)
   {
       weaponRange = character.Arms.selectedItem.MaxRange;
      AddFreeNeighbours(weaponRange);
       CellButton c = Left != null
            && (Left.Type == CellType.Free ||CurrentCharacter == character) 
            ? Left 
            :  Right != null 
            && (Right.Type == CellType.Free || CurrentCharacter == character)
            ? Right
            :  Up != null && 
            (Up.Type == CellType.Free || CurrentCharacter == character) 
            ? Up
            :  Down != null && 
            (Down.Type == CellType.Free || CurrentCharacter == character) 
            ? Down : null;
       return c;
   }
  
   bool CheckBorders(int x, int y, List<List<CellButton>> f)
   {
       bool check = x == Mathf.Clamp(x, 0, f.Count - 1) && y == Mathf.Clamp(y, 0, f[x].Count - 1);
       return check;
   }
   public void CreateWalkCell()
   {
       tag = "WalkTile";
       SetCellType(tag);
       CreateCell(true);
       CreateFreeWalkCells();
   }

   public void HideWalkCell()
   {
       tag = Type switch
       {
           CellType.Free => "FreeTile",
           CellType.Enemy => "CharacterTile",
           CellType.Hero => "CharacterTile",
       };
     
       SetCellType(tag);
       CreateCell(false);
   }

   public void CreateAttackCell()
   {
       tag = Type == CellType.Enemy? "AttackTile" : "VoidTile";
       SetCellType(tag);
       CreateCell(true);
   }

   void SetCellType(string tag)
   {
       _spriteRenderer.color = tag switch
       {
           "WalkTile" => new Color(0.21f,0.58f,0.52f),
           "AttackTile" => Color.red,
           "VoidTile" => Color.grey,
           "CharacterTile" => new Color(0.74f,0.41f,0.2f),
           _ => default
       };
       
   }

   void CreateCell(bool create)
   {
       _animator.SetBool("Show",create);
   }
   
   public void ChangeType(Character c, bool free)
   {
       if (!free)
       {
           if (c.side == Character.Fraction.Hero)
               Type = CellType.Hero;
           if (c.side == Character.Fraction.Enemy)
               Type = CellType.Enemy;
           tag = "CharacterTile";
           CurrentCharacter = c;
       }
       else
       {
           Type = CellType.Free;
           CurrentCharacter = null;
       }
   }
 public  void  CreateHighLight(bool create)
 {
       SetCellType("CharacterTile");
       CreateCell(create);
 }
}
