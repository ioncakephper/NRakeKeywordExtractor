//-----------------------------------------------------------------
// <copyright file="OptionTreeNodeItem.cs" company="Ion Gireada">
//      Copyright (c) 2018 Ion Gireada. All rights reserved.
// </copyright>
//-----------------------------------------------------------------
using System.Collections.Generic;

namespace WindowsFormsApplication2
{
    /// <summary>
    /// Defines the <see cref="OptionTreeNodeItem" /> class.
    /// </summary>
    public class OptionTreeNodeItem
    {
        #region Constructors of OptionTreeNodeItem (5)

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionTreeNodeItem"/> class.
        /// </summary>
        public OptionTreeNodeItem()
        {
            Text = string.Empty;
            Items = new OptionTreeNodeItem[] { };
            Page = new OptionPage();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionTreeNodeItem"/> class.
        /// </summary>
        /// <param name="optionPage">The optionPage<see cref="OptionPage"/>.</param>
        public OptionTreeNodeItem(OptionPage optionPage) : this(optionPage.Text, optionPage)
        {
            var generalOptionPage = optionPage;
            generalOptionPage.Text = "General";

            var itemList = new List<OptionTreeNodeItem>(Items);
            itemList.Add(new OptionTreeNodeItem(generalOptionPage));
            Items = itemList.ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionTreeNodeItem"/>. class.
        /// </summary>
        /// <param name="text">The text<see cref="string"/></param>
        public OptionTreeNodeItem(string text) : this()
        {
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionTreeNodeItem"/>. class.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="optionPage">The optionPage<see cref="OptionPage"/>.</param>
        public OptionTreeNodeItem(string title, OptionPage optionPage) : this(title)
        {
            Page = optionPage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionTreeNodeItem"/> class.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="optionPage">The optionPage<see cref="OptionPage"/>.</param>
        /// <param name="items">The items<see cref="OptionTreeNodeItem[]"/>.</param>
        public OptionTreeNodeItem(string title, OptionPage optionPage, OptionTreeNodeItem[] items) : this(title, optionPage)
        {
            Items = items;
        }

        #endregion Constructors of OptionTreeNode (5)

        #region Properties of OptionTreeNode (3)

        /// <summary>
        /// Gets or sets the Items.
        /// </summary>
        public OptionTreeNodeItem[] Items { get; set; }

        /// <summary>
        /// Gets or sets the Page.
        /// </summary>
        public OptionPage Page { get; set; }

        /// <summary>
        /// Gets the Text.
        /// </summary>
        public string Text { get; private set; }

        #endregion Properties of OptionTreeNode (3)
    }
}
