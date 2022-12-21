using Script.Enum;
using UnityEngine;


    public abstract class Entity: MonoBehaviour
    {
        [Header("Shop Settings")]
        public EntityType entityType;
        [HideInInspector]public SpriteRenderer Icon;
        public string Name;
        public int ShopCost;
        public int QuantityInShop, QuantityInBag;
        public bool instantUse;
        public bool Bought => QuantityInShop == 0;

        public void SetQuantity(bool buy)
        {
            QuantityInShop = buy ? --QuantityInShop: ++QuantityInShop;
            QuantityInBag = buy ? ++QuantityInBag: --QuantityInBag;
            Debug.Log("buy");
        }
        public abstract void Buy(Character character, bool buy);
        public abstract void Initialize(Spawner starter);
        
      


    }
