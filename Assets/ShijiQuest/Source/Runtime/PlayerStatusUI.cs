using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ShijiQuest
{
    public class PlayerStatusUI : MonoBehaviour
    {
        public CharacterDataRef player;
        public TMP_Text HPValueDisplay, MPValueDisplay;
        // Update is called once per frame
        void Update()
        {
            HPValueDisplay.text = $"{Mathf.RoundToInt(player.value.HP.value)}/{(int)player.value.HP.max.value}";
            MPValueDisplay.text = $"{Mathf.RoundToInt(player.value.MP.value)}/{(int)player.value.MP.max.value}";
        }
    }
}
