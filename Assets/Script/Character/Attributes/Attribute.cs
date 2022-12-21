using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attribute
{ 
    [HideInInspector] public AtrributeTypes attributeType;
    [HideInInspector] public string Name; 
    public float current; 
    protected int start;
    
    [Range(0,25)]  public int max ;
    protected int _duration;

    public bool notShow;

   public void ChangeAttribute(int i, int duration)
   {
       _duration = duration;
       if (duration == 0)
       {
           max += 1;
           SetCurrentToMax();
       }
       else
       {
           current += i;
       }
     
   }

   public void DecreaseDuration()
   {
       if (_duration > 0)
       {
           _duration -= 1;
           if(_duration == 0)
               SetCurrentToMax();
       }
   }
   
   public void SetCurrentToMax() => current = max;
   public abstract void SetName();
   

}
