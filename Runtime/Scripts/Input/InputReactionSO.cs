using UnityEngine;
using UnityEngine.InputSystem;

namespace ChukStuph.Input
{
    /// <summary>
    /// References an InputAction and defines how to react to it
    /// </summary>
    public abstract class InputReactionSO : ScriptableObject
    {
        [Header("Input"), Tooltip("The input signal we listen to")]
        public InputActionReference action;

        protected bool enabled;

        public void Enable()
        {
            if (enabled || action?.action == null) return;
            enabled = true;

            action.action.performed += OnPerformed;
            action.action.Enable();

            OnEnableInternal();
        }
        public void Disable()
        {
            if (!enabled || action?.action == null) return;
            enabled = false;

            action.action.performed -= OnPerformed;
            action.action.Disable();

            OnDisableInternal();
        }

        private void OnPerformed(InputAction.CallbackContext ctx)
        {
            LastUsedInput.Device = ctx.control.device;
        }

        protected abstract void OnEnableInternal();
        protected abstract void OnDisableInternal();

        /// <summary>
        /// Gets called once every frame, reads the current value of the InputAction.
        /// Applies deadzone on float and Vector2-based inputs
        /// For buttons, handles OnPressed, OnHeld, OnReleased and Performed
        /// </summary>
        /// <param name="dt">Time in seconds since the last frame</param>
        public abstract void Tick(float dt);
    }
}