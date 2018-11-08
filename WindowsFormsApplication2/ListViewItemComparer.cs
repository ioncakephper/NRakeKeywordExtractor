using System;
using System.Collections;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public enum ListViewItemComparerModes
    {
        Standard,
        CaseInsensitive,
        SimpleString,
        Date,
        Number
    }

    public class ListViewItemComparer : IComparer
    {
        // Specifies the column to be sorted
        private int ColumnToSort;


        public ListViewItemComparer()
        {
            Mode = ListViewItemComparerModes.Standard;
        }

        public ListViewItemComparer(ListViewItemComparerModes mode) : this()
        {
            Mode = mode;
        }

        public ListViewItemComparerModes Mode { get; set; }

        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            switch (Mode)
            {
                case ListViewItemComparerModes.Standard:
                    break;
                case ListViewItemComparerModes.CaseInsensitive:
                    var objectComparer = new CaseInsensitiveComparer();
                    compareResult = objectComparer.Compare(
                        listviewX.SubItems[ColumnToSort].Text,
                        listviewY.SubItems[ColumnToSort].Text
                    );
                    break;
                case ListViewItemComparerModes.SimpleString:
                    break;
                case ListViewItemComparerModes.Date:
                    break;
                case ListViewItemComparerModes.Number:
                    break;
                default:
                    break;
            }
        }
    }
}