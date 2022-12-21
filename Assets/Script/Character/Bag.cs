using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Bag
{
    public List<Item> Items = new List<Item>();
    
    
    public void AddItem(Item item)
    {
        if (!Items.Contains(item))
        {
            Items.Add(item);
            item.SetQuantity(true);
        }
        else 
        {
          foreach (var _item in Items)
              if(_item.entityType == item.entityType)
                  _item.SetQuantity(true); 
        }
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }
    
    
}
