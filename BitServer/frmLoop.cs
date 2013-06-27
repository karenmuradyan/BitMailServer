using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BitServer
{
    public partial class frmLoop : Form
    {
        public frmLoop()
        {
            InitializeComponent();
        }

        private void frmLoop_Shown(object sender, EventArgs e)
        {
            Program.buildIcon();
            Program.startListener();
            Hide();
        }
    }
}
