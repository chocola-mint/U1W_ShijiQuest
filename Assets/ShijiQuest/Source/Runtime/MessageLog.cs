using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace ShijiQuest
{
    [RequireComponent(typeof(TMP_Text))]
    public class MessageLog : MonoBehaviour, ISubmitHandler
    {
        private TMP_Text display;
        private Queue<string> messageQueue = new();
        private string pendingMessage = "";
        private int messageCharPointer = 1;
        private float nextPrintCharTime = 0;
        public float printCharPeriod = 0.05f;
        public float linePausePeriod = 0.25f;
        public float speedUpFactor = 4;
        public bool speedUp = false;
        public float actualLinePausePeriod => speedUp ? linePausePeriod / speedUpFactor : linePausePeriod;
        public float actualPrintCharPeriod => speedUp ? printCharPeriod / speedUpFactor : printCharPeriod;
        public bool done => messageQueue.Count <= 0 && currentMessageDone;
        public bool currentMessageDone => messageCharPointer > pendingMessage.Length;
        public bool isCleared => display.text.Length == 0 && pendingMessage.Length == 0;
        public bool isDoneAndCleared => done && isCleared;
        public AudioClip playOnPrintChar;
        public void Enqueue(string message)
        {
            var textInfo = display.GetTextInfo(message);
            if(textInfo.pageCount > 1)
                for(int i = 0; i < textInfo.pageCount; ++i)
                    messageQueue.Enqueue(message.Substring(textInfo.pageInfo[i].firstCharacterIndex, textInfo.pageInfo[i].lastCharacterIndex - textInfo.pageInfo[i].firstCharacterIndex));
            else messageQueue.Enqueue(message);
            // messageQueue.Enqueue(message);
        }
        public void Clear()
        {
            display.text = "";
            pendingMessage = "";
        }
        public void ShowNext()
        {
            gameObject.SetActive(true);
            display.text = "";
            if(messageQueue.TryDequeue(out pendingMessage))
            {
                messageCharPointer = 1;
                speedUp = false;
                nextPrintCharTime = Time.time + actualPrintCharPeriod;
            }
            else pendingMessage = "";
        }
        protected virtual void OnAwake() {}
        private void Awake() 
        {
            TryGetComponent<TMP_Text>(out display);
            Clear();
            display.overflowMode = TextOverflowModes.Page;
            OnAwake();
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(!currentMessageDone && Time.time >= nextPrintCharTime)
            {
                bool isNewLine = pendingMessage[messageCharPointer - 1] == '\n';
                display.text = pendingMessage.Substring(0, messageCharPointer++);
                if(playOnPrintChar)
                    AudioSource.PlayClipAtPoint(playOnPrintChar, Vector3.zero);
                nextPrintCharTime += actualPrintCharPeriod;
                if(isNewLine) nextPrintCharTime += actualLinePausePeriod;
            }

            if(isDoneAndCleared) gameObject.SetActive(false);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if(!currentMessageDone)
            {
                speedUp = true;
            }
            else ShowNext();
        }
    }
}
