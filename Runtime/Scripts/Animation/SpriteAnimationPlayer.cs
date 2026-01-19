using UnityEngine;

namespace ChukStuph.Animation
{
    /// <summary>
    /// Handles 2D animation logic for AnimatedRenderers
    /// </summary>
    public sealed class SpriteAnimationPlayer
    {
        private Sprite[] sprites;
        private float frameDuration;
        private ELoopType loopType;

        private float timer;
        private int currentFrame;
        private int direction;
        private bool finished;

        public int CurrentFrame => currentFrame;
        public bool IsFinished => finished;
        public bool HasSprites => sprites != null && sprites.Length > 0;

        /// <summary>
        /// Changes the active sprite set to a new AnimatedSpriteData asset.
        /// Resets animation to the start of the new set.
        /// </summary>
        public void SetSpriteSet(AnimatedSpriteDataSO spriteSet)
        {
            sprites = spriteSet.sprites;
            frameDuration = Mathf.Max(spriteSet.frameDuration, 0.0001f);
            loopType = spriteSet.loopType;
            Reset();
        }

        public void Reset()
        {
            timer = 0f;
            currentFrame = 0;
            direction = 1;
            finished = false;
        }

        /// <summary>
        /// Tries to step to the next frame, if it's time
        /// </summary>
        /// <returns>True if the frame changed during this step</returns>
        public bool Step(float deltaTime)
        {
            if (!HasSprites || finished || loopType == ELoopType.None)
                return false;

            bool frameChanged = false;
            timer += deltaTime;

            while (timer >= frameDuration)
            {
                timer -= frameDuration;
                frameChanged |= Advance();
            }

            return frameChanged;
        }

        private bool Advance()
        {
            int previous = currentFrame;

            switch (loopType)
            {
                case ELoopType.Normal:
                    currentFrame = (currentFrame + 1) % sprites.Length;
                    break;

                case ELoopType.PlayOnce:
                    AdvanceOnce(reset: false);
                    break;

                case ELoopType.PlayOnceAndReset:
                    AdvanceOnce(reset: true);
                    break;

                case ELoopType.PingPong:
                    AdvancePingPong(sticky: false);
                    break;

                case ELoopType.StickyPingPong:
                    AdvancePingPong(sticky: true);
                    break;
            }

            return currentFrame != previous;
        }

        /// <summary>
        /// Steps to the next frame, unless the animation is finished
        /// </summary>
        private void AdvanceOnce(bool reset)
        {
            if (currentFrame < sprites.Length - 1)
                currentFrame++;
            else
            {
                finished = true;
                if (reset)
                    currentFrame = 0;
            }
        }

        /// <summary>
        /// Steps to the next frame, and turns around when it reaches the end
        /// </summary>
        /// <param name="sticky">Whether or not the animation should play the first and last frame twice before looping</param>
        private void AdvancePingPong(bool sticky)
        {
            currentFrame += direction;

            if (currentFrame >= sprites.Length)
            {
                currentFrame = sticky
                    ? sprites.Length - 1
                    : Mathf.Max(sprites.Length - 2, 0);
                direction = -1;
            }
            else if (currentFrame < 0)
            {
                currentFrame = sticky
                    ? 0
                    : Mathf.Min(1, sprites.Length - 1);
                direction = 1;
            }
        }

        public Sprite GetCurrentSprite() =>
            HasSprites ? sprites[currentFrame] : null;
    }
}