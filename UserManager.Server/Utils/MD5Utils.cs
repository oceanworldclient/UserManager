using System.Security.Cryptography;
using System.Text;

namespace UserManager.Server.Utils;

public static class MD5Utils
{
    public static string Encrypt(string msg, string salt = "")
    {
        using var md5 = MD5.Create();
        var arr = md5.ComputeHash(Encoding.UTF8.GetBytes(msg + salt));
        var sb = new StringBuilder();
        foreach (var t in arr)
        {
            sb.Append(t.ToString("x2"));
        }

        return sb.ToString();
    }
}