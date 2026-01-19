using UnityEngine;
using UnityEngine.Events;

namespace ChukStuph.DebugUtils
{
    /// <summary>
    /// A Unity event paired with a key input, to be read by the DebugInputReader
    /// </summary>
    [System.Serializable]
    public struct DebugInput
    {
        public KeyCode key;
        public UnityEvent action;

        public void Invoke()
        {
            action?.Invoke();
        }
    }
}