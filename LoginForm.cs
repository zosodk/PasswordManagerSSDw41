using System;
using System.Windows.Forms;

namespace PasswordManagerSSDw41
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            btnLogin.Click += btnLogin_Click;
            btnSelectKeyFile.Click += btnSelectKeyFile_Click;
        }

    private void btnSelectKeyFile_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Key Files (*.key)|*.key|All Files (*.*)|*.*";
            openFileDialog.Title = "Select the MasterPassword.key file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string keyFilePath = openFileDialog.FileName;
                byte[] randomData = ConfigManager.GetRandomData();
                ConfigManager.SaveKeyFilePath(keyFilePath, randomData);
                txtMasterPassword.Tag = keyFilePath;
            }
        }
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Key Files (*.key)|*.key|All Files (*.*)|*.*";
            openFileDialog.Title = "Select the MasterPassword.key file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string keyFilePath = openFileDialog.FileName;
                byte[] randomData = ConfigManager.GetRandomData();
                KeyFileManager keyFileManager = new KeyFileManager();
                string storedMasterPassword = keyFileManager.ReadKeyFile(keyFilePath);

                string masterPassword = txtMasterPassword.Text;
                string username = txtUsername.Text;
                string twoFactorCode = txtTwoFactorCode.Text;

                UserManager userManager = new UserManager();

                if (userManager.VerifyMasterPassword(username, masterPassword) &&
                    masterPassword == storedMasterPassword &&
                    userManager.VerifyTwoFactorCode(username, twoFactorCode))
                {
                    int userId = userManager.GetUserId(username);
                    this.Hide();
                    var mainForm = new MainForm(userId, masterPassword);
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username, master password, or 2FA code.");
                }
            }
        }
    }


        private void btnReset_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to reset? This will delete all your passwords and reset the application.",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                // Slet konfigurationsfilen for at starte forfra
                if (File.Exists("KeyFilePath.config"))
                {
                    File.Delete("KeyFilePath.config");
                }

                // Slet SetupCompleted filen
                if (File.Exists("SetupCompleted.txt"))
                {
                    File.Delete("SetupCompleted.txt");
                }

                // Slet passwords databasen
                if (File.Exists("passwords.db"))
                {
                    File.Delete("passwords.db");
                }

                // Start InitialSetupForm igen
                this.Hide();
                var initialSetupForm = new InitialSetupForm();
                initialSetupForm.ShowDialog();
                this.Close();
            }
        }

    }
}