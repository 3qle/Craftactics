using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Enum;
using Script.Managers;

using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Field 
{
    [Header("Field Settings")]
    public  List<List<CellButton>> _tilePool = new List<List<CellButton>>();
    private int _posX, _posY;
    public int height,width;

    public List<Character> GetNearCharacters(Character character)
    {
        var list = new List<Character>();
        for (int x = (int)character.transform.position.x - 1; x < (int)character.transform.position.x + 1; x++)
        for (int y = (int)character.transform.position.y - 1; y < (int)character.transform.position.y + 1; y++) 
            if (CheckMaxRange(x,y) &&(_tilePool[x][y].CurrentCharacter != null && _tilePool[x][y].CurrentCharacter != character))
               list.Add(_tilePool[x][y].CurrentCharacter);
        return list;
    }

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
           _posY = Random.Range(1, height);
       }
       return new Vector2(_posX, _posY);
   }
   
    bool CheckMaxRange(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;
        
    
    public void ShowWalkTile(Character character, bool selected)
    {
        if (character.Attributes.Get(Trait.Stamina).current > 0 && character.entityType == EntityType.Hero && selected ) 
            _tilePool[(int)character.transform.position.x][(int)character.transform.position.y].CreateFreeWalkCells();
    }

    public List<CellButton> GetTargetsForEnemy(Character obj,Pool pool)
    {
        var cells = new List<CellButton>();
        HideTiles();
        var list = _tilePool.ToArray();

        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++) 
            if(_tilePool[x][y].Type == CellButton.CellType.Hero) 
                cells.Add(_tilePool[x][y]);
        
        
        
        Debug.Log($"cell {cells.Count}");
        return cells;
    }

    public bool CheckForFreeTile(int x, int y) 
        =>_tilePool[x][y].Type == CellButton.CellType.Free;
    
    public void HideTiles()
    {
        for (int i = 0; i < _tilePool.Count; i++) 
            for (int j = 0; j < _tilePool[i].Count ; j++) 
                _tilePool[i][j].HideWalkCell();
    }
}



