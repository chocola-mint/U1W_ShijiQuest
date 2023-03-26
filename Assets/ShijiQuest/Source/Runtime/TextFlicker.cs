using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ShijiQuest
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextFlicker : MonoBehaviour
    {
        // Start is called before the first frame update
        IEnumerator Start()
        {
            if(TryGetComponent<TMP_Text>(out var textDisplay))
            {
                while(true)
                {
                    textDisplay.enabled = true;
                    yield return new WaitForSeconds(1.5f);
                    textDisplay.enabled = false;
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

    }
}
