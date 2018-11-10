//------------------------------------------------------------------------------
// <copyright file="OptionsDialog.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
namespace WindowsFormsApp1
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="OptionsDialog" />.
    /// </summary>
    public partial class OptionsDialog : Form
    {
        #region Properties of OptionsDialog (1)

        /// <summary>
        /// Gets or sets the Options.
        /// </summary>
        public List<ApplicationOption> Options { get; set; }

        #endregion Properties of OptionsDialog (1)

        #region Constructors of OptionsDialog (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsDialog"/> class.
        /// </summary>
        public OptionsDialog()
        {
            InitializeComponent();
            Options = new List<ApplicationOption>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsDialog"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="List{ApplicationOption}"/>.</param>
        public OptionsDialog(List<ApplicationOption> options) : this()
        {
            Options = options;
        }

        #endregion Constructors of OptionsDialog (2)

        #region Methods of OptionsDialog (1)

        /// <summary>
        /// The OptionsDialog_Load.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.EventArgs"/>.</param>
        private void OptionsDialog_Load(object sender, System.EventArgs e)
        {
            // Populate controls from values passed in property. The property yet to develop.
        }

        #endregion Methods of OptionsDialog (1)
    }
}
