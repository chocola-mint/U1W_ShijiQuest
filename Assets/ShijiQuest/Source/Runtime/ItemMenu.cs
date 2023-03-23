using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using TriInspector;
using System.Linq;
using UnityEngine.UI;

namespace ShijiQuest
{
    public class ItemMenu : MonoBehaviour
    {
        [Required]
        public CharacterDataRef player;
        [Required]
        public Transform optionContainer;
        [Required]
        public GameObject optionPrefab;
        [Required]
        public TMP_Text itemCountDisplay;
        public ItemData selectedItem;
        private int selectedIndex = 0;
        public void Load(int initialIndex = 0)
        {
            selectedItem = null;
            foreach(Transform child in optionContainer) Destroy(child.gameObject);
            foreach(var item in player.value.inventory.items)
            {
                if(!item.IsInStock()) continue;
                GameObject instance = Instantiate(optionPrefab, optionContainer);
                instance.GetComponent<GameOptionText>().gameOption = item;
                instance.GetComponent<Selectable>().interactable = item.IsInStock();
                var menuItem = instance.GetComponent<MenuItem>();
                menuItem.itemName = item.localizedName.GetLocalizedString();
                menuItem.onSelect += () => {
                    itemCountDisplay.gameObject.SetActive(true);
                    itemCountDisplay.text = $"x{item.count}";
                    selectedItem = item;
                };
                menuItem.onSubmit += () => {
                    EventBus.Trigger(nameof(EventOnItemSelected), gameObject, selectedItem);
                };
            }
            if(player.value.inventory.items.Count > initialIndex)
            {
                selectedIndex = initialIndex;
                selectedItem = player.value.inventory.items[initialIndex];
            }
        }
        public GameObject MoveIndex2D(int x, int y)
        {
            int totalRows = transform.childCount;
            selectedIndex += x * totalRows + y;
            selectedIndex = Mathf.RoundToInt(Mathf.Repeat(selectedIndex, transform.childCount)) % transform.childCount;
            return transform.GetChild(selectedIndex).gameObject;
        }
        public GameObject RandomStep2D()
        {
            int x = Random.Range(-1, 2);
            int y = (x == 0) ? Random.Range(0, 1) * 2 - 1 : 0;
            return MoveIndex2D(x, y);
        }
        public GameObject MoveIndexTowards(ItemData itemData)
        {
            int currentX = selectedIndex / 2;
            int currentY = selectedIndex % 2;
            int targetIndex = GetComponentsInChildren<TMP_Text>().Where(x => x.text == itemData.localizedName.GetLocalizedString()).First().transform.GetSiblingIndex();
            int targetX = targetIndex / 2;
            int targetY = targetIndex % 2;
            return MoveIndex2D((int)Mathf.Sign(targetX - currentX), (int)Mathf.Sign(targetY - currentY));
        }
        public GameObject ResetIndex()
        {
            selectedIndex = 0;
            return transform.GetChild(selectedIndex).gameObject;
        }
        public void Show()
        {
            gameObject.SetActive(true);
            itemCountDisplay.text = "";
            itemCountDisplay.gameObject.SetActive(false);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
