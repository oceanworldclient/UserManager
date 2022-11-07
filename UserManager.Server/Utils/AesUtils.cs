using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserManager.Server.Utils;

public static class AesUtils
{
    private static string KeyStr { get; set; } = "A?d?!<(A?><KD:LK";
    private static string IvStr { get; set; } = "A?d?!<(A?><KD:LK";

    public static void Set(string key)
    {
        Console.WriteLine("key:" + key);
        KeyStr = key;
        IvStr = key;
    }

    public static string Encrypt(this string plainText)
    {
        try
        {
            // Check arguments.
            if (string.IsNullOrWhiteSpace(plainText))
                return "";

            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.ASCII.GetBytes(KeyStr);
                aesAlg.IV = Encoding.ASCII.GetBytes(IvStr);

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Base64UrlEncoder.Encode(encrypted); 
            // Return the encrypted bytes from the memory stream.
        }
        catch (Exception e)
        {
            return plainText;
        }
    }

    public static string Decrypt(this string cipherText)
    {
        try
        {
            // Check arguments.
            if (string.IsNullOrWhiteSpace(cipherText))
                return "";

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = "";

            byte[] cipherTextBytes = Base64UrlEncoder.DecodeBytes(cipherText);
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.ASCII.GetBytes(KeyStr);
                aesAlg.IV = Encoding.ASCII.GetBytes(IvStr);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
        catch
        {
            return cipherText;
        }
    }
    
}