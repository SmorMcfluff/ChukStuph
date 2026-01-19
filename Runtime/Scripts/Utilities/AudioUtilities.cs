using UnityEngine;

namespace ChukStuph.Audio
{
    public class AudioUtilities
    {
        /// <summary>
        /// Converts a linear 0-1 value to decibels for use in AudioMixer.
        /// </summary>
        /// <param name="linear">The linear volume value (0-1) to convert</param>
        /// <returns>
        /// Scaled decibel value.
        /// <para>0 = -80 dB (silence)</para>
        /// <para>1 = 0 dB (maximum volume)</para>
        /// </returns>
        public static float LinearToDecibels(float linear)
        {
            linear = Mathf.Clamp01(linear);
            if (linear <= 0f) return -80f;
            return 20f * Mathf.Log10(linear);
        }

        /// <summary>
        /// Converts a decibel value back to a linear 0-1 value.
        /// </summary>
        /// <param name="dB">The decibel value to convert</param>
        /// <returns>Linear 0-1 value</returns>
        public static float DecibelsToLinear(float dB)
        {
            return Mathf.Pow(10f, dB / 20f);
        }
    }
}
