using System;
using Script.Enum;
using UnityEngine;
    public class Status
    {
        public ShiftType _type;
        public int Duration;
        private int _startDuration;
        public Attribute trait;
        public Item _item;
        public Sprite Icon;
       public float Points;
       public float TraitCurrent;
        public static Action< Status , Vector2> ShowText;
        public void CheckStatus(Vector2 pos)
        {
          
            if (_startDuration == Duration  || _type == ShiftType.Attack)
            {
                ShowText.Invoke(this,pos);
                if (_type == ShiftType.Attack)
                    trait.DealDamage(Points);
               
            }
            Duration = Duration > 0 ? Duration-=1 : Duration;
            if(Duration == 0  && _type != ShiftType.Attack) trait?.Revert(Points);
            Icon = Duration == 0 ? null:Icon;
        }

        public bool CanRemovePassive()
        {
            if (_type == ShiftType.Passive && !_item.Bought)
            {
                trait?.Revert(Points);
                Icon = null; 
            }
            return _type == ShiftType.Passive && !_item.Bought;
               
        }

      public Status(Shaper shaper, Item item, Attribute statusTrait)
        {
            TraitCurrent = statusTrait.current;
            _item = item;
            trait = statusTrait;
            _type = shaper.type;
            Duration =  shaper.Duration;
            Points = shaper.type == ShiftType.Attack? statusTrait.damage: shaper.Points;
            Icon = shaper.Icon;
            _startDuration = Duration;
        }

        public void Remove()
        {
            Duration = 0;
            Icon =  null;
        }
    }
