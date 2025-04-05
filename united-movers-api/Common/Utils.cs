using Konscious.Security.Cryptography;
using System.Data;
using System.Text;

namespace united_movers_api.Common
{
    public class Utils
    {
        public static IDataParameter AddParameter(IDbCommand command, string parameterName, object value, DbType dbType)
        {
            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value ?? DBNull.Value;
            parameter.DbType = dbType;
            return parameter;
        }

        public static string HashPassword(string password, byte[] salt)
        {
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 8; // Number of threads
                argon2.MemorySize = 65536; // Memory usage in KB
                argon2.Iterations = 4; // Number of iterations

                return Convert.ToBase64String(argon2.GetBytes(32)); // Hash output
            }
        }

        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(validChars[rnd.Next(validChars.Length)]);
            }
            return res.ToString();
        }
    }
}
