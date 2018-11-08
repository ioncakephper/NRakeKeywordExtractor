namespace WindowsFormsApp1
{
    internal class KeyPhraseSummary
    {

        public KeyPhraseSummary(string keyPhrase)
        {
            Text = keyPhrase;
        }

        public int DocumentCount { get; internal set; }
        public double Score { get; internal set; }
        public string Text { get; internal set; }
        public int WordCount { get; internal set; }
    }
}