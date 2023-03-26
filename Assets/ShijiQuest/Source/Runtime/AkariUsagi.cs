using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;
using ChocoUtil.Algorithms;
using CRandom = ChocoUtil.Algorithms.Random;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace ShijiQuest
{
    [RequireComponent(typeof(Image))]
    public class AkariUsagi : MonoBehaviour
    {
        public enum Action
        {
            None,
            Attack1, Attack2, Attack3,
            HypnosisPre, Hypnosis, HypnosisAttack,
        }
        [System.NonSerialized]
        public Action action;
        [Required]
        public GameManager gameManager;
        [Required]
        public CharacterData characterData;
        private Image image;
        [System.Serializable]
        public struct FlashSprite
        {
            public Sprite normal, flash;
        }
        public FlashSprite initial, hypnosisPre, hypnosis;
        private void Awake() 
        {
            TryGetComponent<Image>(out image);
            image.sprite = initial.normal;
        }
        public IEnumerator CoroFlash(FlashSprite flashSprite)
        {
            for(int i = 0; i < 2; ++i)
            {
                image.sprite = flashSprite.flash;
                yield return new WaitForSeconds(0.1f);
                image.sprite = flashSprite.normal;
                yield return new WaitForSeconds(0.1f);
            }
        }
        public bool MatchAction(Action action)
        {
            if(this.action == action)
            {
                // this.action = Action.None;
                return true;
            }
            else return false;
        }
        public void StartScript()
        {
            StartCoroutine(Script());
        }
        private Action SampleTable1(float w1, float w2, float w3)
        {
            return CRandom.Select(new WeightedValue<Action>[]
            {
                new(w1, Action.Attack1), 
                new(w2, Action.Attack2),
                new(w3, Action.Attack3)
            });
        }
        private IEnumerator Script()
        {
            yield return AttackPhase(1.0f, 0, 0, () => characterData.HP.GetPercentage() <= 0.5f);
            yield return TryHypnosis(); 
            yield return AttackPhase(1, 1, 0.5f, () => characterData.HP.GetPercentage() <= 0.2f);
            yield return TryHypnosis(); 
            yield return AttackPhase(0.5f, 2, 2, () => characterData.HP.GetPercentage() <= 0);
        }
        private IEnumerator AttackPhase(float w1, float w2, float w3, System.Func<bool> stopCondition)
        {
            while(!stopCondition())
            {
                if(characterData.HP.value > 0.5f)
                    action = SampleTable1(w1, w2, w3);
                else action = SampleTable1(w1 * 0.5f, w2, w3);
                StartCoroutine(CoroFlash(initial));
                if(action == Action.Attack1)
                {
                    w2 += 0.5f;
                    w3 += 0.25f;
                }
                else if(action == Action.Attack2)
                {
                    w3 = (w3 + 0.25f) * 2;
                    w1 *= 0.5f;
                    w2 *= 0.75f;
                }
                else if(action == Action.Attack3)
                {
                    w1 = 1; w2 = 0; w3 = 0;
                }
                
                if(w1 + w2 + w3 > 15)
                {
                    w1 = w2 = w3 = 1;
                }
                yield return WaitForNextEnemyTurn();
            }
        }
        private IEnumerator TryHypnosis()
        {
            action = Action.HypnosisPre;
            StartCoroutine(CoroFlash(hypnosisPre));
            yield return WaitForNextEnemyTurn();
            action = Action.Hypnosis;
            StartCoroutine(CoroFlash(hypnosis));
            yield return WaitForNextEnemyTurn();
            float sleepChance = 0.7f;
            int turnsElapsed = 0;
            while(gameManager.player.value.MatchStatus(CharacterData.Status.Asleep))
            {
                action = Action.HypnosisAttack;
                StartCoroutine(CoroFlash(hypnosis));

                if(Random.value > Mathf.Pow(sleepChance, turnsElapsed++)) 
                {
                    gameManager.player.value.AssignStatus(CharacterData.Status.Normal);
                    break;
                }
                yield return WaitForNextEnemyTurn();
            }
            image.sprite = initial.normal;
        }
        private IEnumerator WaitForNextEnemyTurn()
        {
            yield return gameManager.WaitForEvenTurn();
            action = Action.None;
            yield return gameManager.WaitForOddTurn();
        }
        private void LateUpdate() 
        {
            if(Mathf.RoundToInt(characterData.HP.value) <= 0) StopAllCoroutines();
        }
    }
}
