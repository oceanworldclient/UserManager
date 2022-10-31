using System.Security.Cryptography;
using System.Text;

namespace UserManager.Server.Utils;

public static class SHA256Utils
{
    public static string Encrypt(string message, string salt = "")
    {
        var bytes = Encoding.UTF8.GetBytes(message + salt);
        var hash = SHA256.Create().ComputeHash(bytes);

        var builder = new StringBuilder();
        foreach (var t in hash)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }
}