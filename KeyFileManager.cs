using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerSSDw41
{
    public class KeyFileManager
    {
        public byte[] GenerateKeyFileToMemory(string masterPassword, byte[] randomData)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = randomData;
                aes.GenerateIV();

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(aes.Key, 0, aes.Key.Length);
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] passwordBytes = Encoding.UTF8.GetBytes(masterPassword);
                        cs.Write(passwordBytes, 0, passwordBytes.Length);
                        cs.FlushFinalBlock();

                        return ms.ToArray();
                    }
                }
            }
        }

        public string ReadKeyFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] key = new byte[32];
                    fs.Read(key, 0, key.Length);
                    aes.Key = key;

                    byte[] iv = new byte[16];
                    fs.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (CryptoStream cs = new CryptoStream(fs, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}