using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Enum;
using UnityEngine;

[Serializable]
public class Attributes
{
    public List<Attribute> TraitList;
    public List<Status> StatusList = new List<Status>();
    public Attribute Get(Trait type) =>
        TraitList.Find(attribute => attribute.Trait == type);
    
    public void Initialize(Character character)
    {
        foreach (var stat in TraitList)
            stat.Initialize(character); 
    }

    public bool isWounded => Get(Trait.Health).current < Get(Trait.Health).startPoint  * 0.4f;

    public void SetStatus(Status status)
    {
        if(!StatusList.Contains(status))
         StatusList.Add(status);
    }

    public void CheckPassive(Item item)
    {
        var list = StatusList.ToArray();
        foreach (var status in list.Where(status => status._item == item)) 
           if(status.CanRemovePassive()) StatusList.Remove(status) ;
    }

    public IEnumerator CheckActiveModifiers(Vector2 pos)
    {
        var list = StatusList.ToArray();
        foreach (var status in list)
        {
            
                status.CheckStatus(pos);
                Debug.Log(status._type);
                if(!status.Icon) StatusList.Remove(status);
                yield return new WaitForSeconds(1);
            
        }
    }

    public float GetStack() 
        => (Get(Trait.Strength).current
            + Get(Trait.Accuracy).current
            + Get(Trait.Magic).current
            + Get(Trait.Spirit).current
            + Get(Trait.Flow).current) / 100;
    
}
