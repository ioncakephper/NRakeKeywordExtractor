//------------------------------------------------------------------------------
// <copyright file="KeyPhraseSummary.cs" company="Weblidity Software">
//      Copyright (c) Weblidity Software. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
namespace WindowsFormsApp1
{
    /// <summary>
    /// Defines the <see cref="KeyPhraseSummary" />
    /// </summary>
    internal class KeyPhraseSummary
    {
        #region Properties of KeyPhraseSummary (5)

        /// <summary>
        /// Gets or sets the DocumentCount
        /// </summary>
        public int DocumentCount { get; internal set; }

        /// <summary>
        /// Gets or sets the Rank
        /// </summary>
        public int Rank { get; internal set; }

        /// <summary>
        /// Gets or sets the Score
        /// </summary>
        public double Score { get; internal set; }

        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Gets or sets the WordCount
        /// </summary>
        public int WordCount { get; internal set; }

        #endregion Properties of KeyPhraseSummary (5)

        #region Constructors of KeyPhraseSummary (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPhraseSummary"/> class.
        /// </summary>
        /// <param name="keyPhrase"></param>
        public KeyPhraseSummary(string keyPhrase)
        {
            Text = keyPhrase;
        }

        #endregion Constructors of KeyPhraseSummary (1)
    }
}
