using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChukStuph.Input
{
    /// <summary>
    /// OnPressed: fired immediately when the button is first pressed.<br />
    /// OnHeld: fired every frame when the button is held
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
        /// Invoked when input is held (OnButton)
        /// </summary>
        public event Action OnHeld;     // """"HENRY""""

        /// <summary>
        /// Invoked when input is released (OnButtonUp)
        /// </summary>
        public event Action OnReleased; // Yoshi

        public bool IsHeld => isHeld;

        protected bool isHeld;

        protected override void OnEnableInternal()
        {
            action.action.started += OnStarted;
            action.action.canceled += OnCanceled;
        }

        protected override void OnDisableInternal()
        {
            action.action.started -= OnStarted;
            action.action.canceled -= OnCanceled;

            isHeld = false;
        }

        /// <summary>
        /// Called when the input starts (OnButtonDown)
        /// </summary>
        /// <param name="ctx">Information about what triggered the input</param>
        private void OnStarted(InputAction.CallbackContext ctx)
        {
            isHeld = true;
            OnPressed?.Invoke();
        }

        /// <summary>
        /// Called when the input is canceled (OnButtonUp)
        /// </summary>
        /// <param name="ctx">Information about what triggered the input</param>
        private void OnCanceled(InputAction.CallbackContext ctx)
        {
            isHeld = false;
            OnReleased?.Invoke();
        }

        public override void Tick(float dt)
        {
            if (!isHeld) return;
                OnHeld?.Invoke();
        }
    }
}