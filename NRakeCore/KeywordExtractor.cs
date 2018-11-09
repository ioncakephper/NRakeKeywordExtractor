using NRakeCore.StopWordFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NRakeCore
{
    public class KeywordExtractor
    {
        readonly IStopWordFilter _stopWords;

        readonly Regex _reSplit = new Regex(@"\s|([_,.;:!?\(\)\[\]\{\}\/\|\\\*\#\%\^\&\-\=\+])"); //This split captures punctuation, but discards spaces.

        SortedSet<string> _uniqueWords = null;

        public SortedSet<string> UniqueWordIndex
        {
            get
            {
                return _uniqueWords;
            }
        }

        public IStopWordFilter StopWordFilter
        {
            get
            {
                return this._stopWords;
            }
        }

        public SortedList<string, WordScore> LeagueTable { get; set; }
        public SortedList<string, double> AggregatedLeagueTable { get; private set; }

        public KeywordExtractor()
        {
            _stopWords = new BasicStopWordFilter();
            LeagueTable = new SortedList<string, WordScore>();
            AggregatedLeagueTable = new SortedList<string, double>();
        }

        public KeywordExtractor(IStopWordFilter filter)
        {
            _stopWords = filter;
        }

        /// <summary>
        /// Returns a KeywordExtractor intialized with the best stop-word filter for a given language/culture string.
        /// The default filter is EnglishSmartStopWordFilter.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static KeywordExtractor GetBestInstanceForCulture(string culture)
        {
            IStopWordFilter filter = new EnglishSmartStopWordFilter();
            if (!string.IsNullOrEmpty(culture))
            {
                culture = culture.Trim().ToLower();
                if (culture.Length > 2)
                {
                    culture = culture.Substring(0, 2);
                }

                if (culture.Length == 2)
                {
                    switch (culture)
                    {
                        case "en":
                            break; //intentionally doing nothing

                        case "fr":
                            filter = new FrenchStopWordFilter();
                            break;

                        default:
                            break; //intentionally doing nothing
                    }
                }
                else
                {
                    //Not a valid culture/language code... ignore.
                }
            }

            return new KeywordExtractor(filter);
        }

        public string[] FindKeyPhrases(string inputText)
        {
            string[] tokens = Tokenize(inputText);
            string[] phrases = ToPhrases(tokens);
            WordCooccurrenceMatrix matrix = new WordCooccurrenceMatrix(this.UniqueWordIndex);
            matrix.CompileOccurrences(phrases);
            SortedList<string, WordScore> leagueTable = matrix.LeagueTable;
            LeagueTable = leagueTable;
            SortedList<string, double> aggregatedLeagueTable = WordCooccurrenceMatrix.AggregateLeagueTable(leagueTable, phrases);
            AggregatedLeagueTable = aggregatedLeagueTable;

            int count = (int)Math.Ceiling((double)phrases.Length / (double)3); //Take the top 1/3 of the key phrases

            // TODO: make scoring system configurable (for now, just use the Ratio).
            return aggregatedLeagueTable.OrderByDescending(x => x.Value).Take(count).Select(x => x.Key).ToArray();
        }

        public string[] Tokenize(string inputText)
        {
            List<string> tokens = new List<string>();
            foreach (string s in _reSplit.Split(inputText))
            {
                if (!string.IsNullOrEmpty(s))
                {
                    tokens.Add(s.Trim().ToLower());
                }
            }

            return tokens.ToArray();
        }

        /// <summary>
        /// Note: this method has side-effects. In addition to returning the array of phrases, it maintains the internal index of unique words.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public string[] ToPhrases(string[] tokens)
        {
            _uniqueWords = new SortedSet<string>();
            List<string> phrases = new List<string>();
            string current = string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (string t in tokens)
            {
                if (_stopWords.IsPunctuation(t) || _stopWords.IsStopWord(t))
                {
                    //Throw it away!
                    if (sb.Length > 0)
                    {
                        current = sb.ToString();
                        phrases.Add(current);
                        // current = string.Empty;
                        sb.Clear();
                    }
                }
                else
                {
                    _uniqueWords.Add(t);
                    if (sb.Length == 0)
                    {
                        sb.Append(t);

                    }
                    else
                    {
                        sb.Append(" " + t);

                    }
                }
            }

            
            if (sb.Length > 0)
            {
                current = sb.ToString();
                phrases.Add(current);
            }

            return phrases.ToArray();
        }

        public List<Tuple<string, float>> ScorePhrases(string[] phrases)
        {
            if (_uniqueWords == null)
            {
                throw new ApplicationException("You must call ToPhrases(string[]) before calling ScorePhrases(string[]).");
            }

            return new List<Tuple<string, float>>();

        }
    }
}
