using System;
using TMPro;
using UnityEngine;

[Serializable]
    public class DamageStack : ItemProperty
    {
        public int StrenghtMultiplier, AccuracyMultiplier, SupportMultiplier, MagicMultiplier, MusicMultiplier;
        [HideInInspector] public int[] Stack;
        public ItemProperty Initialize()
        {
            Stack = new[] { StrenghtMultiplier,AccuracyMultiplier,MagicMultiplier,SupportMultiplier,MusicMultiplier };
            return this;
        }
        public override void Use(global::Character target, Field field, Item item)
        {
          
        }

        public override TextMeshProUGUI Text(TextMeshProUGUI text)
        {
            return text;
        }

        public override float StatusDamageFill()
        {
            return 0;
        }
    }
