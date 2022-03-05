using System;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.PasswordUtilities
{
    public class PasswordUtilities: IPasswordUtilities
    {
        public bool IsCorrectCspPassword(string passwordToCheck)
        {
            var correctPassword = DateTime.UtcNow.Month + "TSQ";
            return passwordToCheck == correctPassword;
        }

        public bool IsCorrectUserPassword(User user, string password)
        {
            return QstV7Decryption(user.QstEncryptedPassword) == password;
        }

        private static string QstV7Decryption(string pwd)
        {
            int n;
            char c;
            string str = "";
            try
            {
                n = Int32.Parse(pwd.Substring(0, 2));
                for (int i = 1; i <= n; i++)
                {
                    var substring = pwd.Substring(i * 3 - 1, 3);

                    c = (char)(999 - Int32.Parse(substring));
                    str += c;
                }
            }
            catch (Exception)
            {
                return "";
            }
            return str;
        }
    }
}
