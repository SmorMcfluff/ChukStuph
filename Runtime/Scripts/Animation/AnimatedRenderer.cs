using UnityEngine;

namespace ChukStuph.Animation
{
    /// <summary>
    /// An abstract class that can be inherited from to play to any sprite-based renderer
    /// </summary>
    public abstract class AnimatedRenderer<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected T target;
        [SerializeField] protected AnimatedSpriteDataSO spriteSet;

        protected readonly SpriteAnimationPlayer player = new();

        protected virtual void Awake()
        {
            if (target == null)
                target = GetComponent<T>();
        }

        protected virtual void OnEnable()
        {
            if (spriteSet != null)
                SetSpriteSet(spriteSet);
        }

        protected virtual void Update()
        {
            if (player.Step(Time.deltaTime))
                ApplySprite(player.GetCurrentSprite());
        }

        public void SetSpriteSet(AnimatedSpriteDataSO data)
        {
            if (data == null || data.sprites == null || data.sprites.Length == 0)
                return;

            player.SetSpriteSet(data);
            ApplySprite(player.GetCurrentSprite());
        }

        protected abstract void ApplySprite(Sprite sprite);
    }
}
