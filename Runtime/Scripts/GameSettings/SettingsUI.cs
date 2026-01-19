using UnityEngine;
using UnityEngine.Events;


namespace ChukStuph.GameSettings
{
    /// <summary>
    /// Generic base class connecting a SettingValueSO<TValue> to a UI component.
    /// Handles runtime syncing and event subscriptions.
    /// </summary>
    /// <typeparam name="TUI">UI component type (Slider, Toggle, etc.)</typeparam>
    /// <typeparam name="SO">ScriptableObject type (FloatSettingValue, BoolSettingValue, etc.)</typeparam>
    public abstract class SettingsUI<TUI, SO, TValue> : MonoBehaviour where TUI : Component where SO : SettingValueSO<TValue>
    {
        [SerializeField] protected TUI uiComponent;
        [SerializeField] protected SO setting;

        protected UnityAction<TValue> uiCallback;

        protected virtual void Awake()
        {
            if (uiComponent == null)
                uiComponent = GetComponent<TUI>();
        }

        protected virtual void Start()
        {
            if (setting == null) return;

            ApplySettingToUI(setting.Value);
            setting.OnChanged += OnSettingChanged;

            uiCallback = _ => ApplyUIToSetting();
            SubscribeToUI();
        }

        protected virtual void OnDestroy()
        {
            if (setting != null)
                setting.OnChanged -= OnSettingChanged;
            UnsubscribeFromUI();
        }

        protected abstract void SubscribeToUI();
        protected abstract void UnsubscribeFromUI();

        protected abstract void ApplySettingToUI(TValue value);
        protected abstract void ApplyUIToSetting();
        protected abstract TValue GetUIValue();

        private void OnSettingChanged(TValue newValue)
        {
            ApplySettingToUI(newValue);
        }
    }
}