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

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private List<Topic> topics;

        public Form1()
        {
            InitializeComponent();
            topics = new List<Topic>();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new AboutBox1();
            d.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var topNodes = new List<TreeNode>();
            topNodes.Add(new TreeNode("Keyword Extraction", new TreeNode[] { new TreeNode("General") }));
            topNodes.Add(new TreeNode("Output Generation", new TreeNode[] { new TreeNode("General") }));

            var d = new OptionsDialog(topNodes.ToArray());
            if (d.ShowDialog().Equals(DialogResult.OK))
            {

            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTopicFiles();
        }

        private void AddTopicFiles()
        {
            AddTopics(GetTopicFiles());
        }

        private void AddTopics(string[] topicFiles)
        {
            var topics = topicFiles.Select(fileName => new Topic(fileName)).ToArray();
            topics.ToList().ForEach(topic => topic.WordCount = GetTopicWordCount(topic));
            AddTopics(topics);
        }

        private int GetTopicWordCount(Topic topic)
        {
            var kwe = new KeywordExtractor(new NRakeCore.StopWordFilters.EnglishSmartStopWordFilter());
            var wordCount = kwe.Tokenize(topic.GetText()).Length;
            return wordCount;
        }

        private void AddTopics(Topic[] topic)
        {
            topics.AddRange(topic);
            PopulateTopicListView();
        }

        private void PopulateTopicListView()
        {
            PopulateTopicListView(topics);
        }

        private void PopulateTopicListView(List<Topic> topics)
        {
            PopulateTopicListView(topics, string.Empty);
        }

        private void PopulateTopicListView(List<Topic> topics, string filter)
        {
            listView1.Items.Clear();
            ListViewItem[] items = GetTopicListViewItems(topics, filter);
            listView1.Items.AddRange(items);
        }

        private ListViewItem[] GetTopicListViewItems(List<Topic> topics, string filter)
        {
            return ApplyFilter(filter, GetTopicListViewItems(topics));
        }

        private ListViewItem[] ApplyFilter(string filter, ListViewItem[] listViewItem)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return listViewItem;
            }
            return listViewItem.Where(item => MatchesFilter(item, filter)).ToArray();
        }

        private bool MatchesFilter(ListViewItem item, string filter)
        {
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

        private ListViewItem[] GetTopicListViewItems(List<Topic> topics)
        {
            return topics.Select(topic => GetSingleTopicListViewItem(topic)).ToArray();
        }

        private ListViewItem GetSingleTopicListViewItem(Topic topic)
        {
            var item = new ListViewItem(topic.Title);
            item.Tag = topic;
            item.SubItems.AddRange(new string[] { topic.FileName, topic.Folder, topic.WordCount.ToString() });

            return item;
        }

        private string[] GetTopicFiles()
        {
            openFileDialog1.FileName = string.Empty;
            return (openFileDialog1.ShowDialog().Equals(DialogResult.OK)) ? openFileDialog1.FileNames : new string[] { };
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddTopicFiles();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewItemComparer columnItemComparer = new ListViewItemComparer();
            var colHeader = listView1.Columns[e.Column].Text;
            switch (colHeader)
            {
                case "Word":
                    columnItemComparer.Mode = ListViewItemComparerModes.Number;
                    break;
                default:

                    break;
            }
            listView1.ListViewItemSorter = columnItemComparer;
        }
    }
}
