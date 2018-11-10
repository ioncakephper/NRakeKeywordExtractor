//-----------------------------------------------------------------------
// <copyright file="Form1.cs" company="Ion Gireada">
//     Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace WindowsFormsApp1
{
    using NRakeKeywordExtractor;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        ///
        /// </summary>
        private List<Topic> topics;

        /// <summary>
        ///
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            topics = new List<Topic>();
        }

        /// <summary>
        ///
        /// </summary>
        public enum TopicKeywordExtractionOptions
        {
            All,
            Checked,
            Unchecked
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="listViewItem"></param>
        /// <returns></returns>
        private ListViewItem[] ApplyFilter(string filter, ListViewItem[] listViewItem)
        {
            return listViewItem.Where(item => MatchFilter(filter, item)).ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool MatchFilter(string filter, ListViewItem item)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return true;
            }

            if (item.Text.Contains(filter))
            {
                return true;
            }

            foreach (var subItem in item.SubItems)
            {
                if (subItem.ToString().Contains(filter))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        private void ExtractFilesListKeywords()
        {
            ExtractFilesListKeywords(TopicKeywordExtractionOptions.Checked);
        }

        /// <summary>
        /// The ExtractFilesListKeywords.
        /// </summary>
        private void ExtractFilesListKeywords(TopicKeywordExtractionOptions status)
        {
            List<Topic> selectedTopics;
            switch (status)
            {
                case TopicKeywordExtractionOptions.Checked:
                    selectedTopics = topics.Where(topic => topic.Checked.Equals(true)).ToList();
                    break;
                case TopicKeywordExtractionOptions.Unchecked:
                    selectedTopics = topics.Where(topic => topic.Checked.Equals(false)).ToList();
                    break;
                default:
                    selectedTopics = topics;
                    break;
            }

            ExtractFilesListKeywords(selectedTopics);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="topics"></param>
        private void ExtractFilesListKeywords(List<Topic> topics)
        {
            var tke = new TopicKeywordExtractor(topics);
            var keyPhrases = tke.FindKeyPhrases();

            var keyPhraseSummaries = new List<KeyPhraseSummary>();
            foreach (var keyPhrase in keyPhrases)
            {
                var item = GetKeyPhraseSummary(keyPhrase, tke);
                keyPhraseSummaries.Add(item);
            }

            UpdateKeyPhrasesList(keyPhraseSummaries);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyPhraseSummaries"></param>
        private void UpdateKeyPhrasesList(List<KeyPhraseSummary> keyPhraseSummaries)
        {
            listView2.Items.Clear();
            var items = GetKeyPhrasesListViewItems(keyPhraseSummaries, string.Empty);
            listView2.Items.AddRange(items);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyPhraseSummaries"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private ListViewItem[] GetKeyPhrasesListViewItems(List<KeyPhraseSummary> keyPhraseSummaries, string filter)
        {
            return ApplyFilter(filter, GetKeyPhrasesListViewItem(keyPhraseSummaries));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyPhraseSummaries"></param>
        /// <returns></returns>
        private ListViewItem[] GetKeyPhrasesListViewItem(List<KeyPhraseSummary> keyPhraseSummaries)
        {
            return keyPhraseSummaries.Select(keyPhrase => GetSingleKeyphraseListItem(keyPhrase)).ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyPhraseSummary"></param>
        /// <returns></returns>
        private ListViewItem GetSingleKeyphraseListItem(KeyPhraseSummary keyPhraseSummary)
        {
            var item = new ListViewItem();
            item.Tag = keyPhraseSummary;
            item.Text = keyPhraseSummary.Text;
            item.SubItems.AddRange(new string[] { keyPhraseSummary.DocumentCount.ToString(), keyPhraseSummary.Score.ToString(), keyPhraseSummary.WordCount.ToString(), keyPhraseSummary.Rank.ToString() });

            return item;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyPhrase"></param>
        /// <param name="tke"></param>
        /// <returns></returns>
        private KeyPhraseSummary GetKeyPhraseSummary(string keyPhrase, TopicKeywordExtractor tke)
        {
            var item = new KeyPhraseSummary(keyPhrase);
            item.WordCount = keyPhrase.Split(' ').Length;
            item.DocumentCount = tke.KeyPhraseTopics[keyPhrase].Count;
            item.Score = (tke.PhraseScore.ContainsKey(keyPhrase)) ? tke.PhraseScore[keyPhrase] : 0;
            item.Rank = (tke.PhraseRank.ContainsKey(keyPhrase)) ? tke.PhraseRank[keyPhrase] : 0;

            return item;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExtractOnChosenTopicFiles();
        }

        /// <summary>
        ///
        /// </summary>
        private void ExtractOnChosenTopicFiles()
        {
            var d = new TopicFilesDialog(topics);
            if (d.ShowDialog().Equals(DialogResult.OK))
            {
                topics = d.Topics;
                ExtractFilesListKeywords();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var appOptions = new ApplicationOptions();
            var optionsDialog = new OptionsDialog(appOptions.Options);

            if (optionsDialog.ShowDialog().Equals(DialogResult.OK))
            {
                appOptions.Update(optionsDialog.Options);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var appCustomization = new ApplicationCustomization();

            var d = new CustomizationDialog(appCustomization.Options);
            if (d.ShowDialog().Equals(DialogResult.OK))
            {
                appCustomization.Update(d.Options);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ExportPhrasesAndKeywords();
        }

        /// <summary>
        ///
        /// </summary>
        private void ExportPhrasesAndKeywords()
        {
            MessageBox.Show("Exporting phrases and keywords...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Remove this method
            ExportPhrasesAndKeywords();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void extractToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ExtractOnChosenTopicFiles();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExportPhrasesAndKeywords();
        }
    }
}
