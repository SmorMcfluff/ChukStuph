using System.Collections;
using UnityEngine;

namespace ChukStuph.Audio
{
    /// <summary>
    /// Handles having multiple songs or other audio tracks over a single drumbeat
    /// </summary>
    public class MultitrackLoopHandler : MonoBehaviour
    {
        public static MultitrackLoopHandler Instance { get; private set; }

        [SerializeField] private AudioSource drumSrc;
        [SerializeField] private TextedAudioPlayer songPlayer;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            if (drumSrc != null && songPlayer != null) return;
        }

        private void Start()
        {
            drumSrc.Play();
            StartCoroutine(PlaySongWhenDrumReady());
        }

        private IEnumerator PlaySongWhenDrumReady()
        {
            while (!drumSrc.isPlaying)
                yield return null;

            yield return null;

            if (songPlayer != null && songPlayer.currentSong != null)
            {
                songPlayer.Play(songPlayer.currentSong);
                songPlayer.audioSource.time = drumSrc.time;
            }
        }

        /// <summary>
        /// Change the drum loop, optionally maintaining playtime.
        /// </summary>
        /// <param name="newLoop">The new loop clip</param>
        /// <param name="maintainPlaytime">Whether or not to keep the current play time.</param>
        /// <param name="stopSong">Whether or not to stop playing the song track.</param>
        public void ChangeDrumLoop(AudioClip newLoop, bool maintainPlaytime = false, bool stopSong = true)
        {
            drumSrc.clip = newLoop;
            float startTime = maintainPlaytime ? drumSrc.time : 0;

            drumSrc.Play();
            drumSrc.time = startTime;

            if (stopSong)
            {
                songPlayer.Stop();
            }
        }

        /// <summary>
        /// Change the track playing over the basic drum loop to a new song with lyrics
        /// </summary>
        /// <param name="newSong">The new song clip</param>
        public void ChangeSongClip(TextedAudioSO newSong)
        {
            if (songPlayer == null || newSong == null || drumSrc == null) return;
            songPlayer.Play(newSong);
            songPlayer.audioSource.time = drumSrc.time;
        }
    }
}