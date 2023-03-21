using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{
    public class DanmakuServer : MonoBehaviour
    {
        [Required]
        public Transform spawnAnchorStart, spawnAnchorEnd;
        public void StartSpawnProcess(DanmakuBag danmakuBag, float spawnPeriodMin, float spawnPeriodMax)
        {
            StartCoroutine(CoroSpawnProcess(danmakuBag, spawnPeriodMin, spawnPeriodMax));
        }
        public void StopAll()
        {
            StopAllCoroutines();
        }
        IEnumerator CoroSpawnProcess(DanmakuBag danmakuBag, float spawnPeriodMin, float spawnPeriodMax)
        {
            while(true)
            {
                yield return new WaitForSeconds(Random.Range(spawnPeriodMin, spawnPeriodMax));
                SpawnDanmaku(danmakuBag);
            }
        }
        private void SpawnDanmaku(DanmakuBag danmakuBag)
        {
            Vector3 samplePosition = Vector3.Lerp(spawnAnchorStart.position, spawnAnchorEnd.position, Random.value);
            Instantiate(danmakuBag.Sample(), transform).transform.position = samplePosition;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnDrawGizmosSelected() 
        {
            if(spawnAnchorStart && spawnAnchorEnd)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(spawnAnchorStart.position, spawnAnchorEnd.position);
            }
        }
    }
}
