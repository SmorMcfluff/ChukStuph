using System;
using UnityEngine;
using UnityEngine.Events;

namespace ChukStuph.Input
{
    [Serializable]
    public class BoundInput
    {
        public InputReactionSO input;

        [Header("Button Events")]
        public UnityEvent onPressed;
        public UnityEvent onHeld;
        public UnityEvent onReleased;

        [Header("Value Events")]
        public UnityEventFloat onValueChangedFloat;
        public UnityEventFloat getValueFloat;

        public UnityEventVector2 onValueChangedVector2;
        public UnityEventVector2 getValueVector2;
    }

    [Serializable]
    public class UnityEventFloat : UnityEvent<float> { }

    [Serializable]
    public class UnityEventVector2 : UnityEvent<Vector2> { }
}