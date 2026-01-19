using UnityEngine;
using TMPro;

namespace ChukStuph.GameSettings
{
    /// <summary>
    /// A generic input field, used to read from or edit a StringSettingValue
    /// </summary>

    [RequireComponent(typeof(TMP_InputField))]
    public class SettingsUIInputField : SettingsUI<TMP_InputField, StringSettingValueSO, string>
    {
        protected override void SubscribeToUI()
        {
            uiComponent.onValueChanged.AddListener(uiCallback);
        }

        protected override void UnsubscribeFromUI()
        {
            uiComponent.onValueChanged.RemoveListener(uiCallback);

        }

        protected override void ApplySettingToUI(string value)
        {
            uiComponent.SetTextWithoutNotify(value);
        }

        protected override void ApplyUIToSetting()
        {
            setting.Value = uiComponent.text;
        }

        protected override string GetUIValue() => uiComponent.text;
    }
}
