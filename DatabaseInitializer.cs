using System;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows.Forms;

namespace PasswordManagerSSDw41
{
    public class DatabaseInitializer
    {
        private string connectionString = "Data Source=passwords.db;";

        public void InitializeDatabase()
        {
            if (!IsInitialSetupCompleted())
            {
                DeleteExistingDatabase();
                CreateDatabase();
                MarkInitialSetupAsCompleted();
            }
            else
            {
                // Only show message if database exists and no action is needed.
                if (File.Exists("passwords.db"))
                {
                    MessageBox.Show("Database already initialized.");
                }
            }
        }

        private void DeleteExistingDatabase()
        {
            if (File.Exists("passwords.db"))
            {
                File.Delete("passwords.db");
            }
        }

        private void CreateDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createUsersTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Email TEXT NOT NULL,
                        Name TEXT NOT NULL,
                        Username TEXT NOT NULL,
                        MasterPasswordHash TEXT NOT NULL,
                        TwoFactorSecret TEXT NOT NULL
                    );";
                using (SqliteCommand command = new SqliteCommand(createUsersTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                string createPasswordsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Passwords (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Site TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Username TEXT NOT NULL,
                        EncryptedPassword TEXT NOT NULL,
                        UserId INTEGER NOT NULL,
                        FOREIGN KEY (UserId) REFERENCES Users(Id)
                    );";
                using (SqliteCommand command = new SqliteCommand(createPasswordsTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Database initialized successfully.");
            }
        }

        private bool IsInitialSetupCompleted()
        {
            // Assuming the presence of key file indicates setup completion.
            return File.Exists("MasterPassword.key");
        }

        private void MarkInitialSetupAsCompleted()
        {
            // Create a simple text file as a marker.
            File.WriteAllText("SetupCompleted.txt", "Initial setup is done");
        }
    }
}
