using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    /// <summary>
    /// Defines the <see cref="OptionPage" /> for <see cref="OptionTreeNodeItem" />.
    /// </summary>
    public class OptionPage
    {
        public OptionPage()
        {
        }

        public OptionPage(string text) : this()
        {
            Text = text;
        }

        /// <summary>
        /// Option page title
        /// </summary>
        public string Text
        {
            get; set;
        }
    }
}