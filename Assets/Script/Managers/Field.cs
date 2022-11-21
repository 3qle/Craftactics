using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class Field :Spawner,IViewable
{
    //проверка
    [Header("Field Settings")]
    public Cell tile;
    private readonly List<List<Cell>> _tilePool = new List<List<Cell>>();
    
    private ISelectable _selectable;
    private int _posX, _posY;
    private int _stamina;
    private Vector3 _pos;

    private void Awake()
    {
        CreateField();
        SpawnHeroes(this);
        StartCoroutine(CreateSpawnPoint());
    }
    
    public Field GetStats() => this;
    
    public List<List<Cell>> GetField() 
        => _tilePool;
    
    public void CreateHighLight(Vector3 pos,bool create) 
        => _tilePool[(int)pos.x][(int)pos.y].CreateHighLight(create);
    
    public void SetTileType(Character obj, bool free) 
        => _tilePool[(int)obj.Pos().x][(int)obj.Pos().y].ChangeType(obj, free);
    
    public void SpawnButton() 
        => StartCoroutine(CreateSpawnPoint());
    
   public IEnumerator CreateSpawnPoint()
   {
       yield return new WaitForSeconds(0.1f);
     
       _posX = Random.Range(0, width); 
       _posY = Random.Range(0, height);
       for (int i = 0; i < maxEnemy; i++) 
       { 
           yield return new WaitForSeconds(0.1f); 
           while(_tilePool[_posX][_posY].Type != Cell.CellType.Free) 
           { 
               _posX = Random.Range(0, width); 
               _posY = Random.Range(0, height); 
            
           } 
           SpawnEnemy(new Vector3(_posX, _posY),this); 
       }

       maxEnemy++;
   }
   
   void CreateField()
    {
        for (var i = 0; i <= width; i++)
            _tilePool.Add(new List<Cell>());
        
        for (var i = 0; i < width; i++) 
        for (var j = 0; j < height; j++) 
        {
            _tilePool[i].Add(Instantiate(tile, new Vector3(i, j), quaternion.identity,transform.GetChild(0)));
            _tilePool[i][j].name = (i + "." + j);
        }
    }

    void GetStatsFromChar(ISelectable obj)
    {
        _selectable = obj;
        _pos = obj.Pos();
        _stamina = 10;
    }

    bool CheckMaxRange(int x, int y)
    {
        var get = x >= 0 && x < width && y >= 0 && y < height;
        return get;
    }
    public void ShowWalkTile(ISelectable obj)
    {
        HideTiles();
        GetStatsFromChar(obj);
       // for (var x = (int)_pos.x - _stamina; x <= _stamina + _pos.x; x++) 
      //  for (var y = (int)_pos.y - _stamina ; y <= _stamina + _pos.y; y++)
            if (CheckMaxRange((int)obj.GetStats().Pos().x, (int)obj.GetStats().Pos().y) && //_tilePool[(int)obj.GetStats().Pos().x][y].Type == Cell.CellType.Free &&
                obj.GetStats().side == Character.Fraction.Hero)
            {
                _tilePool[(int)obj.GetStats().Pos().x][(int)obj.GetStats().Pos().y].CreateWalkCell();
            }
    }

    public List<Cell> GetTargetsForEnemy(Character obj)
    {
        var cells = new List<Cell>();
        HideTiles();
        GetStatsFromChar(obj);
        for (int x = (int) _pos.x - _stamina; x <= _stamina + _pos.x; x++)
        for (int y = (int) _pos.y - _stamina; y <= _stamina + _pos.y; y++)
            if (CheckMaxRange(x,y) && _tilePool[x][y].Type == Cell.CellType.Hero && obj.GetStats().side == Character.Fraction.Enemy)
                    cells.Add(_tilePool[x][y]);
        return cells;
    }

    public bool CheckForFreeTile(int x, int y)
    {
        bool free = _tilePool[x][y].Type == Cell.CellType.Free;
        return free;
    }
   
    public void ShowAttackTiles(IWeapon stats)
    {
        HideTiles();
        Vector2 pos = _selectable.Pos();
        for (int x = (int) pos.x - stats.MaxRange(); x <= stats.MaxRange() + pos.x; x++) 
        for (int y = (int) pos.y - stats.MaxRange(); y <= stats.MaxRange() + pos.y; y++) 
            if (CheckMaxRange(x,y) && CheckMinRange(x,y,stats.MinRange(),pos) && _tilePool[x][y].Type != Cell.CellType.Hero) 
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



