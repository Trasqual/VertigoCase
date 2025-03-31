using System;
using UnityEngine;

namespace ItemSystem
{
    [Serializable]
    public class Item
    {
        [field: SerializeField] public ItemData ItemData { get; private set; }

        [field: SerializeField] public int Amount { get; private set; }
        
        public void AddAmount(int amount)
        {
            Amount += amount;
        }

        public Item Copy()
        {
            return new Item
            {
                    ItemData = ItemData,
                    Amount = Amount
            };
        }
    }
}