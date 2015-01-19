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
    public partial class FormSortList : Form
    {
        public List<string> Items
        {
            get
            {
                var list = new List<string>();
                foreach (var item in cmbExperiments.Items)
                    list.Add((string)item);
                return list;
            }
            set
            {
                cmbExperiments.Items.Clear();
                foreach (var item in value)
                {
                    cmbExperiments.Items.Add(item);
                }
            }
        }
        public FormSortList()
        {
            InitializeComponent();
        }

        private void cmdFirst_Click(object sender, EventArgs e)
        {
            var current = cmbExperiments.SelectedItem;
            if (current == null) return;

            cmbExperiments.Items.Remove(current);
            cmbExperiments.Items.Insert(0, current);
            cmbExperiments.SelectedItem = current;
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            var current = cmbExperiments.SelectedItem;
            if (current == null) return;
            int currentIndex = cmbExperiments.SelectedIndex;

            if (currentIndex < 1) return;

            cmbExperiments.Items.Remove(current);
            cmbExperiments.Items.Insert(currentIndex-1, current);
            cmbExperiments.SelectedItem = current;
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            var current = cmbExperiments.SelectedItem;
            if (current == null) return;
            int currentIndex = cmbExperiments.SelectedIndex;

            if (currentIndex +1 >= cmbExperiments.Items.Count) return;

            cmbExperiments.Items.Remove(current);
            cmbExperiments.Items.Insert(currentIndex + 1, current);
            cmbExperiments.SelectedItem = current;
        }

        private void cmdLast_Click(object sender, EventArgs e)
        {
            var current = cmbExperiments.SelectedItem;
            if (current == null) return;

            cmbExperiments.Items.Remove(current);
            cmbExperiments.Items.Add(current);
            cmbExperiments.SelectedItem = current;
        }
    }
}
