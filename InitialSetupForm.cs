using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using QRCoder;
using Google.Authenticator;

namespace PasswordManagerSSDw41
{
    public partial class InitialSetupForm : Form
    {
        private List<Point> mouseMovements = new List<Point>();
        private string keyFilePath = "MasterPassword.key";

        public InitialSetupForm()
        {
            InitializeComponent();
            Load += InitialSetupForm_Load;
            pictureBoxMouseMovement.MouseMove += PictureBox_MouseMove;
            btnSetup.Click += btnSetup_Click;
            linkDownloadKeyFile.Click += linkDownloadKeyFile_Click;
            btnContinue.Click += btnContinue_Click;
        }

        private void InitialSetupForm_Load(object sender, EventArgs e)
        {
            DatabaseInitializer dbInitializer = new DatabaseInitializer();
            dbInitializer.InitializeDatabase();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            // Check if the login form is already open
            if (Application.OpenForms["LoginForm"] == null)
            {
                // Open the LoginForm
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                // If LoginForm is already open, bring it to front
                Application.OpenForms["LoginForm"].BringToFront();
            }
        }


        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMovements.Add(e.Location);
            txtMasterPassword.Text = GenerateRandomPasswordFromMouseMovements(32);
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string name = txtName.Text;
            string username = txtUsername.Text;
            string masterPassword = txtMasterPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (masterPassword == confirmPassword)
            {
                byte[] randomData = GenerateRandomDataFromMouseMovements();
                KeyFileManager keyFileManager = new KeyFileManager();
                byte[] encryptedKeyData = keyFileManager.GenerateKeyFileToMemory(masterPassword, randomData);

                string twoFactorSecret = GenerateTwoFactorSecret();
                UserManager userManager = new UserManager();
                userManager.CreateUser(email, name, username, masterPassword, twoFactorSecret);

                ConfigManager.SaveRandomData(randomData); // Save randomData

                // Gem stien til nøglefilen
                ConfigManager.SaveKeyFilePath("MasterPassword.key", randomData);


                ShowQrCode(twoFactorSecret);

                linkDownloadKeyFile.Visible = true;
                linkDownloadKeyFile.Tag = encryptedKeyData;
                btnContinue.Visible = true;

                MessageBox.Show("Setup complete! Please save your key file securely. Remember to keep your master password safe!");
            }
            else
            {
                MessageBox.Show("Passwords do not match!");
            }
        }


        private void linkDownloadKeyFile_Click(object sender, EventArgs e)
        {
            byte[] keyData = (byte[])((LinkLabel)sender).Tag;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "MasterPassword.key",
                Filter = "Key Files (*.key)|*.key|All Files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog.FileName, keyData);
                MessageBox.Show("Key file downloaded successfully.");
            }
        }

        private byte[] GenerateRandomDataFromMouseMovements()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var point in mouseMovements)
                {
                    sb.Append(point.X);
                    sb.Append(point.Y);
                }
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            }
        }

        private string GenerateRandomPasswordFromMouseMovements(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder sb = new StringBuilder();
            using (SHA256 sha256 = SHA256.Create())
            {
                foreach (var point in mouseMovements)
                {
                    sb.Append(point.X);
                    sb.Append(point.Y);
                }
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
                Random random = new Random(BitConverter.ToInt32(hash, 0));
                char[] password = new char[length];
                for (int i = 0; i < length; i++)
                {
                    password[i] = validChars[random.Next(validChars.Length)];
                }
                return new string(password);
            }
        }

        private string GenerateTwoFactorSecret()
        {
            byte[] secretKey = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(secretKey);
            }
            return Base32Encoding.ToString(secretKey);
        }

        private void ShowQrCode(string twoFactorSecret)
        {
            try
            {
                string user = txtEmail.Text;
                string issuer = "PasswordManagerSSDw41";
                string qrCodeData = $"otpauth://totp/{issuer}:{user}?secret={twoFactorSecret}&issuer={issuer}";

                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    QRCodeData qrCodeDataObj = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                    using (QRCode qrCode = new QRCode(qrCodeDataObj))
                    {
                        using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                        {
                            Bitmap resizedQrCodeImage = new Bitmap(qrCodeImage, pictureBoxQRCode.Size);
                            pictureBoxQRCode.Image = resizedQrCodeImage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while generating the QR code: {ex.Message}");
            }
        }
    }
}
