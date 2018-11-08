using NRakeKeywordExtractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public List<Topic> Topics { get; private set; }

        public Form1()
        {
            InitializeComponent();
            Topics = new List<Topic>();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddTopicFiles();
        }

        private void AddTopicFiles()
        {
            string[] fileNames = GetTopicFileNames();
            AddTopics(fileNames);
        }

        private string[] GetTopicFileNames()
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = @"All files(*.*)|*.*|HTML files(*.htm;*.html)|*.htm;*.html";
            openFileDialog1.FilterIndex = 2;

            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                return openFileDialog1.FileNames;
            }
            return new string[] { };
        }

        private void AddTopics(string[] fileNames)
        {
            Topic[] topics = fileNames.Select(path => new Topic(path)).ToArray();
            AddTopics(topics);
        }

        private void AddTopics(Topic[] topics)
        {
            Topics.AddRange(topics);
            PopulateTopicFilesListView(Topics);
        }

        private void PopulateTopicFilesListView(List<Topic> topics)
        {
            PopulateTopicFilesListView(topics, string.Empty);
        }

        private void PopulateTopicFilesListView(List<Topic> topics, string filter)
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(GetTopicsListViewItems(topics, filter));
        }

        private ListViewItem[] GetTopicsListViewItems(List<Topic> topics, string filter)
        {
            return topics.Select(topic => GetSingleTopicListViewItem(topic)).Where(item => {
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
            }).ToArray();
        }

        private ListViewItem GetSingleTopicListViewItem(Topic topic)
        {
            var item = new ListViewItem();
            item.Tag = topic;

            item.Text = topic.Title;
            item.SubItems.AddRange(new string[] { topic.FileName, topic.Folder });

            return item;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExtractKeywords();
        }

        private void ExtractKeywords()
        {
            var tke = new TopicKeywordExtractor(Topics);
            var keyPhrases = tke.FindKeyPhrases();
            PopulateKeyPhrasesListBox(keyPhrases);
            tabControl1.SelectedIndex = 1;
        }

        private void PopulateKeyPhrasesListBox(string[] keyPhrases)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(keyPhrases);
        }

        private void toolStripSplitButton1_ButtonDoubleClick(object sender, EventArgs e)
        {
            
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                var topic = (Topic)item.Tag;
                var index = Topics.IndexOf(topic);
                Topics.RemoveAt(index);
            }
            PopulateTopicFilesListView(Topics);
        }

        private void removeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Topics.Clear();
            PopulateTopicFilesListView(Topics);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTopicFiles();
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractKeywords();
        }
    }
}
