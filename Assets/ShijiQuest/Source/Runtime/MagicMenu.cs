using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using TriInspector;

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
        public SpellData selectedSpell;
        public void Load()
        {
            selectedSpell = null;
            foreach(Transform child in optionContainer) Destroy(child);
            foreach(var spell in player.value.spells)
            {
                GameObject instance = Instantiate(optionPrefab, optionContainer);
                instance.GetComponent<GameOptionText>().gameOption = spell;
                var menuItem = instance.GetComponent<MenuItem>();
                menuItem.itemName = spell.localizedName.GetLocalizedString();
                menuItem.onSelect += () => {
                    MPInfoDisplay.text = $"{spell.cost} MP";
                };
                menuItem.onSubmit += () => {
                    selectedSpell = spell;
                    EventBus.Trigger(nameof(EventOnSpellSelected), gameObject, selectedSpell);
                };
            }
        }
        public void Show()
        {
            gameObject.SetActive(true);
            MPInfoDisplay.text = "";
            MPInfoDisplay.gameObject.SetActive(false);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
