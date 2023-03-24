using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;
using Unity.VisualScripting;

namespace ShijiQuest
{
    public class DanmakuServer : MonoBehaviour
    {
        [Required]
        public Transform spawnAnchorStart, spawnAnchorEnd;
        public Transform deployCanvas;
        private float totalWeight = 0;
        [Min(0.01f)]
        public float mainSpawnPeriodMin = 1, mainSpawnPeriodMax = 4;
        [Range(0.01f, 1)]
        public float spawnPeriodVariation = 0.9f;
        public void StartSpawnProcess(DanmakuBag danmakuBag, float weight = 1, float initialDelay = 0.5f)
        {
            StartCoroutine(CoroSpawnProcess(danmakuBag, weight, initialDelay));
        }
        private IEnumerator CoroSpawnProcess(DanmakuBag danmakuBag, float weight, float initialDelay)
        {
            yield return new WaitForSeconds(initialDelay);
            while(true)
            {
                float w = Mathf.Approximately(totalWeight, 0) ? 0 : weight / totalWeight;
                yield return new WaitForSeconds(Mathf.Lerp(mainSpawnPeriodMin, mainSpawnPeriodMax, 1 - w) * Random.Range(spawnPeriodVariation, 1));
                SpawnDanmakuFromBag(danmakuBag);
            }
        }
        [Inspectable]
        public struct BurstFrame
        {
            public float relativeDelay;
            public int amount;
            public BurstFrame(float relativeDelay, int amount)
            {
                this.relativeDelay = relativeDelay;
                this.amount = amount;
            }
        }
        public void StartSpawnBurstsProcess(DanmakuBag danmakuBag, BurstFrame[] frames, bool loop = false)
        {
            StartCoroutine(CoroSpawnBurstsProcess(danmakuBag, frames, loop));
        }
        private IEnumerator CoroSpawnBurstsProcess(DanmakuBag danmakuBag, BurstFrame[] frames, bool loop)
        {
            foreach(var frame in frames)
            {
                yield return new WaitForSeconds(frame.relativeDelay);
                for(int i = 0; i < frame.amount; ++i) 
                    SpawnDanmakuFromBag(danmakuBag);
            }
        }
        public void StopAll()
        {
            StopAllCoroutines();
            totalWeight = 0;
        }
        public void SpawnDanmakuFromBag(DanmakuBag danmakuBag)
        {
            Vector3 samplePosition = Vector3.Lerp(spawnAnchorStart.position, spawnAnchorEnd.position, Random.value);
            Instantiate(danmakuBag.Sample(), transform).transform.position = samplePosition;
        }
        public void SpawnDanmaku(GameObject prefab, int burstAmount = 1)
        {
            while(burstAmount-- > 0)
            {
                Vector3 samplePosition = Vector3.Lerp(spawnAnchorStart.position, spawnAnchorEnd.position, Random.value);
                Instantiate(prefab, deployCanvas).transform.position = samplePosition;
            }
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
