using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices
{
    class Helpers
    {
        public string EncryptPassword(string rawPassword)
        {
            string password = "";
            int length = rawPassword.Length;
            password = (length.ToString().Length == 1 ? "0" + length.ToString()  : length.ToString()) +
                "?D20" +
                Reverse(rawPassword) +
                "X!O"; // Basit bir şifreleme algoritması
            return password;
        }
        public string DecryptPassword(string encryptedPassword)
        {
            string password = "";
            int length = encryptedPassword.Length;
            password = Reverse(encryptedPassword.Substring(6, length - 9));
            return password;

        }
        string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
