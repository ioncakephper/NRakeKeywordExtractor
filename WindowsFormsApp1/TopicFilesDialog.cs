using NRakeCore;
using NRakeKeywordExtractor;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TopicFilesDialog : Form
    {
        public TopicFilesDialog()
        {
            InitializeComponent();
            Topics = new List<Topic>();
        }

        public TopicFilesDialog(List<Topic> topics) : this()
        {
            Topics = topics;
        }

        /// <summary>
        /// The list of topics
        /// </summary>
        public List<Topic> Topics
        {
            get; set;
        }

        private void addFilesButton_Click(object sender, System.EventArgs e)
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
            Topics.AddRange(items);
            PopulateTopicsListView(Topics);
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
            item.Checked = topic.Checked;

            return item;
        }
        private Topic[] GetTopicsFromFilename(string[] fileNames)
        {
            return fileNames.Select(fileName => GetTopicFrom(fileName)).ToArray();
        }

        private Topic GetTopicFrom(string fileName)
        {
            return GetTopicFrom(fileName, true);
        }

        private Topic GetTopicFrom(string fileName, bool isChecked)
        {
            var kwe = new KeywordExtractor(new NRakeCore.StopWordFilters.EnglishSmartStopWordFilter());
            var t = new Topic(fileName);
            t.Words = kwe.Tokenize(t.GetText()).Length;
            t.Checked = isChecked;

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

        private void TopicFilesDialog_Load(object sender, System.EventArgs e)
        {
            PopulateTopicsListView(Topics);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var checkStatus = e.Item.Checked;
            var topicIndex = Topics.IndexOf((Topic)e.Item.Tag);
            Topics[topicIndex].Checked = checkStatus;
        }
    }
}
