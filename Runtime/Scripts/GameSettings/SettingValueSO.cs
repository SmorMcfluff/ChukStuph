using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChukStuph.GameSettings
{
    public abstract class SettingValueSO<T> : ScriptableObject
    {
        [SerializeField] protected bool saveToPlayerPrefs = true;
        [SerializeField] protected string playerPrefsKey;

        [SerializeField] protected T value;
        private T runtimeValue;

        public event Action<T> OnChanged;

        /// <summary>
        /// Runtime-safe value. Changes in play mode don't modify the asset.
        /// </summary>
        public virtual T Value
        {
            get => runtimeValue;
            set
            {
                if (EqualityComparer<T>.Default.Equals(runtimeValue, value)) return;

                runtimeValue = value;
                SaveToPrefs();
                OnChanged?.Invoke(runtimeValue);
            }
        }

        private void OnEnable()
        {
            runtimeValue = value;
            LoadFromPrefs();
        }

        private void SaveToPrefs()
        {
            if (!saveToPlayerPrefs) return;

            if (string.IsNullOrEmpty(playerPrefsKey))
                playerPrefsKey = name;

            PlayerPrefs.SetString(playerPrefsKey, ConvertToString(runtimeValue));
            PlayerPrefs.Save();
        }

        protected void LoadFromPrefs()
        {
            if (!saveToPlayerPrefs) return;

            if (string.IsNullOrEmpty(playerPrefsKey))
                playerPrefsKey = name;

            if (!PlayerPrefs.HasKey(playerPrefsKey)) return;

            ApplyLoadedDataFromPrefs();
        }

        protected virtual void ApplyLoadedDataFromPrefs()
        {
            string saved = PlayerPrefs.GetString(playerPrefsKey);
            runtimeValue = ConvertFromString(saved);
            OnChanged?.Invoke(runtimeValue);
        }

        protected abstract string ConvertToString(T value);
        protected abstract T ConvertFromString(string str);

        public abstract void ResetToDefault();

        public T Default => value;
    }
}