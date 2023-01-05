using System;
using System.Numerics;
using Script.Enum;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class ItemRange: ItemProperty
{
    public int MinRange, MaxRange;
    private Field _field;
    public RangeType RangeType;

    public void Initialize()
    {
        Icon = Resources.Load<Sprite>("Sprites/Status/Range/" + RangeType);
    }
    public override void Use(Character target, Field field, Item item)
    {
        _field = field;
        _field.HideTiles();
        ShowAttackTiles(target);
    
      
    }

    public override TextMeshProUGUI Text(TextMeshProUGUI text)
    {
        throw new NotImplementedException();
    }

    public override float StatusDamageFill()
    {
        return 0;
    }

    public void ShowAttackTiles(Character user)
    {
        Vector3 pos = user.transform.position;
       
        for (int x = (int) pos.x - MaxRange; x <= MaxRange + pos.x; x++)
        for (int y = (int) pos.y - MaxRange; y <= MaxRange + pos.y; y++)
        {
            if (RangeType == RangeType.AllAlly || RangeType == RangeType.AllEnemy)
                _field.SelectAllSide(RangeType);
            else
            {
                if (CheckMaxRange(x, y) && CheckMinRange(x, y, MinRange, pos) || RangeType == RangeType.Self) 
                    _field._tilePool[x][y].CreateAttackCell(RangeType);
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
