namespace ERP_BACKEND.helper;

using ERP_BACKEND.interfaces;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher : IPasswordHasher
 {
     public const string AlgoTag = "PBKDF2-SHA256";
     public const int SaltSize = 16;
     public const int KeySize = 32;

     public string Hash(string password, int iterations)
     {
         var salt = RandomNumberGenerator.GetBytes(SaltSize);
         var key = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, iterations, HashAlgorithmName.SHA256, KeySize);
         return $"{AlgoTag}${iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(key)}";
     }

   
     public bool Verify(string taggedHash, string password)
     {
         var parts = taggedHash.Split('$');
         if (parts.Length != 4 || parts[0] != AlgoTag) return false;

         var iterations = int.Parse(parts[1]);
         var salt = Convert.FromBase64String(parts[2]);
         var expected = Convert.FromBase64String(parts[3]);

         var actual = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, iterations, HashAlgorithmName.SHA256, expected.Length);
         return CryptographicOperations.FixedTimeEquals(actual, expected);
     }
 }