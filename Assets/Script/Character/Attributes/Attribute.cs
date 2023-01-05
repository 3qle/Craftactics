using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Attribute
{ 
    [HideInInspector] public AtrributeTypes attributeType;
    [HideInInspector] public string Name; 
    [HideInInspector] public float current; 
    protected int start;
    
    [Range(0,25)]  public int max ;
    
   protected bool notShow;
   [HideInInspector]  public Sprite Icon;
    private Status status;
    public void Initialize(List<Status> list)
    {
        notShow = attributeType == AtrributeTypes.Health || attributeType == AtrributeTypes.Stamina ;
        SetName();
        SetCurrentToMax();
        
        Icon = Resources.Load<Sprite>("Sprites/Stats/" + Name);
    
        status = new Status();
        
    }
   public void ChangeAttribute(int i, int duration, AttributeModifier.BuffType type,Character target, Item item)
   {
       switch (type)
       {
           case AttributeModifier.BuffType.Upgrade :
               max += 1;
               SetCurrentToMax();
               break;
           case AttributeModifier.BuffType.Buff:
           case AttributeModifier.BuffType.Passive:
               current += i;
               target.Attributes.SetStatus(status.Set(type, duration, this, target, item));
               break;
           case AttributeModifier.BuffType.Restore:
               current = current + i < max ? current += i : max;
               break;
       }
      
    }
   
   public void SetCurrentToMax() => current = max;
   public abstract void SetName();
   

}
