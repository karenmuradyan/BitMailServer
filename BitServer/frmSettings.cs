using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BitServer
{
    public partial class frmSettings : Form
    {
        public BitSettings BS;
        public frmSettings(BitSettings B)
        {
            InitializeComponent();
            BS = B;
            tbKeys.Text = BS.BitConfig;
            tbIP.Text = BS.IP;
            tbPort.Text = BS.Port>=0?BS.Port.ToString():"8442";
            tbUser.Text = BS.UName.ToString();
            tbPass.Text = BS.UPass;
            cbRandom.Checked = BS.Random;
            cbStrip.Checked = BS.StripHdr;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;
            //API
            BS.BitConfig = tbKeys.Text;
            BS.IP = tbIP.Text;
            if (int.TryParse(tbPort.Text, out i) && i > 0 && i <= ushort.MaxValue)
            {
                BS.Port = i;
            }
            else
            {
                BS.Port = 8442;
                MessageBox.Show("Port is invalid. Default Port 8442 set");
            }
            BS.UName = tbUser.Text;
            BS.UPass = tbPass.Text;

            //Mail
            BS.Random = cbRandom.Checked;
            BS.StripHdr = cbStrip.Checked;
            this.DialogResult = DialogResult.OK;
        }
    }
}
