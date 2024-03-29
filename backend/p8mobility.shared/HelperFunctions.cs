using System;
using System.Security.Cryptography;
using System.Text;

namespace p8_shared;

public class HelperFunctions
{
    /// <summary>
    /// Generates a hash from a string
    /// </summary>
    /// <param name="source"></param>
    /// <returns>Returns the hashed value as a string</returns>
    public virtual string GenerateHash(string source)
    {
        //Generates hash from string
        SHA256 sha256 = SHA256.Create();
        byte[] tmpSource = Encoding.UTF8.GetBytes(source);
        var tmpHash = sha256.ComputeHash(tmpSource);

        //Assembles the hash as a string from the byte[]
        StringBuilder sb = new StringBuilder();
        foreach (byte b in tmpHash)
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    /// <summary>
    /// Generates a salt
    /// </summary>
    /// <returns>Returns a salt as a string</returns>
    public virtual string GenerateSalt()
    {
        var random = RandomNumberGenerator.Create();
        byte[] buffer = new byte[32];
        random.GetBytes(buffer);
        string salt = BitConverter.ToString(buffer);
        return salt;
    }
}