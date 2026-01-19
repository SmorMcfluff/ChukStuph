using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChukStuph.Input
{
    /// <summary>
    /// OnPressed: fired immediately when the button is first pressed.<br />
    /// OnHeld: fired repeatedly after holdThreshold, every repeatRate seconds while held.<br />
    /// OnReleased: fired once when button is released.<br />
    /// Performed: fired alongside each of the above events for general use.
    /// </summary>
    [CreateAssetMenu(fileName = "Button Input", menuName = "Chuk Essentials/Input/Button Input")]
    public class InputReactionButtonSO : InputReactionSO
    {
        /// <summary>
        /// Invoked when input starts (OnButtonDown)
        /// </summary>
        public event Action OnPressed;  //TJ

        /// <summary>
        /// Invoked when input is held (after holdThreshold)
        /// </summary>
        public event Action OnHeld;     // """"HENRY""""

        /// <summary>
        /// Invoked when input is released (OnButtonUp)
        /// </summary>
        public event Action OnReleased; // Yoshi

        /// <summary>
        /// Invoked whenever input is signaled, regardless of phase (OnButton)
        /// </summary>
        public event Action Performed;

        public bool IsHeld => isHeld;

        [Tooltip("How long we need to hold the button before repeating")]
        public float holdThreshold = 0f;

        [Tooltip("How often the signal is repeated")]
        public float repeatRate = 0.1f;

        protected bool isHeld;
        protected float holdTimer;
        protected float repeatTimer;

        public override void Enable()
        {
            if (enabled || action == null) return;
            enabled = true;

            action.action?.Enable();
            action.action.started += OnStarted;
            action.action.canceled += OnCanceled;
        }

        public override void Disable()
        {
            if (!enabled || action == null) return;
            enabled = false;

            action.action?.Disable();
            action.action.started -= OnStarted;
            action.action.canceled -= OnCanceled;

            isHeld = false;
            holdTimer = 0f;
            repeatTimer = 0f;
        }

        /// <summary>
        /// Called when the input starts (OnButtonDown)
        /// </summary>
        /// <param name="ctx">Information about what triggered the input</param>
        private void OnStarted(InputAction.CallbackContext ctx)
        {
            holdTimer = 0f;
            repeatTimer = 0f;
            isHeld = true;

            OnPressed?.Invoke();
            Performed?.Invoke();
        }

        /// <summary>
        /// Called when the input is canceled (OnButtonUp)
        /// </summary>
        /// <param name="ctx">Information about what triggered the input</param>
        private void OnCanceled(InputAction.CallbackContext ctx)
        {
            isHeld = false;

            OnReleased?.Invoke();
            Performed?.Invoke();
        }

        public override void Tick(float dt)
        {
            if (!isHeld) return;

            holdTimer += dt;
            repeatTimer += dt;

            if (holdTimer < holdThreshold) return;
                if (repeatTimer >= repeatRate || holdThreshold == 0f)
                {
                    repeatTimer = 0f;
                    OnHeld?.Invoke();
                    Performed?.Invoke();
                }
        }
    }
}