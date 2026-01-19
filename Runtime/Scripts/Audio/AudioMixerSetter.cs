using UnityEngine;
using UnityEngine.Audio;

namespace ChukStuph.Audio
{
    /// <summary>
    /// Uses FloatSettingValue Scriptable Objects to set the volume of Audio Mixer Groups
    /// </summary>
    public class AudioMixerSetter : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private MixerParameterSetting[] mixerParameterSettings;

        private void OnValidate()
        {
            foreach (var setting in mixerParameterSettings)
            {
                if (setting.volumeSetting != null && string.IsNullOrEmpty(setting.mixerParameter))
                    setting.mixerParameter = setting.volumeSetting.name;
            }
        }

        private void Start()
        {
            if (mixer == null) return;

            foreach (var setting in mixerParameterSettings)
            {
                if (setting.volumeSetting == null || string.IsNullOrEmpty(setting.mixerParameter)) continue;

                UpdateMixer(setting.mixerParameter, setting.volumeSetting.Value);

                var caughtSetting = setting;

                caughtSetting.changeHandler = value => UpdateMixer(caughtSetting.mixerParameter, value);

                caughtSetting.volumeSetting.OnChanged += caughtSetting.changeHandler;
            }
        }

        private void UpdateMixer(string mixerParam, float linearValue)
        {
            if (mixer == null) return;

            float dB = AudioUtilities.LinearToDecibels(linearValue);
            mixer.SetFloat(mixerParam, dB);
        }

        private void OnDestroy()
        {
            foreach (var setting in mixerParameterSettings)
            {
                if (setting.volumeSetting != null && setting.changeHandler != null)
                    setting.volumeSetting.OnChanged -= setting.changeHandler;
            }
        }
    }
}