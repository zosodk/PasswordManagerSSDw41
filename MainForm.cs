using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace PasswordManagerSSDw41
{
    public partial class MainForm : Form
    {
        private string connectionString = "Data Source=passwords.db;";
        private int userId;
        private string masterPassword;

        public MainForm(int userId, string masterPassword)
        {
            InitializeComponent();
            this.userId = userId;
            this.masterPassword = masterPassword; // Brug master passwordet fra login
            btnSavePassword.Click += btnSavePassword_Click;
            btnDeletePassword.Click += btnDeletePassword_Click;
            //this.Shown += MainForm_Shown;
            LoadPasswords();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadPasswords();
        }
        private bool isSaving = false;
        private bool ndLoop = true;
        private int currentEditingId = -1; // Track currently editing ID

        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            isSaving = true; // Move this to the top to prevent any immediate re-entry

            if (isSaving)
            {
                return;
            }

            try
            {
                string site = txtSite.Text;
                string email = txtEmail.Text;
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                string encryptedPassword = EncryptPassword(password);

                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query;

                    if (currentEditingId >= 0)
                    {
                        // Update existing entry
                        query = @"
                    UPDATE Passwords
                    SET Site = @Site, Email = @Email, Username = @Username, EncryptedPassword = @EncryptedPassword
                    WHERE Id = @Id AND UserId = @UserId";
                        using (SqliteCommand command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Site", site);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@EncryptedPassword", encryptedPassword);
                            command.Parameters.AddWithValue("@UserId", userId);
                            command.Parameters.AddWithValue("@Id", currentEditingId);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert new entry
                        query = @"
                    INSERT INTO Passwords (Site, Email, Username, EncryptedPassword, UserId)
                    VALUES (@Site, @Email, @Username, @EncryptedPassword, @UserId);";
                        using (SqliteCommand command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Site", site);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@EncryptedPassword", encryptedPassword);
                            command.Parameters.AddWithValue("@UserId", userId);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Password saved successfully.");

                // Disable event handler temporarily
                dgvPasswords.CellClick -= dgvPasswords_CellClick;
                LoadPasswords(); // Reload the passwords to reflect the new addition
                dgvPasswords.CellClick += dgvPasswords_CellClick; // Re-enable event handler

                currentEditingId = -1; // Reset editing ID to -1 after saving
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during save: " + ex.Message);
            }
            finally
            {
                isSaving = false;
               
            }
        }

        private void LoadPasswords()
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT Id, Site, Email, Username, EncryptedPassword FROM Passwords WHERE UserId = @UserId";
                    using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dgvPasswords.DataSource = dt;

                            // Improve layout
                            
                            foreach (DataGridViewColumn column in dgvPasswords.Columns)
                            {
                                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            }
                            
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading passwords: " + ex.Message);
            }
        }


        private void btnNewEntry_Click(object sender, EventArgs e)
        {
            txtSite.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            dgvPasswords.ClearSelection(); // Deselect any selected rows in the DataGridView
            currentEditingId = -1; // Reset editing ID to -1 for new entry
        }


        private void dgvPasswords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvPasswords.Rows.Count &&
                    dgvPasswords.Rows[e.RowIndex].Cells["Site"].Value != null)
                {
                    DataGridViewRow row = dgvPasswords.Rows[e.RowIndex];
                    txtSite.Text = row.Cells["Site"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();
                    txtUsername.Text = row.Cells["Username"].Value.ToString();
                    txtPassword.Text = DecryptPassword(row.Cells["EncryptedPassword"].Value.ToString());
                    currentEditingId = Convert.ToInt32(row.Cells["Id"].Value); // Set current editing ID
                }
            }
            catch (Exception ex)
            {
                LoadPasswords();
            }
        }

        private void btnDeletePassword_Click(object sender, EventArgs e)
        {
            if (dgvPasswords.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvPasswords.SelectedRows[0].Cells["Id"].Value);

                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Passwords WHERE Id = @Id";
                    using (SqliteCommand command = new SqliteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }

                //MessageBox.Show("Password deleted successfully.");
                LoadPasswords(); // Reload the passwords to reflect the deletion
            }
        }

        private string EncryptPassword(string password)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key = GenerateKey();
                aes.Key = key;
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new System.IO.MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(password);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private string DecryptPassword(string encryptedPassword)
        {
            byte[] fullCipher = Convert.FromBase64String(encryptedPassword);

            using (Aes aes = Aes.Create())
            {
                byte[] iv = new byte[16];
                byte[] cipher = new byte[fullCipher.Length - iv.Length];

                Array.Copy(fullCipher, iv, iv.Length);
                Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                byte[] key = GenerateKey();
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new System.IO.MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private byte[] GenerateKey()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(masterPassword));
            }
        }

       

    }
}
