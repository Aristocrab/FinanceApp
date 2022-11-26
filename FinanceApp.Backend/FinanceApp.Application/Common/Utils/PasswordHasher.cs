namespace FinanceApp.Application.Common.Utils;

public static class PasswordHasher
{
    public static string HashPassword(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public static string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }

    public static bool ValidatePassword(string inputPassword, string salt, string hashedPassword)
    {
        return HashPassword(inputPassword, salt) == hashedPassword;
    }
}