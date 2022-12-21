using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class ItemRange: ItemProperty
{
    public bool ToEnemy;
    public int MinRange, MaxRange;
    private Field _field;

    public override void Use(Character target, Field field)
    {
        _field = field;
        ShowAttackTiles(target);
       
    }

    public override TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        throw new NotImplementedException();
    }

    public void ShowAttackTiles(Character user)
    {
        Vector3 pos = user.transform.position;
       
        for (int x = (int) pos.x - MaxRange; x <= MaxRange + pos.x; x++)
        for (int y = (int) pos.y - MaxRange; y <= MaxRange + pos.y; y++)
        {
            
            if (CheckMaxRange(x, y) && CheckMinRange(x, y, MinRange, pos))
            {
                _field._tilePool[x][y].CreateAttackCell(ToEnemy);
               
            }
        }
        
               
                   
    }
    bool CheckMaxRange(int x, int y)
    {
        var get = x >= 0 && x < _field.width && y >= 0 && y < _field.height;
        return get;
    }
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
}
