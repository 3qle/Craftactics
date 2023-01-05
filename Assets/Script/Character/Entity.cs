using Script.Enum;
using UnityEngine;


    public abstract class Entity: MonoBehaviour
    {
        [Header("Shop Settings")]
        public EntityType entityType;
        [HideInInspector]public SpriteRenderer Icon;
        public string Name;
        public int ShopCost;
        public int QuantityInShop;
        protected Pool pool;
        public int IndexOfCategory;
        public int GetAmount() => pool.GetAmount(IndexOfCategory);
        public bool Bought;

        public abstract void Buy(Character character, bool buy);
        public abstract void Initialize(Spawner starter, int i);

        public virtual bool TrySell()
        {
            return true;
        }


    }
