using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CustomizationDialog : Form
    {
        public CustomizationDialog()
        {
            InitializeComponent();
            Options = new List<ApplicationCustomizationItem>();
        }

        public CustomizationDialog(List<ApplicationCustomizationItem> options) : this()
        {
            Options = options;
        }

        public List<ApplicationCustomizationItem> Options
        {
            get; set;
        }

        private void CustomizationDialog_Load(object sender, System.EventArgs e)
        {
            // TODO: Dialog controls need to be initialized with values passed on in Options property.
        }
    }
}
