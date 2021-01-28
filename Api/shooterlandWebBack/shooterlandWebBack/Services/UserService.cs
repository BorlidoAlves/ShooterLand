using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using shooterlandWebBack.Entity;
using shooterlandWebBack.Helpers;

namespace shooterlandWebBack.Services.UserService
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Create(User user, string password);
        User GetById(int id);
        User[] GetAll();
        User UpdateCredentials(string email, string password);
        User UpdatePassword(string password, int id);
        void Delete(int id);
        string GenerateRandomPassword();
    }

    public class UserService : IUserService
    {
        private DataContext _context;
        
        public UserService(DataContext context)
        {
            _context = context;
        }
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            
            
            if (user == null)
                return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)){
                return null;
            }

            return user;
        }

        public User Create(User user, string password)
        {
            Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Match emailMatch = emailRegex.Match(user.Email);

            if (!emailMatch.Success)
            {
                throw new AppException("Incorrect email formatting");
            }

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");
            
            if (password.Length < 8)
                throw new AppException("Password is too short !! (8 characters minimun)");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username " + user.Username + " is already taken");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email " + user.Email + " is already use in one account");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Hidden = "False";
            user.Type = "User";

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetById(int id)
        {
            var user = _context.Users.Find(id);
            return user;

        }
        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Find(id));
            _context.SaveChanges();
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if(password == null) throw new ArgumentNullException("Password");

            if (string.IsNullOrWhiteSpace(password)) throw new AppException("Null password or have whitespaces");

            using(var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) {
                        
                        return false;
                    }
                }
            }

            return true;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public User UpdateCredentials(string email, string password)
        {
            if (_context.Users.Any(x => x.Email == email)){ 


                var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _context.SaveChanges();

                return user;
            }
            else
            {
                throw new AppException("No account registered with the given email !!");
            }
        }

        public string GenerateRandomPassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        public User UpdatePassword(string password, int id)
        {
            var user = _context.Users.Find(id);

            if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AppException("Use a new Password !!");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.SaveChanges();

            return user;
        }

        public User[] GetAll()
        {
            var listUsers = _context.Users.ToArray();
            return listUsers;
        }
    }
}
