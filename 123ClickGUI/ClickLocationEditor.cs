using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _123ClickGUI
{
    public partial class ClickLocationEditor : Form
    {
        public ClickLocations clickLocations;
        private bool editMode;
        public ClickLocationEditor(ClickLocations clickLocations)
        {
            InitializeComponent();
            editMode = false;
            this.clickLocations = clickLocations;
            btnSave.Enabled = false;
        }

        public ClickLocationEditor(string title, int x, int y, ClickLocations clickLocations)
        {
            InitializeComponent();
            editMode = true;
            this.clickLocations = clickLocations;
            tbTitle.Text = title;
            tbX.Text = x.ToString();
            tbY.Text = y.ToString();
            btnSave.Enabled = true;
        }


        private void tbXY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            if(tbTitle.TextLength > 0 && tbX.TextLength > 0 && tbY.TextLength > 0)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(editMode)
                clickLocations.editRecord(tbTitle.Text, int.Parse(tbX.Text), int.Parse(tbY.Text));
            else
                clickLocations.addRecordWithName(tbTitle.Text, int.Parse(tbX.Text), int.Parse(tbY.Text));
            this.Close();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            ((Button)sender).Focus();
        }

        private void ClickLocationEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Home)
            {
                tbX.Text = Cursor.Position.X.ToString();
                tbY.Text = Cursor.Position.Y.ToString();
                e.SuppressKeyPress = true;
            }
        }
    }
}
