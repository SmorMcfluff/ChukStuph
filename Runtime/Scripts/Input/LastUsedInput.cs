using System;
using UnityEngine.InputSystem;

namespace ChukStuph.Input
{
    public static class LastUsedInput
    {
        public static event Action<InputDevice> OnInputDeviceChanged;
        public static event Action<InputScheme> OnControlSchemeChanged;


        private static InputDevice device;
        private static InputScheme scheme = InputScheme.Unknown;

        public static InputDevice Device
        {
            get => device;
            internal set
            {
                if (device == value) return;

                device = value;
                OnInputDeviceChanged?.Invoke(device);

                var newScheme = ResolveScheme(device);

                if (newScheme != scheme)
                {
                    scheme = newScheme;
                    OnControlSchemeChanged?.Invoke(scheme);
                }
            }
        }

        public static InputScheme Scheme => scheme;

        private static InputScheme ResolveScheme(InputDevice device)
        {
            if (device is Gamepad) return InputScheme.Gamepad;
            if (device is Keyboard || device is Mouse) return InputScheme.KeyboardMouse;
            return InputScheme.Unknown;
        }
    }

    public enum InputScheme
    {
        KeyboardMouse,
        Gamepad,
        Unknown
    }
}
