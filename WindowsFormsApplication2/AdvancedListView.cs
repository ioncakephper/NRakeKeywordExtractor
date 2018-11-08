using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class AdvancedListView : ListView
    {
        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0xf;

            // if the control is in details view mode and columns
            // have been added, then intercept the WM_PAINT message
            // and reset the last column width to fill the list view
            switch (m.Msg)
            {
                case WM_PAINT:
                    if (this.View == View.Details && this.Columns.Count > 0)
                        this.Columns[this.Columns.Count - 1].Width = -2;
                    break;
            }

            // pass messages on to the base control for processing
            base.WndProc(ref m);
        }
    }

    public class FilterableListView : AdvancedListView
    {
    }
}