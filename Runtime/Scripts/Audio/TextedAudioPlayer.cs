using UnityEngine;
using ChukStuph.GameSettings;

namespace ChukStuph.Audio
{
    /// <summary>
    /// Handles playback of TextedAudio
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class TextedAudioPlayer : MonoBehaviour
    {
        public TextedAudioSO currentSong;
        [HideInInspector] public AudioSource audioSource;

        [SerializeField] private BoolSettingValueSO showTextBool;

        private TimedTextManager textManager;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            textManager = new(showTextBool, currentSong?.timestampedTextList);
        }

        private void Update()
        {
            if (textManager != null && audioSource.clip != null && audioSource.isPlaying)
            {
                textManager.UpdateTime(audioSource.time);
            }
        }

        public void Play(TextedAudioSO newSong)
        {
            if (newSong == null) return;

            currentSong = newSong;

            audioSource.clip = newSong.clip;
            audioSource.Play();

            textManager.SetNewTimedText(newSong.timestampedTextList);
        }

        public void RegisterTextListener(ITimedTextListener listener)
        {
            textManager?.RegisterListener(listener);
        }

        public void UnregisterTextListener(ITimedTextListener listener)
        {
            textManager?.UnregisterListener(listener);
        }

        public void Stop()
        {
            audioSource.Stop();
            textManager?.Stop();
        }

        private void OnDestroy()
        {
            textManager?.Dispose();
        }
    }
}