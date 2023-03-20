using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TriInspector;
using System.Linq;

namespace Freeheart.UI.Yarn
{
    public class YarnDialogueCharacterImage : MonoBehaviour
    {
        [SerializeField, ReadOnly, Required]
        private Image image;
        [System.Serializable]
        private struct CharacterSprite
        {
            [Required]
            public string tag;
            [Required]
            public Sprite sprite;
        }
        [SerializeField, ValidateInput(nameof(ValidateCharacterSprites))]
        private List<CharacterSprite> characterSprites = new();
        private TriValidationResult ValidateCharacterSprites()
        {
            HashSet<string> tags = new();
            foreach(var characterSprite in characterSprites)
                if(!tags.Add(characterSprite.tag)) 
                    return TriValidationResult.Warning($"Detected repeated tag: {characterSprite.tag}");
            return TriValidationResult.Valid;
        }
        [System.Serializable]
        private struct Anchors
        {
            [SerializeField, Required]
            private Transform left, center, right, far_left, far_right;
            public Vector3 NameToPosition(string anchorName)
            {
                switch(anchorName)
                {
                    case nameof(far_left): return far_left.position;
                    case nameof(far_right): return far_right.position;
                    case nameof(left): return left.position;
                    case nameof(center): return center.position;
                    case nameof(right): return right.position;
                    default: throw new System.ArgumentException("Unrecognized anchor name.");
                }
            }
        }
        [SerializeField]
        private Anchors anchors;
        [Title("Image coloring")]
        [SerializeField]
        private Color normalColor = Color.white;
        [SerializeField]
        private Color darkenedColor = Color.gray;
        private Coroutine pendingAsyncMove;
        private void Reset() 
        {
            image = GetComponentInChildren<Image>();
        }
        /// <summary>
        /// Set the character's image variant according to the given imageTag. Common tags: normal, happy, sad
        /// </summary>
        [YarnCommand("set_image")]
        public void SetImage(string imageTag)
        {
            try{
                image.sprite = characterSprites.Where(x => x.tag == imageTag).First().sprite;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
            }
        }
        /// <summary>
        /// Flip the character's image.
        /// </summary>
        [YarnCommand("flip")]
        public void Flip()
        {
            Vector3 localScale = image.transform.localScale;
            localScale.x *= -1;
            image.transform.localScale = localScale;
        }
        /// <summary>
        /// Reset the character's image's flip state.
        /// </summary>
        [YarnCommand("reset_flip")]
        public void ResetFlip()
        {
            Vector3 localScale = image.transform.localScale;
            localScale.x = Mathf.Abs(localScale.x);
            image.transform.localScale = localScale;
        }
        /// <summary>
        /// Snap the character's image to a predefined anchor, specified by anchorName.
        /// </summary>
        [YarnCommand("snap")]
        public void Snap(string anchorName)
        {
            image.transform.position = anchors.NameToPosition(anchorName);
        }
        [YarnCommand("snap_relative")]
        public void SnapRelative(float deltaX, float deltaY)
        {
            image.rectTransform.anchoredPosition += new Vector2(deltaX, deltaY);
        }
        /// <summary>
        /// Start a coroutine that moves the character's image to a predefined anchor, specified by anchorName.
        /// </summary>
        [YarnCommand("move")]
        public IEnumerator Move(string anchorName, float seconds, float easePow)
        {
            float startTime = Time.time;
            float t = 0;
            Vector3 startPosition = image.transform.position;
            Vector3 endPosition = anchors.NameToPosition(anchorName);
            while(t < 1)
            {
                t = (Time.time - startTime) / seconds;
                image.transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.Pow(t, easePow));
                yield return null;
            }
        }
        [YarnCommand("start_move_async")]
        public void StartMoveAsync(string anchorName, float seconds, float easePow)
        {
            StopMoveAsync();
            pendingAsyncMove = StartCoroutine(Move(anchorName, seconds, easePow));
        }
        [YarnCommand("stop_move_async")]
        public void StopMoveAsync()
        {
            if(pendingAsyncMove != null) StopCoroutine(pendingAsyncMove);
        }
        /// <summary>
        /// Start a coroutine that moves the character's image relative to its current position (before move).
        /// </summary>
        [YarnCommand("move_relative")]
        public IEnumerator MoveRelative(float deltaX, float deltaY, float seconds = 0.5f, float easePow = 1.0f)
        {
            float startTime = Time.time;
            float t = 0;
            Vector2 startPosition = image.rectTransform.anchoredPosition;
            Vector2 endPosition = startPosition + new Vector2(deltaX, deltaY);
            while(t < 1)
            {
                t = (Time.time - startTime) / seconds;
                image.rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, Mathf.Pow(t, easePow));
                yield return null;
            }
        }
        [YarnCommand("start_move_relative_async")]
        public void StartMoveRelativeAsync(float deltaX, float deltaY, float seconds, float easePow)
        {
            StopMoveAsync();
            pendingAsyncMove = StartCoroutine(MoveRelative(deltaX, deltaY, seconds, easePow));
        }
        /// <summary>
        /// Darken the character's image's color.
        /// </summary>
        [YarnCommand("color_dark")]
        public void SetColorDarken()
        {
            image.color = darkenedColor;
        }
        /// <summary>
        /// Reset the character's image's color.
        /// </summary>
        [YarnCommand("color_normal")]
        public void SetColorNormal()
        {
            image.color = normalColor;
        }
        /// <summary>
        /// Hide the character's image right away.
        /// </summary>
        [YarnCommand("hide")]
        public void Hide()
        {
            image.enabled = false;
        }
        /// <summary>
        /// Show the character's image right away.
        /// </summary>
        [YarnCommand("show")]
        public void Show()
        {
            Debug.Log($"Show {gameObject.name}");
            image.enabled = true;
        }
        /// <summary>
        /// Set the character's draw priority. Characters with higher priority are drawn first.
        /// </summary>
        /// <param name="order"></param>
        [YarnCommand("set_draw_priority")]
        public void SetDrawPriority(int order)
        {
            order = Mathf.Clamp(order, 0, transform.childCount);
            transform.SetSiblingIndex(order);
        }
        // private void Awake() 
        // {
        //     // Start hidden.
        //     Hide();
        // }
    }
}
