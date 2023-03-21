using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ShijiQuest
{
    [RequireComponent(typeof(TMP_Text))]
    public class BattleLog : MonoBehaviour
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
        public void Enqueue(string message)
        {
            messageQueue.Enqueue(message);
        }
        public void Clear()
        {
            display.text = "";
        }
        public void ShowNext()
        {
            if(messageQueue.TryDequeue(out pendingMessage))
            {
                messageCharPointer = 1;
                speedUp = false;
                nextPrintCharTime = Time.time + actualPrintCharPeriod;
            }
        }
        private void Awake() 
        {
            TryGetComponent<TMP_Text>(out display);
            Clear();
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
                nextPrintCharTime += actualPrintCharPeriod;
                if(isNewLine) nextPrintCharTime += actualLinePausePeriod;
            }
        }
    }
}
