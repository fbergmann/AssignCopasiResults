using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AssignCopasiResults
{
    public partial class FormPreferences : Form
    {
        public FormPreferences()
        {
            InitializeComponent();
        }

        private void OnBrowseCurrent(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Title = "Locate UI executable", FileName = CurrentExecutable })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    txtCurrent.Text = dialog.FileName;
            }
        }

        public string CurrentExecutable
        {
            get
            {
                return txtCurrent.Text;
            }
            set
            {
                txtCurrent.Text = value;
            }
        }

    }
}
