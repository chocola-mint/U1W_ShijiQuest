using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using TriInspector;

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
        public void Load()
        {
            selectedItem = null;
            foreach(Transform child in optionContainer) Destroy(child);
            foreach(var item in player.value.inventory.items)
            {
                if(!item.IsInStock()) continue;
                GameObject instance = Instantiate(optionPrefab, optionContainer);
                instance.GetComponent<GameOptionText>().gameOption = item;
                var menuItem = instance.GetComponent<MenuItem>();
                menuItem.itemName = item.localizedName.GetLocalizedString();
                menuItem.onSelect += () => {
                    itemCountDisplay.gameObject.SetActive(true);
                    itemCountDisplay.text = $"x{item.count}";
                };
                menuItem.onSubmit += () => {
                    selectedItem = item;
                    EventBus.Trigger(nameof(EventOnItemSelected), gameObject, selectedItem);
                };
            }
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
