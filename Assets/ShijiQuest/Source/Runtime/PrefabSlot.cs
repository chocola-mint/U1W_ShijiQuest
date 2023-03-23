using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShijiQuest
{
    public class PrefabSlot : MonoBehaviour
    {
        public GameObject InstantiatePrefab(GameObject prefab)
        {
            foreach(Transform child in transform) Destroy(child.gameObject);
            return Instantiate(prefab, transform);
        }
        public GameObject currentInstance => 
        transform.childCount <= 0 ? null : transform.GetChild(0).gameObject;
    }
}
