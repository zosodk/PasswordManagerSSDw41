using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class ConfigManager
{
    public static void SaveRandomData(byte[] randomData)
    {
        File.WriteAllBytes("RandomData.config", randomData);
    }

    public static byte[] GetRandomData()
    {
        return File.ReadAllBytes("RandomData.config");
    }

    public static void SaveKeyFilePath(string path, byte[] randomData)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = randomData;
            aes.GenerateIV();

            byte[] pathBytes = Encoding.UTF8.GetBytes(path);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(aes.Key, 0, aes.Key.Length);
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(pathBytes, 0, pathBytes.Length);
                    cs.FlushFinalBlock();
                }
                File.WriteAllBytes("KeyFilePath.config", ms.ToArray());
            }
        }
    }

    public static string GetKeyFilePath(string masterPassword, byte[] randomData)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = randomData;
            aes.GenerateIV();

            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes("KeyFilePath.config")))
            {
                byte[] key = new byte[32];
                ms.Read(key, 0, key.Length);
                aes.Key = key;

                byte[] iv = new byte[16];
                ms.Read(iv, 0, iv.Length);
                aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
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
