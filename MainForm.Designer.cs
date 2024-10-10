namespace PasswordManagerSSDw41
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.TextBox txtSite;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnSavePassword;
        private System.Windows.Forms.DataGridView dgvPasswords;
        private System.Windows.Forms.Button btnDeletePassword;

        private void InitializeComponent()
        {
            lblSite = new Label();
            txtSite = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnSavePassword = new Button();
            dgvPasswords = new DataGridView();
            btnDeletePassword = new Button();
            btnNewEntry = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPasswords).BeginInit();
            SuspendLayout();
            // 
            // lblSite
            // 
            lblSite.AutoSize = true;
            lblSite.Location = new Point(12, 15);
            lblSite.Name = "lblSite";
            lblSite.Size = new Size(26, 15);
            lblSite.TabIndex = 0;
            lblSite.Text = "Site";
            // 
            // txtSite
            // 
            txtSite.Location = new Point(15, 31);
            txtSite.Name = "txtSite";
            txtSite.Size = new Size(260, 23);
            txtSite.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 54);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(36, 15);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(15, 70);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(260, 23);
            txtEmail.TabIndex = 3;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(12, 93);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(60, 15);
            lblUsername.TabIndex = 4;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(15, 109);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(260, 23);
            txtUsername.TabIndex = 5;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(12, 132);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(15, 148);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(260, 23);
            txtPassword.TabIndex = 7;
            // 
            // btnSavePassword
            // 
            btnSavePassword.Location = new Point(119, 177);
            btnSavePassword.Name = "btnSavePassword";
            btnSavePassword.Size = new Size(75, 23);
            btnSavePassword.TabIndex = 8;
            btnSavePassword.Text = "Add/Save";
            btnSavePassword.UseVisualStyleBackColor = true;
            btnSavePassword.Click += btnSavePassword_Click;
            // 
            // dgvPasswords
            // 
            dgvPasswords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPasswords.Location = new Point(12, 203);
            dgvPasswords.Name = "dgvPasswords";
            dgvPasswords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPasswords.Size = new Size(732, 405);
            dgvPasswords.TabIndex = 10;
            dgvPasswords.CellClick += dgvPasswords_CellClick;
            // 
            // btnDeletePassword
            // 
            btnDeletePassword.Location = new Point(12, 626);
            btnDeletePassword.Name = "btnDeletePassword";
            btnDeletePassword.Size = new Size(75, 23);
            btnDeletePassword.TabIndex = 11;
            btnDeletePassword.Text = "Delete";
            // 
            // btnNewEntry
            // 
            btnNewEntry.Location = new Point(200, 177);
            btnNewEntry.Name = "btnNewEntry";
            btnNewEntry.Size = new Size(75, 23);
            btnNewEntry.TabIndex = 12;
            btnNewEntry.Text = "New Entry";
            btnNewEntry.UseVisualStyleBackColor = true;
            btnNewEntry.Click += btnNewEntry_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(759, 661);
            Controls.Add(btnNewEntry);
            Controls.Add(dgvPasswords);
            Controls.Add(btnDeletePassword);
            Controls.Add(btnSavePassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtSite);
            Controls.Add(lblSite);
            Name = "MainForm";
            Text = "Password Manager";
            ((System.ComponentModel.ISupportInitialize)dgvPasswords).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnNewEntry;
    }
}