namespace BitServer
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.gbAPI = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbKeys = new System.Windows.Forms.TextBox();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.gbMail = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRandom = new System.Windows.Forms.CheckBox();
            this.cbStrip = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbAPI.SuspendLayout();
            this.gbMail.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAPI
            // 
            this.gbAPI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAPI.Controls.Add(this.label7);
            this.gbAPI.Controls.Add(this.label6);
            this.gbAPI.Controls.Add(this.label5);
            this.gbAPI.Controls.Add(this.label4);
            this.gbAPI.Controls.Add(this.label3);
            this.gbAPI.Controls.Add(this.tbPass);
            this.gbAPI.Controls.Add(this.tbPort);
            this.gbAPI.Controls.Add(this.tbUser);
            this.gbAPI.Controls.Add(this.tbKeys);
            this.gbAPI.Controls.Add(this.tbIP);
            this.gbAPI.Location = new System.Drawing.Point(12, 12);
            this.gbAPI.Name = "gbAPI";
            this.gbAPI.Size = new System.Drawing.Size(495, 158);
            this.gbAPI.TabIndex = 0;
            this.gbAPI.TabStop = false;
            this.gbAPI.Text = "API";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(414, 26);
            this.label7.TabIndex = 9;
            this.label7.Text = "IP, Port, Username and Password are readed from the keys.dat File.\r\nIf no file na" +
                "me/path is given or it is invalid, the settings can be entered manually below.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "IP && Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "keys.dat File";
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(78, 97);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(100, 20);
            this.tbPass.TabIndex = 8;
            this.tbPass.Text = "PASS";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(184, 45);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(41, 20);
            this.tbPort.TabIndex = 4;
            this.tbPort.Text = "8442";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(78, 71);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(100, 20);
            this.tbUser.TabIndex = 6;
            this.tbUser.Text = "USER";
            // 
            // tbKeys
            // 
            this.tbKeys.Location = new System.Drawing.Point(78, 19);
            this.tbKeys.Name = "tbKeys";
            this.tbKeys.Size = new System.Drawing.Size(206, 20);
            this.tbKeys.TabIndex = 1;
            this.tbKeys.Text = "keys.dat";
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(78, 45);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(100, 20);
            this.tbIP.TabIndex = 3;
            this.tbIP.Text = "127.0.0.1";
            // 
            // gbMail
            // 
            this.gbMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMail.Controls.Add(this.label2);
            this.gbMail.Controls.Add(this.label1);
            this.gbMail.Controls.Add(this.cbRandom);
            this.gbMail.Controls.Add(this.cbStrip);
            this.gbMail.Location = new System.Drawing.Point(12, 176);
            this.gbMail.Name = "gbMail";
            this.gbMail.Size = new System.Drawing.Size(495, 152);
            this.gbMail.TabIndex = 1;
            this.gbMail.TabStop = false;
            this.gbMail.Text = "E-Mail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enable this to allow random@bitmessage.ch as sender.\r\nIf the random address is us" +
                "ed as sender, it will be replaced\r\nwith a newly generated Bitmessage Address.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enable this Option to remove all E-Mail headers and quoted printable encodings.\r\n" +
                "Allows better readability in bitmessage, but removes all E-Mail features\r\n(Threa" +
                "ding, Attachments, Multipart)";
            // 
            // cbRandom
            // 
            this.cbRandom.AutoSize = true;
            this.cbRandom.Location = new System.Drawing.Point(6, 86);
            this.cbRandom.Name = "cbRandom";
            this.cbRandom.Size = new System.Drawing.Size(176, 17);
            this.cbRandom.TabIndex = 2;
            this.cbRandom.Text = "Enable random@bitmessage.ch";
            this.cbRandom.UseVisualStyleBackColor = true;
            // 
            // cbStrip
            // 
            this.cbStrip.AutoSize = true;
            this.cbStrip.Location = new System.Drawing.Point(6, 19);
            this.cbStrip.Name = "cbStrip";
            this.cbStrip.Size = new System.Drawing.Size(275, 17);
            this.cbStrip.TabIndex = 0;
            this.cbStrip.Text = "Strip E-Mail Headers and quoted printable Text (=XX)";
            this.cbStrip.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(432, 339);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(351, 339);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(519, 374);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbMail);
            this.Controls.Add(this.gbAPI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.gbAPI.ResumeLayout(false);
            this.gbAPI.PerformLayout();
            this.gbMail.ResumeLayout(false);
            this.gbMail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAPI;
        private System.Windows.Forms.GroupBox gbMail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbRandom;
        private System.Windows.Forms.CheckBox cbStrip;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbKeys;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}