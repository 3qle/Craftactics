using System;
using System.Linq;
using Script.Enum;
using UnityEngine;
[Serializable]
public class Attribute
{
    private Character _character;
    private Attributes _traits;
    [HideInInspector] public float current;
    public Trait Trait;
    [Range(-100,100)] public int startPoint;
    [HideInInspector]public float damage;
    public bool IsLowerZero => current <= 0;
    public bool IsMore(int cost) => current > cost;
    public void Initialize(Character character)
    {
        _traits = character.Attributes;
        _character = character;
        SetCurrentToStart();
    }
    public void Revert(float i) => current -= i;
    public void Restore(float amount) => current = current + amount < startPoint ? current += amount : startPoint;
    public void SetCurrentToStart() => current = startPoint;

    public void Loose(float i)
    {
        current -= i;
        current = (float)Math.Round(current, 1);
      
        _character.TryKill();
    } 
   public void ChangeAttribute(Shaper shaper, Item item)
   {
       switch (shaper.type)
       {
         
           case ShiftType.Buff:
           case ShiftType.Debuff:
           case ShiftType.Passive:
               SetTemporary(shaper, item);
               break;
           case ShiftType.Upgrade : 
               SetMax((int)shaper.Points);
               break;
           case ShiftType.Restore:
               Restore(shaper.Points);
               break;
           case ShiftType.Attack: 
               TakeHit(shaper,item);
               break;
           case ShiftType.Dispel:
               RemoveTemporaryEffect();
               break;
           case ShiftType.Move:
               _character.Move(_character.transform.position + (_character.transform.position- item._user.transform.position));
               break;
       }
   }
   
   public void SetMax(int i)
   {
       startPoint += i;
       SetCurrentToStart();
   }
   
   public void SetTemporary(Shaper shaper, Item item)
   {
       current += shaper.Points;
       _character.Attributes.SetStatus(new Status(shaper, item,this));
   }

   private void TakeHit(Shaper shaper, Item item)
   {
       damage = (float) Math.Round(shaper.Points - shaper.Points * current / 100, 1);
       _character.Attributes.SetStatus(new Status(shaper, item,this));
   }

   public void DealDamage(float points)
   {
       Debug.Log($"Points {points}");
       _traits.Get(Trait.Health).Loose(points);
   } 

   void RemoveTemporaryEffect()
   {
       foreach (var status in _traits.StatusList.Where(status => status.trait.Trait == Trait && status._type == ShiftType.Debuff))
           status.Remove();
   }
}
