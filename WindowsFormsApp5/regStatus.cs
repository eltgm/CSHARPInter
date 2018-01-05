using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class regStatus : Form
    {
        public regStatus(string st)
        {
            InitializeComponent();
            this.status.Text = st;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}