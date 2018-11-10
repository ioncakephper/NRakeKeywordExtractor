//-----------------------------------------------------------------------
// <copyright file="TopicFilesDialog.cs" company="Ion Gireada">
//     Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace WindowsFormsApp1
{
    #region Imports (6)

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using NRakeCore;
    using NRakeKeywordExtractor;

    #endregion Imports (6)

    /// <summary>
    ///
    /// </summary>
    public partial class TopicFilesDialog : Form
    {
        #region Properties of TopicFilesDialog (1)

        /// <summary>
        /// The list of topics.
        /// </summary>
        public List<Topic> Topics
        {
            get;
            set;
        }

        #endregion Properties of TopicFilesDialog (1)

        #region Constructors of TopicFilesDialog (2)

        /// <summary>
        ///
        /// </summary>
        public TopicFilesDialog()
        {
            InitializeComponent();
            Topics = new List<Topic>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="topics"></param>
        public TopicFilesDialog(List<Topic> topics) : this()
        {
            Topics = topics;
        }

        #endregion Constructors of TopicFilesDialog (2)

        #region Methods of TopicFilesDialog (26)

        /// <summary>
        ///
        /// </summary>
        private void AddFiles()
        {
            AddTopics(GetFileNames());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFilesButton_Click(object sender, System.EventArgs e)
        {
            AddFiles();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileNames"></param>
        private void AddTopics(string[] fileNames)
        {
            var items = GetTopicsFromFilename(fileNames);
            AddTopics(items);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        private void AddTopics(Topic[] items)
        {
            Topics.AddRange(items);
            PopulateTopicsListView(Topics);
            SelectTopicItems(items);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                foreach (int index in listView1.SelectedIndices)
                {
                    RemoveItemAt(index, Topics, listView1.Items);
                }

                PopulateTopicsListView(Topics);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="buttons"></param>
        private void DisableButtons(bool condition, Button[] buttons)
        {
            if (condition)
            {
                SetButtonEnable(false, buttons);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="buttons"></param>
        private void EnableButtons(bool condition, Button[] buttons)
        {
            if (condition)
            {
                SetButtonEnable(true, buttons);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        private void EnableControls(bool condition)
        {
            var buttons = new Button[] { okButton, button1, button2, button3, button4, button5 };
            DisableButtons(condition, buttons);
            EnableButtons(!condition, buttons);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isSelectedIndicesCountZero"></param>
        private void EnableControlsOnSelectedChanged(bool isSelectedIndicesCountZero)
        {
            var buttons = new Button[] { button2 };
            DisableButtons(isSelectedIndicesCountZero, buttons);
            EnableButtons(!isSelectedIndicesCountZero, buttons);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private string[] GetFileNames()
        {
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                return openFileDialog1.FileNames;
            }

            return new string[] { };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        private ListViewItem GetSingleTopicListViewItem(Topic topic)
        {
            var item = new ListViewItem();
            item.Tag = topic;
            item.Text = topic.Title;
            item.SubItems.AddRange(new string[] { topic.FileName, topic.Folder, topic.Words.ToString() });
            item.Checked = topic.Checked;

            return item;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Topic GetTopicFrom(string fileName)
        {
            return GetTopicFrom(fileName, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="isChecked"></param>
        /// <returns></returns>
        private Topic GetTopicFrom(string fileName, bool isChecked)
        {
            var kwe = new KeywordExtractor(new NRakeCore.StopWordFilters.EnglishSmartStopWordFilter());
            var t = new Topic(fileName);
            t.Words = kwe.Tokenize(t.GetText()).Length;
            t.Checked = isChecked;

            return t;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="topics"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private ListViewItem[] GetTopicListViewItems(List<Topic> topics, string filter)
        {
            return ApplyFilter(filter, GetTopicListViewItems(topics));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="topics"></param>
        /// <returns></returns>
        private ListViewItem[] GetTopicListViewItems(List<Topic> topics)
        {
            return topics.Select(topic => GetSingleTopicListViewItem(topic)).ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        private Topic[] GetTopicsFromFilename(string[] fileNames)
        {
            return fileNames.Select(fileName => GetTopicFrom(fileName)).ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var checkStatus = e.Item.Checked;
            var topicIndex = Topics.IndexOf((Topic)e.Item.Tag);
            Topics[topicIndex].Checked = checkStatus;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableControlsOnSelectedChanged(listView1.SelectedIndices.Count.Equals(0));
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
        /// <param name="topics"></param>
        private void PopulateTopicsListView(List<Topic> topics)
        {
            PopulateTopicsListView(topics, string.Empty);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="topics"></param>
        /// <param name="filter"></param>
        private void PopulateTopicsListView(List<Topic> topics, string filter)
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(GetTopicListViewItems(topics, filter));
            EnableControls(listView1.Items.Count.Equals(0));
            EnableControlsOnSelectedChanged(listView1.SelectedIndices.Count.Equals(0));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="topics"></param>
        /// <param name="items"></param>
        private void RemoveItemAt(int index, List<Topic> topics, ListView.ListViewItemCollection items)
        {
            var topicIndex = Topics.IndexOf((Topic)items[index].Tag);
            topics.RemoveAt(topicIndex);
        }

        /// <summary>
        /// Select list items for specified items.
        /// </summary>
        /// <param name="topics">items to select in list view.</param>
        private void SelectTopicItems(Topic[] topics)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                Topic t = (Topic)item.Tag;
                item.Selected = topics.ToList().Contains(t);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="status"></param>
        /// <param name="buttons"></param>
        private void SetButtonEnable(bool status, Button[] buttons)
        {
            buttons.ToList().ForEach(b => b.Enabled = status);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopicFilesDialog_Load(object sender, System.EventArgs e)
        {
            PopulateTopicsListView(Topics);
        }

        #endregion Methods of TopicFilesDialog (26)
    }
}
