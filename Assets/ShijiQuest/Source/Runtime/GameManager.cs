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
