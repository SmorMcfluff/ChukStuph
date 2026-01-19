using UnityEngine;
using UnityEngine.UI;

namespace ChukStuph.GameSettings
{
    /// <summary>
    /// A generic slider, used to read from/write to a FloatSettingValue
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class SettingsUISlider : SettingsUI<Slider, FloatSettingValueSO, float>
    {
        protected override void ApplySettingToUI(float value)
        {
            uiComponent.SetValueWithoutNotify(Mathf.Lerp(uiComponent.minValue, uiComponent.maxValue, value));
        }

        protected override void ApplyUIToSetting()
        {
            float sliderValue = uiComponent.value;
            float linear = Mathf.InverseLerp(uiComponent.minValue, uiComponent.maxValue, sliderValue);

            if (setting.HasRange)
                linear = Mathf.Clamp(linear, setting.Min, setting.Max);

            setting.Value = linear;
        }

        protected override float GetUIValue() => Mathf.InverseLerp(uiComponent.minValue, uiComponent.maxValue, uiComponent.value);

        protected override void SubscribeToUI()
        {
            uiComponent.onValueChanged.AddListener(uiCallback);
        }

        protected override void UnsubscribeFromUI()
        {
            uiComponent.onValueChanged.RemoveListener(uiCallback);
        }
    }
}