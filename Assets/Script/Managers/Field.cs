using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Field 
{
    [Header("Field Settings")]
    public  List<List<CellButton>> _tilePool = new List<List<CellButton>>();
    private Character _selectable;
    private int _posX, _posY;
    private int _stamina;
    private Vector3 _pos;
    public int height,width;


    public void CreateHighLight(Character obj, bool create)
    {
        HideTiles();
        _tilePool[(int)obj.transform.position.x][(int)obj.transform.position.y].CreateHighLight(create);
    }

    public void SetTileType(Character obj, bool free) 
        => _tilePool[(int)obj.transform.position.x][(int)obj.transform.position.y].ChangeType(obj, free);

    public void CreateField()
    {
        for (var i = 0; i <= width; i++)
            _tilePool.Add(new List<CellButton>());
    }

    public void SetCellOnField(CellButton cellButton, int x,int y)
    {
         _tilePool[x].Add(cellButton);
         _tilePool[x][y].name = (x + "." + y);
         _tilePool[x][y].Initialize(this);
    }
   public Vector2 CreateSpawnPoint()
   {
       _posX = Random.Range(0, width); 
       _posY = Random.Range(1, height); 
       while(_tilePool[_posX][_posY].Type != CellButton.CellType.Free) 
       { 
           _posX = Random.Range(0, width); 
           _posY = Random.Range(0, height);
       }
       return new Vector2(_posX, _posY);
   }
   
    void GetStatsFromChar(Character obj)
    {
        _selectable = obj;
        _pos = obj.transform.position;
        _stamina = 10;
    }

    bool CheckMaxRange(int x, int y)
    {
        var get = x >= 0 && x < width && y >= 0 && y < height;
        return get;
    }
    public void ShowWalkTile(Character obj, bool selected)
    {
        if (obj.Attributes.stamina.current > 0 && obj.side ==Character.Fraction.Hero && selected )
        {
            GetStatsFromChar(obj);
            _tilePool[(int)obj.transform.position.x][(int)obj.transform.position.y].CreateFreeWalkCells();
        }
    }

    public List<CellButton> GetTargetsForEnemy(Character obj)
    {
        var cells = new List<CellButton>();
        HideTiles();
        GetStatsFromChar(obj);
        for (int x = (int) _pos.x - _stamina; x <= _stamina + _pos.x; x++)
        for (int y = (int) _pos.y - _stamina; y <= _stamina + _pos.y; y++)
            if (CheckMaxRange(x,y) && _tilePool[x][y].Type == CellButton.CellType.Hero)
                    cells.Add(_tilePool[x][y]);
        return cells;
    }

    public bool CheckForFreeTile(int x, int y)
    {
        bool free = _tilePool[x][y].Type == CellButton.CellType.Free;
        return free;
    }
   
    public void ShowAttackTiles(Item item)
    {
       
        Vector2 pos = _selectable.transform.position;
        CreateHighLight(_selectable,true);
        for (int x = (int) pos.x - item.MaxRange; x <= item.MaxRange + pos.x; x++) 
        for (int y = (int) pos.y - item.MaxRange; y <= item.MaxRange + pos.y; y++) 
            if (CheckMaxRange(x,y) && CheckMinRange(x,y,item.MinRange,pos) && _tilePool[x][y].Type != CellButton.CellType.Hero
                && _selectable.Attributes.stamina.current >= item.SPCost ) 
                _tilePool[x][y].CreateAttackCell();
    }

    bool CheckMinRange(int x,int y, int min, Vector2 pos)
    {
        var checkX = x > pos.x
            ? pos.x + min < x 
            : pos.x - min > x;
        var checkY = y > pos.y
            ? pos.y + min < y
            : pos.y - min > y;
        return checkX || checkY;
    }
    
    public void HideTiles()
    {
        for (int i = 0; i < _tilePool.Count; i++) 
            for (int j = 0; j < _tilePool[i].Count ; j++) 
                _tilePool[i][j].HideWalkCell();
    }
    /*
    public void CheckSameEnemy(Vector3 firstPos, Weapon w)
    {
        HideTiles(); 
        var firstTarget = _tilePool[(int) firstPos.x][(int) firstPos.y].CharOnCell;
        if(!firstTarget.IsDamaged()) firstTarget.TakeDamage(firstTarget.GetStats()); 
        
        for (var x = -1; x < 2; x++)
        for (var y = -1; y < 2; y++)
        {
            var checkX = firstPos.x + x == Mathf.Clamp(firstPos.x + x, 0, width - 1);
            var checkY = firstPos.y + y == Mathf.Clamp(firstPos.y + y, 0, height - 1);

            var target = _tilePool[(int) firstPos.x + x][(int) firstPos.y + y].CharOnCell;
            if (!checkX || !checkY || (x != 0 && y != 0)) continue;
            if (target != null && target.GetStats().Name == firstTarget.GetStats().Name && !target.IsDamaged() && firstTarget != target) 
                CheckSameEnemy(target.Pos(), w);
        }
    }
    */
}



