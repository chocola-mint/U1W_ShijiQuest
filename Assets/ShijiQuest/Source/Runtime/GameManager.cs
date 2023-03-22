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
    }
}
