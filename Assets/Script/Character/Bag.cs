using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Bag
{
    public List<Item> Items = new List<Item>();
    
    
    public void Initialize(Item item)
    {
        Items.Add(item);
    }
}
