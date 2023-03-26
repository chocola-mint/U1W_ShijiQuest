using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

namespace ShijiQuest
{
    public class ResultManager : MonoBehaviour
    {
        public MessageLog resultDisplay, alertDisplay;
        public LocalizedString alertString;
        public StreamerStats stats;
        // Start is called before the first frame update
        IEnumerator Start()
        {
            resultDisplay.Clear();
            alertDisplay.Clear();
            int trueMentality = Mathf.CeilToInt(stats.mentality * 100);
            string mentalityText = $"残りメンタル：{trueMentality}%（×200）\n";
            string viewsText = $"視聴者数：{stats.viewCount}（×1）\n";
            string subsText = $"登録者数：{stats.subCount}（×10）\n";
            int finalScore = trueMentality * 200
            + stats.viewCount + stats.subCount * 10;
            string finalScoreText = $"最終スコア：{finalScore}";
            resultDisplay.Enqueue(mentalityText + viewsText + subsText + finalScoreText);
            resultDisplay.ShowNext();
            yield return new WaitUntil(() => resultDisplay.done);
            yield return new WaitForSeconds(3.0f);
            alertDisplay.Enqueue(alertString.GetLocalizedString());
            alertDisplay.ShowNext();
        }

        // Update is called once per frame
        void Update()
        {
            if(Keyboard.current.rKey.isPressed) 
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene(0);
            }
        }
    }
}
