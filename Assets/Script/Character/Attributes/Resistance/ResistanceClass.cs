using System;
using UnityEngine;

[Serializable]
    public  class ResistanceClass
    {
        public Element Element;
        [Range(-100, 100)] public int amount;
        private int raw;
        private int _duration;
        public void Set()
        {
            raw = amount;
        }

        public void Reset()
        {
            amount = raw;
        }
        public void Add(int i, int duration)
        {
            amount += i;
            _duration = duration;
        }

        public void CheckDuration()
        {
            if (_duration > 0)
                _duration -= 1;
            if(_duration == 0) 
                Reset();
        }
    }
