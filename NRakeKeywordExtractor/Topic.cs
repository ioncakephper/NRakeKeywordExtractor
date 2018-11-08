//------------------------------------------------------------------------------------
// <copyright file="Topic.cs" company="Ion Gireada">
//    Copyright (c) 2018 Ion Gireada
// </copyright>
//------------------------------------------------------------------------------------

namespace NRakeKeywordExtractor
{
    /// <summary>
    /// Defines the <see cref="Topic" />
    /// </summary>
    public class Topic : BasicTopic
    {
        public int Words { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Topic"/> class.
        /// </summary>
        /// <param name="path">File full path</param>
        public Topic(string path) : base()
        {
            FilePath = path;
            FileName = System.IO.Path.GetFileName(FilePath);
            Folder = System.IO.Path.GetDirectoryName(FilePath);
            Title = GetTitle();
        }

        /// <summary>
        /// The GetTitle
        /// </summary>
        /// <param name="path">The path<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public override string GetTitle(string path)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(FilePath);
            var node = doc.DocumentNode.SelectSingleNode("//head/title");
            return node.InnerText;
        }

        /// <summary>
        /// The GetText
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public override string GetText()
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(FilePath);
            var node = doc.DocumentNode.SelectSingleNode("//body");
            return node.InnerText;
        }
    }
}
