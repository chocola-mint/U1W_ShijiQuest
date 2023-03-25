using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "GameManager", menuName = "ShijiQuest/GameManager")]
    public class GameManager : ScriptableObject
    {
        private GameObject selectedGameObject;
        [System.NonSerialized]
        public bool endTurnSignal = false;
        public int turnCounter = 0;
        [System.NonSerialized]
        public BattleLog currentLog;
        public CharacterDataRef player, enemy;
        public StreamerStats streamer;
        public void ResetTurnCounter() => turnCounter = 0;
        public void IncrementTurnCounter() => turnCounter++;
        public bool IsEvenTurn() => turnCounter % 2 == 0;
        public bool IsOddTurn() => !IsEvenTurn();
        public void SetSelectedGameObject(GameObject GO)
        {
            selectedGameObject = GO;
            EventSystem.current.SetSelectedGameObject(GO);
        }
        public void SyncSelectedGameObject()
        {
            selectedGameObject = EventSystem.current.currentSelectedGameObject;
        }
        public void ReinforceSelectLock()
        {
            EventSystem.current.SetSelectedGameObject(selectedGameObject);
        }
        public IEnumerator WaitForEvenTurn()
        {
            yield return new WaitUntil(IsEvenTurn);
        }
        public IEnumerator WaitForOddTurn()
        {
            yield return new WaitUntil(IsOddTurn);
        }
        private WaitUntil waitUntilIsEvenTurn, waitUntilIsOddTurn;
        private void Awake() 
        {
            waitUntilIsEvenTurn = new WaitUntil(IsEvenTurn);
            waitUntilIsOddTurn = new WaitUntil(IsOddTurn);
        }
    }
}
