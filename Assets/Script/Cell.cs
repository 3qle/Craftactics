using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
   public SpriteRenderer _spriteRenderer;
   private Animator _animator;
   public BoxCollider2D _box;
   public ICharacter CharOnCell;
   public Cell Up,Left,Right,Down;
   private IViewable field;
   public enum CellType { Free, Hero, Enemy, }

   public CellType Type;
   private int weaponRange;
   private void Start()
   {
       field = FindObjectOfType<Field>();
       _box = GetComponent<BoxCollider2D>();
       _spriteRenderer = GetComponent<SpriteRenderer>();
       _animator = GetComponent<Animator>();
   }

   public void AddFreeNeighbours(int range)
   {
       int x = (int)transform.position.x;
       int y = (int)transform.position.y;
       
       Left = CheckBorders(x - range, y, field.GetField()) && field.GetField()[x - range][y].Type == CellType.Free
               ? field.GetField()[x - range][y]
               : null;
       
       Right = CheckBorders(x + range, y, field.GetField()) && field.GetField()[x + range][y].Type == CellType.Free
               ? field.GetField()[x + range][y]
               : null;
       
       Down = CheckBorders(x, y - range, field.GetField()) && field.GetField()[x][y - range].Type == CellType.Free
               ? field.GetField()[x][y - range]
               : null;
       
       Up = CheckBorders(x, y + range, field.GetField()) && field.GetField()[x][y + range].Type == CellType.Free
               ? field.GetField()[x][y + range]
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

   public Cell CheckFreeNeighbours(Character character)
   {
       weaponRange = character.SelectedWeapon.MaxRange();
      AddFreeNeighbours(weaponRange);
       Cell c = Left != null
            && (Left.Type == CellType.Free || (Character) CharOnCell == character) 
            ? Left 
            :  Right != null 
            && (Right.Type == CellType.Free || (Character) CharOnCell == character)
            ? Right
            :  Up != null && 
            (Up.Type == CellType.Free || (Character) CharOnCell == character) 
            ? Up
            :  Down != null && 
            (Down.Type == CellType.Free || (Character) CharOnCell == character) 
            ? Down : null;
       return c;
   }
  
   bool CheckBorders(int x, int y, List<List<Cell>> f)
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
           "WalkTile" => Color.cyan,
           "AttackTile" => Color.red,
           "VoidTile" => Color.grey,
           "CharacterTile" => Color.yellow,
           _ => default
       };
       
   }

   void CreateCell(bool create)
   {
       
       _animator.SetBool("Show",create);
       
       // _box.enabled = create;
   }
   
   public void ChangeType(Character c, bool free)
   {
       if (!free)
       {
           if (c.GetStats().side == Character.Fraction.Hero)
               Type = CellType.Hero;
           if (c.GetStats().side == Character.Fraction.Enemy)
               Type = CellType.Enemy;
           tag = "CharacterTile";
           CharOnCell = c;
       }
       else
       {
           Type = CellType.Free;
           CharOnCell = null;
       }
   }
 public  void  CreateHighLight(bool create)
 {
       SetCellType(tag);
       CreateCell(create);
 }

 void SetCellTag(CellType type)
 {
     tag = type switch
     {
         CellType.Free => "FreeTile",
         CellType.Enemy => "CharacterTile",
         CellType.Hero => "CharacterTile",
     };
 }
}
