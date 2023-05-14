using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace C969___Task_1
{
    class User
    {
        int _userId;
        string _userName;

        public int ID
        {
            get { return _userId; }
        }
        private bool IsHash(string password)
        {
            string sha512Pattern = @"^[A-Fa-f0-9]{50}$"; //50 characters; must be hex characters ie 0-9, A-F. hex strings are case insensitive so also a-f.
            Regex regex = new Regex(sha512Pattern);
            bool isMatch = regex.IsMatch(password);
            return isMatch;
        }
        private string HashPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(_userId.ToString() + password + _userName); //userId and username included as salt
                byte[] hashBytes = sha512.ComputeHash(inputBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").Substring(0,50);
                return hash;
            }
        }
        public User(int userId)
        {
            //lookup user from database.
            if (DatabaseInterface.UserGet(userId, out string userName))
            {
                _userId = userId;
                _userName = userName;
            }
        }
        internal User(string userName, string password)
        {
            _userId = DatabaseInterface.UserAdd(userName);
            _userName = userName;
            SetPassword(password);
        }
        internal User(int userId, string userName)
        {
            //no need to query database. 
            _userId = userId;
            _userName = userName;
        }
        public User (string userName)
        {
            //lookup userId from database.
            if (DatabaseInterface.UserGet(userName, out int userId))
            {
                _userName = userName;
                _userId = userId;
            }
        }
        public string DisplayName()
        {
            return this._userName;
        }
        private bool SetPassword(string password)
        {
            if (IsHash(password))
            {
                return DatabaseInterface.UserPasswordSet(_userId, password);
            }
            else
            {
                return DatabaseInterface.UserPasswordSet(_userId, HashPassword(password));
            }
        }
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (VerifyPassword(oldPassword))
            {
                return SetPassword(newPassword);
            }
            else
            {
                throw new Exception(Language.LanguageFill("#invalidpassword"));
            }
        }
        public bool VerifyPassword(string password)
        {
            if (password == "")
            {
                return false;
            }
            if (!IsHash(password))
            {
                password = HashPassword(password);
            }
            if (DatabaseInterface.UserPasswordGet(_userId, out string dbPassword))
            {
                if (!IsHash(dbPassword))
                {
                    SetPassword(dbPassword);
                    dbPassword = HashPassword(dbPassword);
                }
                if (dbPassword == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception(Language.LanguageFill("#internalerror #usernotfound"));
            }
        }
    }
}
