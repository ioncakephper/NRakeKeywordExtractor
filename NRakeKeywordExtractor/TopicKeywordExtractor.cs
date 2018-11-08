namespace NRakeKeywordExtractor
{
    using NRakeCore;
    using System.Collections.Generic;
    using System.Linq;
    /// <summary>
    /// Defines the <see cref="TopicKeywordExtractor" />
    /// </summary>
    public class TopicKeywordExtractor
    {
        #region Properties of TopicKeywordExtractor (4)

        /// <summary>
        /// Gets or sets the Items
        /// </summary>
        public List<Topic> Items { get; set; }

        /// <summary>
        /// Gets the KeyPhraseTopics
        /// </summary>
        public SortedList<string, List<Topic>> KeyPhraseTopics { get; private set; }

        /// <summary>
        /// Gets the KeywordExtractor
        /// </summary>
        public KeywordExtractor KeywordExtractor { get; private set; }

        /// <summary>
        /// Gets the TopicKeyPhrases
        /// </summary>
        public Dictionary<Topic, List<string>> TopicKeyPhrases { get; private set; }

        #endregion Properties of TopicKeywordExtractor (4)

        #region Constructors of TopicKeywordExtractor (8)

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicKeywordExtractor"/> class.
        /// </summary>
        public TopicKeywordExtractor()
        {
            KeywordExtractor = new KeywordExtractor(new NRakeCore.StopWordFilters.EnglishSmartStopWordFilter());
            Items = new List<Topic>();
            TopicKeyPhrases = new Dictionary<Topic, List<string>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topic">The topic<see cref="Topic"/></param>
        public TopicKeywordExtractor(Topic topic) : this()
        {
            Items.Add(topic);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topic">The topic<see cref="Topic"/></param>
        /// <param name="filter">The filter<see cref="NRakeCore.StopWordFilters.IStopWordFilter"/></param>
        public TopicKeywordExtractor(Topic topic, NRakeCore.StopWordFilters.IStopWordFilter filter) : this(topic)
        {
            KeywordExtractor = new NRakeCore.KeywordExtractor(filter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topics">The topics<see cref="Topic[]"/></param>
        public TopicKeywordExtractor(Topic[] topics) : this()
        {
            Items.AddRange(topics);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topics">The topics<see cref="Topic[]"/></param>
        /// <param name="filter">The filter<see cref="NRakeCore.StopWordFilters.IStopWordFilter"/></param>
        public TopicKeywordExtractor(Topic[] topics, NRakeCore.StopWordFilters.IStopWordFilter filter) : this(topics)
        {
            KeywordExtractor = new NRakeCore.KeywordExtractor(filter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topics">The topics<see cref="List{Topic}"/></param>
        public TopicKeywordExtractor(List<Topic> topics) : this(topics.ToArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topics">The topics<see cref="List{Topic}"/></param>
        /// <param name="filter">The filter<see cref="NRakeCore.StopWordFilters.IStopWordFilter"/></param>
        public TopicKeywordExtractor(List<Topic> topics, NRakeCore.StopWordFilters.IStopWordFilter filter) : this(topics)
        {
            KeywordExtractor = new NRakeCore.KeywordExtractor(filter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicKeywordExtractor"/> class.
        /// </summary>
        /// <param name="filter">The filter<see cref="NRakeCore.StopWordFilters.IStopWordFilter"/></param>
        public TopicKeywordExtractor(NRakeCore.StopWordFilters.IStopWordFilter filter) : this()
        {
            KeywordExtractor = new NRakeCore.KeywordExtractor(filter);
        }

        #endregion Constructors of TopicKeywordExtractor (8)

        #region Methods of TopicKeywordExtractor (4)

        /// <summary>
        /// The FindKeyPhrases
        /// </summary>
        /// <returns>The <see cref="string[]"/></returns>
        public string[] FindKeyPhrases()
        {
            return FindKeyPhrases(Items);
        }

        /// <summary>
        /// The FindKeyPhrases
        /// </summary>
        /// <param name="items">The items<see cref="List{Topic}"/></param>
        /// <returns>The <see cref="string[]"/></returns>
        public string[] FindKeyPhrases(List<Topic> items)
        {
            return FindKeyPhrases(items.ToArray());
        }

        /// <summary>
        /// The FindKeyPhrases
        /// </summary>
        /// <param name="topics">The topics<see cref="Topic[]"/></param>
        /// <returns>The <see cref="string[]"/></returns>
        public string[] FindKeyPhrases(Topic[] topics)
        {
            TopicKeyPhrases.Clear();
            foreach (var topic in topics)
            {
                var keyPhrases = KeywordExtractor.FindKeyPhrases(topic.GetText());
                TopicKeyPhrases.Add(topic, keyPhrases.ToList());
            }
            KeyPhraseTopics = ToKeyPhraseTopics(TopicKeyPhrases);
            return KeyPhraseTopics.Keys.ToArray();
        }

        /// <summary>
        /// The ToKeyPhraseTopics
        /// </summary>
        /// <param name="topicKeyPhrases">The topicKeyPhrases<see cref="Dictionary{Topic, List{string}}"/></param>
        /// <returns>The <see cref="SortedList{string, List{Topic}}"/></returns>
        private SortedList<string, List<Topic>> ToKeyPhraseTopics(Dictionary<Topic, List<string>> topicKeyPhrases)
        {
            var keyPhraseTopics = new SortedList<string, List<Topic>>();
            foreach (var topic in topicKeyPhrases.Keys)
            {
                foreach (var keyPhrase in topicKeyPhrases[topic])
                {
                    if (!keyPhraseTopics.ContainsKey(keyPhrase))
                    {
                        keyPhraseTopics.Add(keyPhrase, new List<Topic>());
                    }
                    keyPhraseTopics[keyPhrase].Add(topic);
                }
            }
            return keyPhraseTopics;
        }

        #endregion Methods of TopicKeywordExtractor (4)
    }
}
