using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;

namespace WindowsFormsControlLibrary1
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        private NumberCaseInsensitiveComparer objectCompare;
        private ImageTextComparer firstObjectCompare;
        private CheckboxTextComparer firstObjectCompare2;

        public enum SortModifiers
        {
            SortByImage,
            SortByCheckbox,
            SortByText
        }

        public int SortColumn { get; set; }
        public SortOrder Order { get; set; }

        public SortModifiers SortModifier
        {
            get; set;
        }

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            SortColumn = 0;
            SortModifier = SortModifiers.SortByText;

            // Initialize the CaseInsensitiveComparer object
            objectCompare = new NumberCaseInsensitiveComparer();
            firstObjectCompare = new ImageTextComparer();
            firstObjectCompare2 = new CheckboxTextComparer();
        }
        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult = 0;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            ListView listViewMain = listviewX.ListView;

            // Calculate correct return value based on object comparison
            if (listViewMain.Sorting != SortOrder.Ascending &&
                listViewMain.Sorting != SortOrder.Descending)
            {
                // Return '0' to indicate they are equal
                return compareResult;
            }

            if (SortModifier.Equals(SortModifiers.SortByText) || SortColumn > 0)
            {
                // Compare the two items

                if (listviewX.SubItems.Count <= SortColumn &&
                    listviewY.SubItems.Count <= SortColumn)
                {
                    compareResult = objectCompare.Compare(null, null);
                }
                else if (listviewX.SubItems.Count <= SortColumn &&
                         listviewY.SubItems.Count > SortColumn)
                {
                    compareResult = objectCompare.Compare(null, listviewY.SubItems[SortColumn].Text.Trim());
                }
                else if (listviewX.SubItems.Count > SortColumn && listviewY.SubItems.Count <= SortColumn)
                {
                    compareResult = objectCompare.Compare(listviewX.SubItems[SortColumn].Text.Trim(), null);
                }
                else
                {
                    compareResult = objectCompare.Compare(listviewX.SubItems[SortColumn].Text.Trim(), listviewY.SubItems[SortColumn].Text.Trim());
                }
            }
            else
            {
                switch (SortModifier)
                {
                    case SortModifiers.SortByCheckbox:
                        compareResult = firstObjectCompare2.Compare(x, y);
                        break;
                    case SortModifiers.SortByImage:
                        compareResult = firstObjectCompare.Compare(x, y);
                        break;
                    default:
                        compareResult = firstObjectCompare.Compare(x, y);
                        break;
                }
            }

            // Calculate correct return value based on object comparison
            if (Order == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (Order == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }
    }
    public class ImageTextComparer : IComparer
    {
        private NumberCaseInsensitiveComparer objectCompare;

        public ImageTextComparer()
        {
            // Initialize the CaseInsensitiveComparer object
            objectCompare = new NumberCaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            int image1, image2;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            image1 = listviewX.ImageIndex;
            listviewY = (ListViewItem)y;
            image2 = listviewY.ImageIndex;

            if (image1 < image2)
            {
                return -1;
            }
            else if (image1 == image2)
            {
                return objectCompare.Compare(listviewX.Text.Trim(), listviewY.Text.Trim());
            }
            else
            {
                return 1;
            }
        }
    }

    public class CheckboxTextComparer : IComparer
    {
        private NumberCaseInsensitiveComparer objectCompare;

        public CheckboxTextComparer()
        {
            // Initialize the CaseInsensitiveComparer object
            objectCompare = new NumberCaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            // Cast the objects to be compared to ListViewItem objects
            ListViewItem listviewX = (ListViewItem)x;
            ListViewItem listviewY = (ListViewItem)y;

            if (listviewX.Checked && !listviewY.Checked)
            {
                return -1;
            }
            else if (listviewX.Checked.Equals(listviewY.Checked))
            {
                if (listviewX.ImageIndex < listviewY.ImageIndex)
                {
                    return -1;
                }
                else if (listviewX.ImageIndex == listviewY.ImageIndex)
                {
                    return objectCompare.Compare(listviewX.Text.Trim(), listviewY.Text.Trim());
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
    }

    public class NumberCaseInsensitiveComparer : CaseInsensitiveComparer
    {
        public NumberCaseInsensitiveComparer()
        {

        }

        public new int Compare(object x, object y)
        {
            if (x == null)
            {
                return (y == null) ? 0 : -1;
            } else
            {
                if (y == null)
                {
                    return 1;
                }
            }

            if ((x is System.String) && IsDecimalNumber((string)x) && (y is System.String) && IsDecimalNumber((string)y))
            {
                try
                {
                    decimal xx = Decimal.Parse(((string)x).Trim());
                    decimal yy = Decimal.Parse(((string)y).Trim());

                    return base.Compare(xx, yy);
                }
                catch
                {
                    return -1;
                }
            }

            return base.Compare(x, y);           
        }

        // http://stackoverflow.com/questions/4246077/matching-numbers-with-regular-expressions-only-digits-and-commas/4247184#4247184
        // https://www.debuggex.com/r/Lyx0F0y1LORvNhwA
        private bool IsDecimalNumber(string strNumber)
        {
            string regex = @"^-?(\d+|(\d{1,3}((,|\.)\d{3})*))((,|\.)\d+)?$";

            Regex wholePattern = new Regex(regex);
            return wholePattern.IsMatch(strNumber);
        }
    }
}