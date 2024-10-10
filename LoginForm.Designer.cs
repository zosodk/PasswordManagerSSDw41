namespace PasswordManagerSSDw41
{
    partial class LoginForm
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

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblMasterPassword;
        private System.Windows.Forms.Label lblTwoFactorCode;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtMasterPassword;
        private System.Windows.Forms.TextBox txtTwoFactorCode;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnSelectKeyFile; 
        private System.Windows.Forms.Button btnReset;
       private System.Windows.Forms.ToolTip toolTip;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblUsername = new Label();
            lblMasterPassword = new Label();
            lblTwoFactorCode = new Label();
            txtUsername = new TextBox();
            txtMasterPassword = new TextBox();
            txtTwoFactorCode = new TextBox();
            btnLogin = new Button();
            btnSelectKeyFile = new Button();
            btnReset = new Button();
            toolTip = new ToolTip(components);
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(14, 14);
            lblUsername.Margin = new Padding(4, 0, 4, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(60, 15);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username";
            // 
            // lblMasterPassword
            // 
            lblMasterPassword.AutoSize = true;
            lblMasterPassword.Location = new Point(14, 59);
            lblMasterPassword.Margin = new Padding(4, 0, 4, 0);
            lblMasterPassword.Name = "lblMasterPassword";
            lblMasterPassword.Size = new Size(96, 15);
            lblMasterPassword.TabIndex = 2;
            lblMasterPassword.Text = "Master Password";
            // 
            // lblTwoFactorCode
            // 
            lblTwoFactorCode.AutoSize = true;
            lblTwoFactorCode.Location = new Point(14, 104);
            lblTwoFactorCode.Margin = new Padding(4, 0, 4, 0);
            lblTwoFactorCode.Name = "lblTwoFactorCode";
            lblTwoFactorCode.Size = new Size(98, 15);
            lblTwoFactorCode.TabIndex = 4;
            lblTwoFactorCode.Text = "Two-Factor Code";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(14, 32);
            txtUsername.Margin = new Padding(4, 3, 4, 3);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(303, 23);
            txtUsername.TabIndex = 1;
            // 
            // txtMasterPassword
            // 
            txtMasterPassword.Location = new Point(14, 77);
            txtMasterPassword.Margin = new Padding(4, 3, 4, 3);
            txtMasterPassword.Name = "txtMasterPassword";
            txtMasterPassword.Size = new Size(303, 23);
            txtMasterPassword.TabIndex = 3;
            txtMasterPassword.UseSystemPasswordChar = true;
            // 
            // txtTwoFactorCode
            // 
            txtTwoFactorCode.Location = new Point(14, 122);
            txtTwoFactorCode.Margin = new Padding(4, 3, 4, 3);
            txtTwoFactorCode.Name = "txtTwoFactorCode";
            txtTwoFactorCode.Size = new Size(303, 23);
            txtTwoFactorCode.TabIndex = 5;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(111, 151);
            btnLogin.Margin = new Padding(4, 3, 4, 3);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(88, 27);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnSelectKeyFile
            // 
            btnSelectKeyFile.Location = new Point(12, 132);
            btnSelectKeyFile.Name = "btnSelectKeyFile";
            btnSelectKeyFile.Size = new Size(179, 23);
            btnSelectKeyFile.TabIndex = 7;
            btnSelectKeyFile.Text = "Select MasterPassword.key file";
            btnSelectKeyFile.UseVisualStyleBackColor = true;
            btnSelectKeyFile.Click += btnSelectKeyFile_Click;
            //
            // btnReset
            // 
            btnReset.BackColor = Color.IndianRed;
            btnReset.Location = new Point(267, 181);
            btnReset.Margin = new Padding(4, 3, 4, 3);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(50, 27);
            btnReset.TabIndex = 7;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            this.toolTip.SetToolTip(this.btnReset, "Clicking this will delete all your data and reset the application.");
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(331, 220);
            Controls.Add(btnReset);
            Controls.Add(btnLogin);
            Controls.Add(txtTwoFactorCode);
            Controls.Add(lblTwoFactorCode);
            Controls.Add(txtMasterPassword);
            Controls.Add(lblMasterPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Margin = new Padding(4, 3, 4, 3);
            Name = "LoginForm";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
