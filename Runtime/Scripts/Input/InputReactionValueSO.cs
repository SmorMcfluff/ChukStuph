using System;
using UnityEngine;

namespace ChukStuph.Input
{
    public abstract class InputReactionValueSO<T> : InputReactionSO where T : struct
    {
        public T CurrentValue { get; private set; }
        public event Action<T> OnValueChanged;
        public event Action<T> GetValue;

        private T lastValue;
        [Header("Deadzone"), Range(0f, 1f)]
        protected float deadzone = 0.1f;
        protected abstract bool CheckDeadzone(T val);

        public override void Enable()
        {
            action.action?.Enable();
        }

        public override void Disable()
        {
            action.action?.Disable();
        }

        /// <summary>
        /// Continuously reads the input value every frame and triggers OnValueChanged if it changed.
        /// </summary>
        public override void Tick(float dt)
        {
            if (action == null || action.action == null) return;


            T newValue = action.action.ReadValue<T>();

            if (CheckDeadzone(newValue))
                newValue = default;

            CurrentValue = newValue;

            if (!newValue.Equals(lastValue))
            {
                lastValue = newValue;
                OnValueChanged?.Invoke(newValue);
            }

            GetValue?.Invoke(CurrentValue);
        }
    }
}