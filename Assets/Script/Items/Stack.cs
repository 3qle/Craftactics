using System;
using TMPro;
using UnityEngine;

[Serializable]
    public class DamageStack
    {
        public int Strenght, Accuracy, Magic,Spirit, Flow;
        [HideInInspector] public int[] Stack;

        public void Initialize()
        {
            Stack = new[] { Strenght,Accuracy,Magic,Spirit,Flow };
        }
        public float Calculate(Attributes traits)
        {
            return (traits.Get(Trait.Strength).current * Strenght 
                    + traits.Get(Trait.Accuracy).current * Accuracy 
                    + traits.Get(Trait.Magic).current * Magic 
                    + traits.Get(Trait.Spirit).current * Spirit 
                    + traits.Get(Trait.Flow).current * Flow)
                   /100;
        }
    }
