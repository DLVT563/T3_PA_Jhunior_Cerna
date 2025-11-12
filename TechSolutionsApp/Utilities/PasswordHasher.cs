using System.Security.Cryptography;
using System.Text;

namespace TechSolutionsApp.Utilities;

public static class PasswordHasher
{
    public static string Hash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashBytes);
    }

    public static bool Verify(string input, string hash) => Hash(input) == hash;
}
