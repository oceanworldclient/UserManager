using System.Security.Cryptography;
using System.Text;

namespace UserManager.Server.Utils;

public static class AesUtils
{
    private static string KeyStr { get; } = "A?d?!<(A?><KD:LK";
    private static string IvStr { get; } = "A?d?!<(A?><KD:LK";

    public static string Encrypt(this string plainText)
    {
        try
        {
            // Check arguments.
            if (string.IsNullOrWhiteSpace(plainText))
                return "";

            // Create an Aes object
            // with the specified key and IV.
            using var aesAlg = Aes.Create();

            aesAlg.Key = Encoding.ASCII.GetBytes(KeyStr);
            aesAlg.IV = Encoding.ASCII.GetBytes(IvStr);

            // Create an encryptor to perform the stream transform.
            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for encryption.
            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using var swEncrypt = new StreamWriter(csEncrypt);

            //Write all data to the stream.
            swEncrypt.Write(plainText);
        
            var encrypted = msEncrypt.ToArray();
        
            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);
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
            var plaintext = "";

            var cipherTextBytes = Convert.FromBase64String(cipherText);
            // Create an Aes object
            // with the specified key and IV.
            using var aesAlg = Aes.Create();
            aesAlg.Key = Encoding.ASCII.GetBytes(KeyStr);
            aesAlg.IV = Encoding.ASCII.GetBytes(IvStr);

            // Create a decryptor to perform the stream transform.
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using var msDecrypt = new MemoryStream(cipherTextBytes);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }
        catch
        {
            return cipherText;
        }
    }
    
}