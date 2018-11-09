using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
            Options = new List<ApplicationOption>();
        }

        public OptionsDialog(List<ApplicationOption> options) : this()
        {
            Options = options;
        }

        public List<ApplicationOption> Options { get; set; }

        private void OptionsDialog_Load(object sender, System.EventArgs e)
        {
            // TODO: Dialog controls should be initialized using dialogs's Options property
        }
    }
}
