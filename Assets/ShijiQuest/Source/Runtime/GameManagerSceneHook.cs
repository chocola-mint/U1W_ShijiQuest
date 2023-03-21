using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{
    public class GameManagerSceneHook : MonoBehaviour
    {
        [Required]
        public GameManager gameManager;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void LateUpdate()
        {
            gameManager.ReinforceSelectLock();
        }
    }
}
