using System.Security.Cryptography;

namespace ProdutosApp.Utils;

public class PasswordHelper
{
    public static string GenerateSalt()
    {
        var bytes = new byte[128 / 8];
        using var keyGenerator = RandomNumberGenerator.Create();
        keyGenerator.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public static string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var saltedPassword = string.Concat(password, salt);
        var saltedPasswordAsBytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
        return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
    }
    
    public static bool VerifyPassword(string password, string salt, string existingHash)
    {
        var hashOfInput = HashPassword(password, salt);

        // Compara o hash da senha inserida com o hash armazenado
        return hashOfInput == existingHash;
    }
}