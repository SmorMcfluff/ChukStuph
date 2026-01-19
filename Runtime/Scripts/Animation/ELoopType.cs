using UnityEngine;

namespace ChukStuph.Animation
{
    /// <summary>
    /// Defines how an animation should loop.
    /// Use in `AnimatedImage` or `AnimatedSprite`.
    /// </summary>
    public enum ELoopType
    {
        [InspectorName("Normal: 1-2-3-1-2-3")]
        Normal,

        [InspectorName("PingPong: 1-2-3-2-1")]
        PingPong,

        [InspectorName("StickyPingPong: 1-2-3-3-2-1-1-2...")]
        StickyPingPong,

        [InspectorName("PlayOnce: 1-2-3")]
        PlayOnce,

        [InspectorName("PlayOnceAndReset: 1-2-3-1")]
        PlayOnceAndReset,

        [InspectorName("None: 1")]
        None = 99
    }
}
