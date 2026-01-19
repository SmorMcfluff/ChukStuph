using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ChukStuph.Utilities
{
    public static class UIUtilities
    {
        public static void SafeAddListener(Slider slider, Action<float> listener)
        {
            if (slider != null)
                slider.onValueChanged.AddListener(new UnityAction<float>(listener));
        }

        public static void SafeAddListener(Button button, Action listener)
        {
            if (button != null)
                button.onClick.AddListener(new UnityAction(listener));
        }
    }
}
