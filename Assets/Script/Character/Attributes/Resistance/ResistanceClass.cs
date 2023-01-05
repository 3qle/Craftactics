using System;
using UnityEngine;

[Serializable]
public class ResistanceClass
{
    public Element Element;
    [Range(-100, 100)] public int current;
    private int raw;

  [HideInInspector]  public Sprite Icon;
    private Status status;
    public void Set()
    {
        raw = current;
       
   
        status = new Status();
    }

    public void Reset()
    {
        current = raw;
    }

    

    public void ChangeAttribute(int i, int duration, AttributeModifier.BuffType type, Character target,Item item)
    {
        switch (type)
        {
            case AttributeModifier.BuffType.Upgrade:
                current += i;
                Reset();
                break;
            case AttributeModifier.BuffType.Buff:
            case AttributeModifier.BuffType.Passive:
                current += i;
                Debug.Log("set");
                target.Attributes.SetStatus( status.Set(type, duration, this, target,item,Icon));
                break;
            case AttributeModifier.BuffType.Restore:
                current = current + i < raw ? current += i : raw;
                break;
        }
    }
}
