using System.Collections.Generic;
using UnityEngine;

namespace ChukStuph.Audio
{
    [CreateAssetMenu(fileName = "New Texted Audio", menuName = "Chuk Essentials/Audio/Texted Audio")]
    public class TextedAudioSO : ScriptableObject
    {
        public AudioClip clip;
        public List<TimedText> timestampedTextList;
    }
}