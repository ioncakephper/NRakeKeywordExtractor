using NRakeCore;
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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Topic> topics;

        public Form1()
        {
            InitializeComponent();
            topics = new List<Topic>();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFiles();
        }

        private void AddFiles()
        {
            AddTopics(GetFileNames());
        }

        private void AddTopics(string[] fileNames)
        {
            var items = GetTopicsFromFilename(fileNames);
            AddTopics(items);
        }

        private void AddTopics(Topic[] items)
        {
            topics.AddRange(items);
            PopulateTopicsListView(topics);
        }

        private void PopulateTopicsListView(List<Topic> topics)
        {
            PopulateTopicsListView(topics, string.Empty);
        }

        private void PopulateTopicsListView(List<Topic> topics, string filter)
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(GetTopicListViewItems(topics, filter));

        }

        private ListViewItem[] GetTopicListViewItems(List<Topic> topics, string filter)
        {
            return ApplyFilter(filter, GetTopicListViewItems(topics));
        }

        private ListViewItem[] ApplyFilter(string filter, ListViewItem[] listViewItem)
        {
            return listViewItem;
        }

        private ListViewItem[] GetTopicListViewItems(List<Topic> topics)
        {
            return topics.Select(topic => GetSingleTopicListViewItem(topic)).ToArray();
        }

        private ListViewItem GetSingleTopicListViewItem(Topic topic)
        {
            var item = new ListViewItem();
            item.Tag = topic;
            item.Text = topic.Title;
            item.SubItems.AddRange(new string[] { topic.FileName, topic.Folder, topic.Words.ToString() });

            return item;
        }

        private Topic[] GetTopicsFromFilename(string[] fileNames)
        {
            return fileNames.Select(fileName => GetTopicFrom(fileName)).ToArray();
        }

        private Topic GetTopicFrom(string fileName)
        {
            var kwe = new KeywordExtractor(new NRakeCore.StopWordFilters.EnglishSmartStopWordFilter());
            var t = new Topic(fileName);
            t.Words = kwe.Tokenize(t.GetText()).Length;

            return t;
        }

        private string[] GetFileNames()
        {
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                return openFileDialog1.FileNames;
            }
            return new string[] { };
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddFiles();
        }
    }
}
