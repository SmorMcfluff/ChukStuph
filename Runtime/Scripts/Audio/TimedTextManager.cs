using ChukStuph.GameSettings;
using System;
using System.Collections.Generic;

namespace ChukStuph.Audio
{
    /// <summary>
    /// Handles audioclips with timestamped text (lyrics, subtitles, etc.)
    /// </summary>
    /// <remarks>
    /// Does not display text on screen, but can be used to set text on a TextMeshPro or other similar thing
    /// </remarks>
    public class TimedTextManager : IDisposable
    {
        private BoolSettingValueSO showText;

        private readonly List<ITimedTextListener> listeners = new();

        private int textIndex = -1;
        private List<TimedText> currentTextList = new();
        private string lastText;
        public string CurrentText => lastText;

        public TimedTextManager(BoolSettingValueSO showTextBool, List<TimedText> newTimedTextList = null)
        {
            showText = showTextBool;
            currentTextList = newTimedTextList ?? new();

            if (showText != null)
                showText.OnChanged += OnShowTextSettingChanged;
        }

        private void OnShowTextSettingChanged(bool show)
        {
            if (!show) ClearText();
        }

        /// <summary>
        /// Change to a new set of timestamped text.
        /// </summary>
        /// <param name="newTimedTextList">The set of timestamped text to change to</param>
        public void SetNewTimedText(List<TimedText> newTimedTextList)
        {
            textIndex = -1;
            lastText = null;
            currentTextList = newTimedTextList;
            ClearText();
        }

        /// <summary>
        /// Check if it's time to change to a new text
        /// </summary>
        /// <param name="currentTime">The current playtime of the audio source</param>
        public void UpdateTime(float currentTime)
        {
            if (showText != null && !showText.Value)
            {
                if (lastText != null)
                    ClearText();
                return;
            }

            if (currentTextList == null || currentTextList.Count == 0) return;

            if (textIndex >= 0 && currentTime < currentTextList[textIndex].timestamp)
                textIndex = -1;

            while (textIndex < currentTextList.Count - 1 && currentTime >= currentTextList[textIndex + 1].timestamp)
            {
                textIndex++;
            }

            string currentText = textIndex >= 0 ? currentTextList[textIndex].text : null;

            if (currentText != lastText)
            {
                lastText = currentText;
                NotifyListeners(currentText);
            }
        }

        public void Stop()
        {
            textIndex = -1;
            currentTextList = null;

            ClearText();
        }

        private void ClearText()
        {
            if (lastText == null) return;

            lastText = null;
            NotifyListeners(null);
        }

        public void RegisterListener(ITimedTextListener listener)
        {
            if (listener != null && !listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(ITimedTextListener listener)
        {
            if (listener != null)
                listeners.Remove(listener);
        }

        private void NotifyListeners(string text)
        {
            foreach (var listener in listeners)
                listener.OnTextChanged(text);
        }

        public void Dispose()
        {
            if (showText != null)
                showText.OnChanged -= OnShowTextSettingChanged;
        }
    }
}