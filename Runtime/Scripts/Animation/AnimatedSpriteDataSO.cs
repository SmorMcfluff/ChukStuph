using UnityEngine;

namespace ChukStuph.Animation
{
    /// <summary>
    /// Data used to animate a sprite. <br />
    /// Contains the individual frame, long each each frame should last, and if/how the sprite should loop
    /// </summary>
    [CreateAssetMenu(fileName = "Animated Sprite", menuName = "Chuk Essentials/Animation/SpriteData")]
    public class AnimatedSpriteDataSO : ScriptableObject
    {
        [Tooltip("The name of the sprite set, can be used to generate a dictionary")]
        public string setName;

        [Tooltip("The actual sprites making up the sprite set")]
        public Sprite[] sprites;

        [Tooltip("How long each frame should last (in seconds)")]
        public float frameDuration;

        public ELoopType loopType;

        /// <summary>
        /// The length of one loop in seconds
        /// </summary>
        public float Duration { get; private set; }

        /// <summary>
        /// Returns the length of one loop in seconds
        /// </summary>
        private float CalculateDuration()
        {
            if (sprites == null || sprites.Length == 0)
                return 0f;

            if (sprites.Length == 1)
            {
                return frameDuration;
            }

            return loopType switch
            {
                ELoopType.Normal => frameDuration * sprites.Length,
                ELoopType.PingPong => frameDuration * (2 * sprites.Length - 2),
                ELoopType.StickyPingPong => frameDuration * (sprites.Length * 2),
                ELoopType.PlayOnce => frameDuration * sprites.Length,
                ELoopType.PlayOnceAndReset => frameDuration * sprites.Length,
                ELoopType.None => 0,
                _ => 0
            };
        }

        private void OnEnable()
        {
            frameDuration = Mathf.Max(frameDuration, 0.0001f);
            Duration = CalculateDuration();
        }

        private void OnValidate()
        {
            Duration = CalculateDuration();
        }
    }
}
