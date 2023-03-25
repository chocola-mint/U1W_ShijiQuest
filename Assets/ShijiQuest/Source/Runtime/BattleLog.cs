using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace ShijiQuest
{
    public class BattleLog : MessageLog
    {
        public GameManager gameManager;
        protected override void OnAwake()
        {
            base.OnAwake();
            gameManager.currentLog = this;
        }
        private void OnDestroy() 
        {
            gameManager.currentLog = null;
        }
    }
}
