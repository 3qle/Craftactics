using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attribute
{
  [HideInInspector] public int current;
  [HideInInspector]  public string Name;
  [Range(0,25)]  public int max ;
   public  void LevelUp()  => max++;
   public void SetCurrentToMax() => current = max;
   public abstract void SetName();

}
