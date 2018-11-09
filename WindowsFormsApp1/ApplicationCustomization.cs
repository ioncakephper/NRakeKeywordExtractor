using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class ApplicationCustomization
    {
        public ApplicationCustomization()
        {
            Options = new List<ApplicationCustomizationItem>();
        }

        public List<ApplicationCustomizationItem> Options { get; set; }

        public void Update(List<ApplicationCustomizationItem> options)
        {
            MessageBox.Show("Customizations are being updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}