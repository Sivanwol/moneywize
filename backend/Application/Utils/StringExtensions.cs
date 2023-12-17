using System.Security.Cryptography;
using System.Text;
namespace Application.Utils;

public static class StringExtensions {
    public static string GenerateToken() {
        return RandomUniqueString(128);
    }
    public static string RandomUniqueString(int length, bool specialCharacters = true) {
        var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
        if (!specialCharacters) {
            alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        }
        var outOfRange = byte.MaxValue + 1 - (byte.MaxValue + 1) % alphabet.Length;
        var baseHash = CreateMD5Hash(Guid.NewGuid().ToString());
        var lengthHash = (length - baseHash.Length) - 1;
        var randomHash = string.Concat(Enumerable
            .Repeat(0, int.MaxValue)
            .Select(e => RandomByte())
            .Where(randomByte => randomByte < outOfRange)
            .Take(lengthHash)
            .Select(randomByte => alphabet[randomByte % alphabet.Length])
        );
        return $"{baseHash}.{randomHash}";
    }

    private static byte RandomByte() {
        var rand = RandomNumberGenerator.Create();
        var result = new byte[1];
        rand.GetBytes(result);
        return result[0];
    }
    public static string CreateMD5Hash(string input) {
        // Step 1, calculate MD5 hash from input
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        // Step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++) {
            sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString();
    }
}