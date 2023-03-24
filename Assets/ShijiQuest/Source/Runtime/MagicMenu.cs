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
    public class MagicMenu : MonoBehaviour
    {
        [Required]
        public CharacterDataRef player;
        [Required]
        public Transform optionContainer;
        [Required]
        public GameObject optionPrefab;
        [Required]
        public TMP_Text MPInfoDisplay;
        [Required]
        public GameObject displayContainer;
        public SpellData selectedSpell;
        private int selectedIndex = 0;
        public void Load(int initialIndex = 0)
        {
            selectedSpell = null;
            foreach(Transform child in optionContainer) Destroy(child.gameObject);
            foreach(var spell in player.value.spells)
            {
                GameObject instance = Instantiate(optionPrefab, optionContainer);
                instance.GetComponent<GameOptionText>().gameOption = spell;
                instance.GetComponent<Selectable>().interactable = spell.canUse;
                var menuItem = instance.GetComponent<MenuItem>();
                menuItem.itemName = spell.localizedName.GetLocalizedString();
                menuItem.onSelect += () => {
                    MPInfoDisplay.text = $"{spell.cost} MP";
                    selectedSpell = spell;
                };
                menuItem.onSubmit += () => {
                    EventBus.Trigger(nameof(EventOnSpellSelected), gameObject, selectedSpell);
                };
            }
            if(player.value.spells.Count > initialIndex)
            {
                selectedIndex = initialIndex;
                selectedSpell = player.value.spells[initialIndex];
            }
        }
        public GameObject MoveIndex2D(int x, int y)
        {
            int totalRows = optionContainer.childCount / 2;
            selectedIndex += x * totalRows + y;
            selectedIndex = Mathf.Clamp(selectedIndex, 0, optionContainer.childCount - 1);
            return optionContainer.GetChild(selectedIndex).gameObject;
        }
        public GameObject RandomStep2D()
        {
            int x = Random.Range(-1, 2);
            int y = (x == 0) ? Random.Range(0, 1) * 2 - 1 : 0;
            return MoveIndex2D(x, y);
        }
        public GameObject MoveIndexTowards(SpellData spellData)
        {
            int currentX = selectedIndex / 2;
            int currentY = selectedIndex % 2;
            int targetIndex = GetComponentsInChildren<MenuItem>().Where(x => x.itemName == spellData.localizedName.GetLocalizedString()).First().transform.GetSiblingIndex();
            int targetX = targetIndex / 2;
            int targetY = targetIndex % 2;
            int x = targetX - currentX;
            if(x != 0) x /= Mathf.Abs(x);
            int y = targetY - currentY;
            if(y != 0) y /= Mathf.Abs(y);
            if(x != 0) y = 0;
            return MoveIndex2D(x, y);
        }
        public GameObject ResetIndex()
        {
            selectedIndex = 0;
            return optionContainer.GetChild(selectedIndex).gameObject;
        }
        public void Show()
        {
            gameObject.SetActive(true);
            MPInfoDisplay.text = "";
            displayContainer.gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            displayContainer.SetActive(false);
        }
    }
}
