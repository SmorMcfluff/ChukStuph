using UnityEngine;
using ChukStuph.GameSettings;
using System;

namespace ChukStuph.Audio
{
    /// <summary>
    /// Pairs a MixerGroup with a FloatSettingValue, used in the AudioMixerSetter component to automagically set the volume
    /// </summary>
    [Serializable]
    public class MixerParameterSetting
    {
        [Tooltip("The name of the Mixer Group we connect to")]
        [SerializeField] public string mixerParameter;
        [Tooltip("The FloatSettingValue Scriptable Object we read the value of")]
        [SerializeField] public FloatSettingValueSO volumeSetting;

        /// <summary>
        /// The action that handles the changing of MixerGroup volume
        /// </summary>
        [NonSerialized] public Action<float> changeHandler;
    }
}