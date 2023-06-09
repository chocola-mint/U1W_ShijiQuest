using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "ShijiQuest/Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<ItemData> items = new();
        public IEnumerable<ItemData> GetUsableItems() => items.Where(x => x.IsInStock());
        public void Add(ItemData itemData)
        {
            itemData.count++;
            if(!items.Contains(itemData)) items.Add(itemData);
        }
        public void Add(ItemData itemData, int count)
        {
            itemData.count += count;
            if(!items.Contains(itemData)) items.Add(itemData);
        }
        public void Clear()
        {
            foreach(var item in items) item.count = 0;
            items.Clear();
        }
    }
}
