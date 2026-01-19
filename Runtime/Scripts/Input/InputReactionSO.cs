using UnityEngine;
using UnityEngine.InputSystem;

namespace ChukStuph.Input
{
    /// <summary>
    /// References an InputAction and defines how to react to it
    /// </summary>
    public abstract class InputReactionSO : ScriptableObject
    {
        /// <summary>
        /// The input signal we listen to
        /// </summary>
        [Header("Input"), Tooltip("The input signal we listen to")]
        public InputActionReference action;

        protected bool enabled;

        public abstract void Enable();
        public abstract void Disable();

        /// <summary>
        /// Should be called once per frame, reads the current value of the InputAction.
        /// Applies deadzone on float and Vector2-based inputs
        /// For buttons, handles OnPressed, OnHeld, OnReleased and Performed
        /// </summary>
        /// <param name="dt">Time in seconds since the last frame</param>
        public abstract void Tick(float dt);
    }
}