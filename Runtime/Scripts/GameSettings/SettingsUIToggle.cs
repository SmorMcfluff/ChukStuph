using UnityEngine;
using UnityEngine.UI;

namespace ChukStuph.GameSettings
{
    /// <summary>
    /// A generic toggle, used to read from or edit to a BoolSettingValue
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class SettingsUIToggle : SettingsUI<Toggle, BoolSettingValueSO, bool>
    {
        protected override void SubscribeToUI()
        {
            uiComponent.onValueChanged.AddListener(uiCallback);
        }

        protected override void UnsubscribeFromUI()
        {
            uiComponent.onValueChanged.RemoveListener(uiCallback);
        }

        protected override void ApplySettingToUI(bool value)
        {
            uiComponent.SetIsOnWithoutNotify(value);
        }

        protected override void ApplyUIToSetting()
        {
            setting.Value = uiComponent.isOn;
        }

        protected override bool GetUIValue() => uiComponent.isOn;
    }
}