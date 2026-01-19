namespace ChukStuph.Audio
{
    public interface ITimedTextListener
    {
        /// <summary>
        /// Called when the text should update
        /// </summary>
        /// <param name="newText">The current text. Null if cleared.</param>
        void OnTextChanged(string newText);
    }
}