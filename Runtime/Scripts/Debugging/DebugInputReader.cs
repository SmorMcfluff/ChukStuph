using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ChukStuph.DebugUtils
{
    /// <summary>
    /// Reads debug key input and invokes paired UnityEvents.
    /// Only active in the Editor; self-destructs in builds.
    /// </summary>
    public class DebugInputReader : MonoBehaviour
    {
        [SerializeField] private DebugInput[] debugInputs;

        private readonly Dictionary<KeyCode, UnityEvent> pairedActions = new();

#if !UNITY_EDITOR
        private void OnEnable()
        {
            Destroy(this);
        }
#else
        private void Awake()
        {
            AssembleDictionary();
        }

        private void AssembleDictionary()
        {
            pairedActions.Clear();
            foreach (DebugInput inputPair in debugInputs)
            {
                bool hasError = false;
                if (inputPair.action == null)
                {
                    Debug.LogWarning($"Debug key {inputPair.key} has no inputAction", this);
                    hasError = true;
                }
                if (pairedActions.ContainsKey(inputPair.key))
                {
                    Debug.LogWarning($"Duplicate debug key {inputPair.key}", this);
                    hasError = true;

                }
                if (hasError) continue;
                pairedActions.Add(inputPair.key, inputPair.action);
            }
        }

        private void Update()
        {
            foreach (var pair in pairedActions)
            {
                if (UnityEngine.Input.GetKeyDown(pair.Key))
                {
                    pair.Value.Invoke();
                }
            }
        }
#endif
    }
}