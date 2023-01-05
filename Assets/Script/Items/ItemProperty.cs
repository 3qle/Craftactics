
    using TMPro;
    using UnityEngine;

    public abstract class ItemProperty
    {
        public bool Using;
        public abstract void Use(Character target, Field field, Item item);
        public Sprite Icon;
        public float Points;
        public int Duration;
       
        public abstract TextMeshProUGUI Text(TextMeshProUGUI text);

        public abstract float StatusDamageFill();
    }
