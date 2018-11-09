using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class ApplicationOptions
    {
        public ApplicationOptions()
        {
            Options = new List<ApplicationOption>();
        }

        public List<ApplicationOption> Options { get; set; }

        public void Update(List<ApplicationOption> options)
        {
            MessageBox.Show("Updating application options...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}