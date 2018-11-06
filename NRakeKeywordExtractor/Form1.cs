//------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="Ion Gireada">
//    Copyright (c) 2018 Ion Gireada
// </copyright>
//------------------------------------------------------------------------------------

namespace NRakeKeywordExtractor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="Form1" />
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Gets the Topics
        /// </summary>
        public List<Topic> Topics { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Topics = new List<Topic>();
        }

        /// <summary>
        /// The ExtractFilesListKeywords
        /// </summary>
        private void ExtractFilesListKeywords()
        {
            var tke = new TopicKeywordExtractor(Topics);
            var keyPhrases = tke.FindKeyPhrases();
            UpdateKeyPhrasesList(keyPhrases);
        }

        /// <summary>
        /// The ExtractTextPageKeywords
        /// </summary>
        private void ExtractTextPageKeywords()
        {
            string text = richTextBox1.Text;
            var kwe = new NRakeCore.KeywordExtractor(new NRakeCore.StopWordFilters.EnglishSmartStopWordFilter());
            var keyPhrases = kwe.FindKeyPhrases(text);
            UpdateKeyPhrasesList(keyPhrases);
        }

        /// <summary>
        /// The UpdateKeyPhrasesList
        /// </summary>
        /// <param name="keyPhrases">The keyPhrases<see cref="string[]"/></param>
        private void UpdateKeyPhrasesList(string[] keyPhrases)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(keyPhrases);
            label1.Text = (!keyPhrases.Length.Equals(0)) ? string.Format(@"{0} key phrases found.", keyPhrases.Length) : "No key phrases found.";
        }

        /// <summary>
        /// The button1_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void button1_Click(object sender, EventArgs e)
        {
            switch(tabControl1.SelectedIndex)
            {
                case 1:
                    ExtractFilesListKeywords();
                    break;
                default:
                    ExtractTextPageKeywords();
                    break;
            }
        }

        /// <summary>
        /// The Form1_Load
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Text = string.Empty;
        }

        /// <summary>
        /// The button2_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                var fileNames = openFileDialog1.FileNames;
                AddTopicFiles(fileNames);
            }
        }

        /// <summary>
        /// The AddTopicFiles
        /// </summary>
        /// <param name="fileNames">The fileNames<see cref="string[]"/></param>
        private void AddTopicFiles(string[] fileNames)
        {
            AddTopicFiles(ToTopics(fileNames));
        }

        /// <summary>
        /// The AddTopicFiles
        /// </summary>
        /// <param name="topic">The topic<see cref="Topic[]"/></param>
        private void AddTopicFiles(Topic[] topic)
        {
            Topics.AddRange(topic);
            PopulateTopicsListView(Topics);
        }

        /// <summary>
        /// The PopulateTopicsListView
        /// </summary>
        /// <param name="topics">The topics<see cref="List{Topic}"/></param>
        private void PopulateTopicsListView(List<Topic> topics)
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(GetTopicsListViewItems(topics));
        }

        /// <summary>
        /// The GetTopicsListViewItems
        /// </summary>
        /// <param name="topics">The topics<see cref="List{Topic}"/></param>
        /// <returns>The <see cref="ListViewItem[]"/></returns>
        private ListViewItem[] GetTopicsListViewItems(List<Topic> topics)
        {
            List<ListViewItem> items = new List<ListViewItem>();
            topics.ForEach(topic =>
            {
                items.Add(GetSingleTopicListViewItem(topic));
            });
            return items.ToArray();
        }

        /// <summary>
        /// The GetSingleTopicListViewItem
        /// </summary>
        /// <param name="topic">The topic<see cref="Topic"/></param>
        /// <returns>The <see cref="ListViewItem"/></returns>
        private ListViewItem GetSingleTopicListViewItem(Topic topic)
        {
            var item = new ListViewItem();
            item.Tag = topic;

            item.Text = topic.Title;
            item.SubItems.AddRange(new string[] { topic.FileName, topic.Folder });

            return item;
        }

        /// <summary>
        /// The ToTopics
        /// </summary>
        /// <param name="fileNames">The fileNames<see cref="string[]"/></param>
        /// <returns>The <see cref="Topic[]"/></returns>
        private Topic[] ToTopics(string[] fileNames)
        {
            var topics = new List<Topic>();
            foreach (var fileName in fileNames)
            {
                topics.Add(new Topic(fileName));
            }
            return topics.ToArray();
        }
    }
}
