using System.Collections.Generic;
using UnityEngine;

namespace ChukStuph.Audio
{
    [CreateAssetMenu(fileName = "new Texted Audio", menuName = "Chuk Essentials/Audio/Timestamped Texted")]
    public class TextedAudioSO : ScriptableObject
    {
        public AudioClip clip;
        public List<TimedText> timestampedTextList;
    }
}