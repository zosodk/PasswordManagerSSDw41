# PasswordManagerSSDw41

# 11/10/2024
For building the project, you need to have the following installed:
Visual Studio 2022
Clone the the repository and open the solution file in Visual Studio 2022

NUGet Package Manager and install min. version at time of writing:
BCrypt.Net-Next(4.0.3)
GoogleAuthenticator(3.2.0)
Microsoft.Data.Sqlite(8.0.8)
Otp.NET(1.4.0)
QRCoder(1.6.0)
VaultSharp(1.17.5.1) // This is the HashiCorp Vault API not implemented, but code might be in place and not utilized. Plan was to store the MasterPassword.Key here - but i'm alone in this "group"
ZXing.Net(0.16.9)
ZXing.Net.Bindings.Windows.Compatibility(0.16.12)
Build the project and run the program

Further documentation and user manual can be found in the documentation folder

For using the compiled program you need to have:
A place to store a file called MasterPassword.Key with a path readable by the program
Google/MS/Alternative Authenticator installed on your phone (or any other TOTP app)

This program is not designed at the moment with UI/UX in mind, but rather as a proof of concept for a password manager
Known issues: The CRUD are implemented but an update or edit to an entry in the password manager sometimes creates a double. I chose to ignore this, at the scope of this assignment is security not fine tuning and bughunting.


Below is some cruel and nostalgic diagrams - but accurate :)


System Architecture:
+---------------------+       +-----------------+
|   InitialSetupForm  |       |   LoginForm     |
| +-----------------+ |       | +-------------+ |
| | KeyFileManager  | |       | | UserManager | |
| +-----------------+ |       | +-------------+ |
|   (Generates and   |       |                 |
|    encrypts keys)  |       | (Validates      |
+---------------------+       |  credentials &  |
                              |  2FA)           |
                              +-----------------+
                                     |
                                     v
                              +-------------+
                              |  MainForm   |
                              +-------------+
                                     |
                                     v
                              +-------------+
                              |  Database   |
                              +-------------+



Data Flow Diagram:
                   +----------------------------+
                   |       User Registration    |
                   +----------------------------+
                               |
                               v
                +----------------------------+
                | User enters master password|
                +----------------------------+
                               |
                               v
                +----------------------------+
                | Random data generated from  |
                | mouse movements             |
                +----------------------------+
                               |
                               v
                +----------------------------+
                | KeyFileManager:             |
                | - Generates encryption keys |
                | - Creates MasterPassword.key|
                | - Encrypts key file using   |
                |   AES                       |
                +----------------------------+
                               |
                               v
                +----------------------------+
                | Store encrypted MasterPassword|
                |.key file & KeyFilePath in    |
                | database                     |
                +----------------------------+

Security Workflow Diagram:
                +---------------------+
                |  User Registration  |
                +---------------------+
                           |
                           v
              +-----------------------------+
              |  Generate Random Data       |
              |  (from mouse movements)     |
              +-----------------------------+
                           |
                           v
              +-----------------------------+
              |  Derive Keys using SHA-256  |
              |  (Master password + Random  |
              |   Data)                     |
              +-----------------------------+
                           |
                           v
              +-----------------------------+
              |  Encrypt Data using AES     |
              +-----------------------------+
                           |
                           v
              +-----------------------------+
              |  Generate 2FA Seed and QR   |
              |  Code                        |
              +-----------------------------+
                           |
                           v
              +-----------------------------+
              |  Store Encrypted Data in    |
              |  Database                   |
              +-----------------------------+
