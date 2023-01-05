using System.Linq;
using Script.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class Status
    {
        public AttributeModifier.BuffType type;
        public int _duration;
        public Sprite Icon;
        private Attribute _attribute;
        private ResistanceClass _resistance;
        private Character _character;
        public Item _item;
        public bool Disable;
        public float _damage;
        public Element Element;
        public void CheckDuration()
        {
            if (_duration > 0)
            {
                _duration -= 1;
                
                if (_duration == 0 && type != AttributeModifier.BuffType.Passive)
                {
                    {
                        _attribute?.SetCurrentToMax();
                        _resistance?.Reset();
                    }
                   
                    Icon = null;
                    
                    Disable = true;
                }
               
            }
            if(type == AttributeModifier.BuffType.Passive && !_character.Bag.AllItems.Contains(_item))
            {
              
                _attribute?.SetCurrentToMax();
                _resistance?.Reset();
                Icon = null;
               
                Disable = true;
            }

           
            {
                
            }
        }

        public Status Set(AttributeModifier.BuffType statusType, int duration, Attribute attribute,Character character, Item item)
        {
            Disable = false;
            _item = item;
            _character = character;
            _attribute = attribute;
            type = statusType;
            _duration = _duration > 0 ? _duration += duration : duration;
            Icon = attribute.Icon;
            return this;
        }
        public Status  Set(AttributeModifier.BuffType statusType, int duration, ResistanceClass attribute,Character character,Item item, Sprite sprite)
        {
            Disable = false;
            _item = item;
            _character = character;
            _resistance = attribute;
            type = statusType;
            _duration = _duration > 0 ? _duration += duration : duration;
            Icon = sprite;
            return this;
        }
        public Status  (int duration, Sprite sprite, float damage, Element element)
        {
            Disable = false;
            _damage = damage;
            _duration = _duration > 0 ? _duration += duration : duration;
            Icon = sprite;
            Element = element;
        }

        public Status()
        {
            
        }
    }
