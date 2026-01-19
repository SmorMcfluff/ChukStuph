using UnityEngine;

namespace ChukStuph.Animation
{
    /// <summary>
    /// Animates a Sprite Renderer component using a set of sprites defined in `AnimatedSpriteData`.
    /// Supports multiple loop types and dynamic sprite set switching.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class AnimatedSpriteRenderer : AnimatedRenderer<SpriteRenderer>
    {
        protected override void ApplySprite(Sprite sprite)
        {
            target.sprite = sprite;
        }
    }
}