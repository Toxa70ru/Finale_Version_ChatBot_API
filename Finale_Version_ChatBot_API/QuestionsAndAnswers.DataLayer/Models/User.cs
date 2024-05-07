﻿using QuestionsAndAnswers.DataLayer.Repository;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace QuestionsAndAnswers.DataLayer.Models
{
    public class User:BaseEntity
    {
        //public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        //public long RolesID { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }

    /*public interface IAuthService
    {
        bool IsUsernameTaken(string username);
        User Authenticate(string username, string password);
        User Register(User user, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly ChatBotContext _context;
        public AuthService(ChatBotContext context)
        {
            _context = context;
        }

        public bool IsUsernameTaken(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            if (user == null || !VerfyPasswordHash(password, user.Password))
            {
                return null;
            }
            return user;
        }

        public User Register(User user, string password)
        {
            if (IsUsernameTaken(user.Username))
            {
                return null;
            }
            user.Password = HashPassword(password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerfyPasswordHash(string password, string hashedPassword)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return AreHashesEqual(buffer3, buffer4);
        }
        static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }*/



        /*public bool VerfyPasswordHash(string password, string storedHash) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) 
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                var sb = new StringBuilder();
                foreach (byte b in computedHash)
                {
                    //sb.Append(b.ToString("X2"));
                    sb.Append(b.ToString());
                }
                return storedHash == sb.ToString();
            }
        }*/

        /*private string HashPassword(string password) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                var sb = new StringBuilder();
                foreach (byte b in computedHash)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }*/
    //}
}
