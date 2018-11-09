using NRakeKeywordExtractor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public enum TopicKeywordExtractionOptions
        {
            All,
            Checked,
            Unchecked
        }

        private List<Topic> topics;

        public Form1()
        {
            InitializeComponent();
            topics = new List<Topic>();
        }

        private ListViewItem[] ApplyFilter(string filter, ListViewItem[] listViewItem)
        {
            return listViewItem.Where(item => MatchFilter(filter, item)).ToArray();
        }

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

        private void ExtractFilesListKeywords()
        {
            ExtractFilesListKeywords(TopicKeywordExtractionOptions.Checked);
        }

        /// <summary>
        /// The ExtractFilesListKeywords
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

        private void UpdateKeyPhrasesList(List<KeyPhraseSummary> keyPhraseSummaries)
        {
            listView2.Items.Clear();
            var items = GetKeyPhrasesListViewItems(keyPhraseSummaries, string.Empty);
            listView2.Items.AddRange(items);
        }

        private ListViewItem[] GetKeyPhrasesListViewItems(List<KeyPhraseSummary> keyPhraseSummaries, string filter)
        {
            return ApplyFilter(filter, GetKeyPhrasesListViewItem(keyPhraseSummaries));
        }

        private ListViewItem[] GetKeyPhrasesListViewItem(List<KeyPhraseSummary> keyPhraseSummaries)
        {
            return keyPhraseSummaries.Select(keyPhrase => GetSingleKeyphraseListItem(keyPhrase)).ToArray();
        }

        private ListViewItem GetSingleKeyphraseListItem(KeyPhraseSummary keyPhraseSummary)
        {
            var item = new ListViewItem();
            item.Tag = keyPhraseSummary;
            item.Text = keyPhraseSummary.Text;
            item.SubItems.AddRange(new string[] { keyPhraseSummary.DocumentCount.ToString(), keyPhraseSummary.Score.ToString(), keyPhraseSummary.WordCount.ToString() });

            return item;
        }

        private KeyPhraseSummary GetKeyPhraseSummary(string keyPhrase, TopicKeywordExtractor tke)
        {
            var item = new KeyPhraseSummary(keyPhrase);
            item.WordCount = keyPhrase.Split(' ').Length;
            item.DocumentCount = tke.KeyPhraseTopics[keyPhrase].Count;
            item.Score = (tke.PhraseScore.ContainsKey(keyPhrase)) ? tke.PhraseScore[keyPhrase] : 0;

            return item;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExtractOnChosenTopicFiles();
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractOnChosenTopicFiles();
        }

        private void ExtractOnChosenTopicFiles()
        {
            var d = new TopicFilesDialog(topics);
            if (d.ShowDialog().Equals(DialogResult.OK))
            {
                topics = d.Topics;
                ExtractFilesListKeywords();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var appOptions = new ApplicationOptions();
            var optionsDialog = new OptionsDialog(appOptions.Options);

            if (optionsDialog.ShowDialog().Equals(DialogResult.OK))
            {
                appOptions.Update(optionsDialog.Options);
            }
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var appCustomization = new ApplicationCustomization();

            var d = new CustomizationDialog(appCustomization.Options);
            if (d.ShowDialog().Equals(DialogResult.OK))
            {
                appCustomization.Update(d.Options);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ExportPhrasesAndKeywords();
        }

        private void ExportPhrasesAndKeywords()
        {
            MessageBox.Show("Exporting phrases and keywords...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportPhrasesAndKeywords();
        }
    }
}
