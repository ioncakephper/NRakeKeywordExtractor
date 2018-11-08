namespace NRakeKeywordExtractor
{
    using System;
    /// <summary>
    /// Defines the <see cref="BasicTopic" />
    /// </summary>
    public class BasicTopic
    {
        #region Properties of BasicTopic (4)

        /// <summary>
        /// Gets or sets the FileName
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the FilePath
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the Folder
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// Gets or sets the topic title
        /// </summary>
        public string Title { get; set; }

        #endregion Properties of BasicTopic (4)

        #region Constructors of BasicTopic (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicTopic"/> class.
        /// </summary>
        public BasicTopic()
        {
            FilePath = string.Empty;
            FileName = string.Empty;
            Folder = string.Empty;
            Title = string.Empty;
        }

        #endregion Constructors of BasicTopic (1)

        #region Methods of BasicTopic (3)

        /// <summary>
        /// Get topic text
        /// </summary>
        /// <returns>String of words</returns>
        public virtual string GetText()
        {
            return string.Empty;
        }

        /// <summary>
        /// Get topic title
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public virtual string GetTitle()
        {
            return GetTitle(FilePath);
        }

        /// <summary>
        /// Get topic title
        /// </summary>
        /// <param name="path">Path to topic full path</param>
        /// <returns>The <see cref="string"/></returns>
        public virtual string GetTitle(string path)
        {
            return String.Empty;
        }

        #endregion Methods of BasicTopic (3)
    }
}
