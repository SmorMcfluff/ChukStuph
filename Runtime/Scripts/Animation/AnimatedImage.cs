using UnityEngine;
using UnityEngine.UI;

namespace ChukStuph.Animation
{
    /// <summary>
    /// Animates a Unity UI Image component using a set of sprites defined in `AnimatedSpriteData`.
    /// Supports multiple loop types and dynamic sprite set switching.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class AnimatedImage : AnimatedRenderer<Image>
    {
        protected override void ApplySprite(Sprite sprite)
        {
            target.sprite = sprite;
        }
    }
}