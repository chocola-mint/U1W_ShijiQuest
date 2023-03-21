using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShijiQuest
{
    public class ItemData : NestedGameOption
    {
        [Min(0)]
        public int count = 3;
        public CharacterDataRef owner;
        public bool IsInStock() => count > 0;
        public void Consume()
        {
            if(IsInStock())
            {
                --count;
                OnConsume();
                if(!IsInStock())
                    owner.value.inventory.items.Remove(this);
            }
        }
        protected virtual void OnConsume() {}
    }
}
