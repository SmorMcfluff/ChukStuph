namespace ChukStuph.Audio
{
    [System.Serializable]
    public struct TimedText
    {
        public float timestamp;
        [UnityEngine.Serialization.FormerlySerializedAs("lyric")] public string text;
    }
}