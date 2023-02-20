using System;
using System.Collections.Generic;

using Script.Enum;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class Range
{
    public int MinRange, MaxRange;
    private Field _field;
    public RangeType target;
   
    public  void Use(Character character)
    {
       
        _field.HideTiles();
        ShowAttackTiles(character);
    }

    public void Initialize(Field field)
    {
        _field = field;
    }

   
    public void ShowAttackTiles(Character user)
    {
        var pos = user.transform.position;
        for (int x = (int) pos.x - MaxRange; x <= MaxRange + pos.x; x++)
        for (int y = (int) pos.y - MaxRange; y <= MaxRange + pos.y; y++) 
            if (CheckMaxRange(x, y) && CheckMinRange(x, y, MinRange, pos) || target == RangeType.Self) 
                _field._tilePool[x][y].CreateAttackCell(target);
    }
    bool CheckMaxRange(int x, int y) => x >= 0 && x < _field.width && y >= 0 && y < _field.height;
       
    
    bool CheckMinRange(int x,int y, int min, Vector3 pos)
    {
        var checkX = x > pos.x
            ? pos.x + min < x 
            : pos.x - min > x;
        var checkY = y > pos.y
            ? pos.y + min < y
            : pos.y - min > y;
        return checkX || checkY;
    }

    public List<Character> GetTargetForPostEffect(Character target)
    {
       
       return _field.GetNearCharacters(target).Count > 0 ? _field.GetNearCharacters(target) : new List<Character>();
    }
       

    
}
