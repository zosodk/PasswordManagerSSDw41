using Microsoft.Data.Sqlite;
using OtpNet;

public class UserManager
{
    private string connectionString = "Data Source=passwords.db;";

    public void CreateUser(string email, string name, string username, string masterPassword, string twoFactorSecret)
    {
        string masterPasswordHash = HashPassword(masterPassword);

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"
                INSERT INTO Users (Email, Name, Username, MasterPasswordHash, TwoFactorSecret)
                VALUES (@Email, @Name, @Username, @MasterPasswordHash, @TwoFactorSecret);";
            using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@MasterPasswordHash", masterPasswordHash);
                command.Parameters.AddWithValue("@TwoFactorSecret", twoFactorSecret);
                command.ExecuteNonQuery();
            }
        }
    }

    public bool VerifyMasterPassword(string username, string masterPassword)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string selectQuery = "SELECT MasterPasswordHash FROM Users WHERE Username = @Username;";
            using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                string storedHash = (string)command.ExecuteScalar();
                return VerifyPassword(masterPassword, storedHash);
            }
        }
    }

    public bool VerifyTwoFactorCode(string username, string code)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string selectQuery = "SELECT TwoFactorSecret FROM Users WHERE Username = @Username;";
            using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                string twoFactorSecret = (string)command.ExecuteScalar();
                var totp = new Totp(Base32Encoding.ToBytes(twoFactorSecret));
                return totp.VerifyTotp(code, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);
            }
        }
    }

    public int GetUserId(string username)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string selectQuery = "SELECT Id FROM Users WHERE Username = @Username;";
            using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                var result = command.ExecuteScalar();
                return (result != null) ? Convert.ToInt32(result) : -1; // Returner -1 hvis brugeren ikke findes
            }
        }
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
}
