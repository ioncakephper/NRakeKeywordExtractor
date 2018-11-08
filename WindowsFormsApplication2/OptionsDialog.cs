//------------------------------------------------------------------------------
// <copyright file="OptionsDialog.cs" company="Weblidity Software">
//      Copyright (c) Weblidity Software. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApplication2
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="OptionsDialog" />.
    /// </summary>
    public partial class OptionsDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsDialog"/> class.
        /// </summary>
        public OptionsDialog()
        {
            InitializeComponent();
            PopulateOptionTreeView(new TreeNode[] { });
        }

        private void PopulateOptionTreeView(TreeNode[] treeNodes)
        {
            TreeNodes = treeNodes;
            treeView1.Nodes.Clear();
            treeView1.Nodes.AddRange(TreeNodes);
        }

        public OptionsDialog(TreeNode[] treeNodes) : this()
        {
            PopulateOptionTreeView(treeNodes);
        }

        public TreeNode[] TreeNodes { get; set; }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
        }
    }
}
