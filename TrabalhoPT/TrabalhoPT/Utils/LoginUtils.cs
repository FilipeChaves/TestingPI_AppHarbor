using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using TrabalhoPT.DataMappers;
using TrabalhoPT.Models;

namespace TrabalhoPT.Utils
{
    public class LoginUtils
    {
        private const int CONF_CODE_SIZE = 16;
        private const int SALT_SIZE = 8;
        private const String EMAIL = "andre.fc.figueiredo@gmail.com";
        private const String EMAIL_PW = "1Calypse1";
        //private const String EMAIL = "trello.pi.isel@gmail.com";
        //private const String EMAIL_PW = "amelhorpassworddomundo";
        private const String CONF_SUBJECT = "Confirmação de registo!";
        private const String CONF_BASE_URI = "http://localhost:49864/Account/Verify/";

        public static void EncryptPassword(AccountModel accountModel)
        {
            var salt = CreateSalt(SALT_SIZE);
            var passwordHash = CreatePasswordHash(accountModel.Password, salt);
            accountModel.Salt = salt;
            accountModel.Password = passwordHash;
        }
        
        public static AccountModel RegisterAccount(RegisterAccountModel registerAccountModel)
        {
            var adm = AccountDataMapper.GetAccountDataMapper();
            if (adm.GetById(registerAccountModel.Username.ToLower()) == null)
            {
                AccountModel accountModel = new AccountModel();
                accountModel.Username = registerAccountModel.Username.ToLower();
                accountModel.Password = registerAccountModel.Password;
                accountModel.Email = registerAccountModel.Email;
                EncryptPassword(accountModel);
                accountModel.Roles = new List<string>() { "User" };
                adm.Add(accountModel);
                AddToConfirmList(accountModel);
                return accountModel;
            }
            return null;
        }

        public static void AddToConfirmList(AccountModel accountModel)
        {
            var toConfirm = ToConfirmDataMapper.GetAccountDataMapper();
            accountModel.ConfirmationCode = GenerateConfirmationCode(accountModel.Email.Substring(0, accountModel.Email.IndexOf('@')));
            accountModel.Confirmed = false;
            toConfirm.Add(accountModel);
        }

        public static bool ComparePasswords(string inserted, AccountModel accountModel)
        {
            String s = CreatePasswordHash(inserted, accountModel.Salt);
            return s.Equals(accountModel.Password);
        }

        private static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");
            hashedPwd = String.Concat(hashedPwd, salt);
            return hashedPwd;
        }

        private static String GenerateConfirmationCode(String email)
        {
            int rpt = CONF_CODE_SIZE / email.Count() + 1;
            String confirmationCode = "";
            while (rpt-- > 0)
                confirmationCode += Convert.ToBase64String(StrToByteArray(email));
            return Convert.ToBase64String(StrToByteArray(confirmationCode)).Substring(0, CONF_CODE_SIZE);
        }

        // C# to convert a string to a byte array.
        private static byte[] StrToByteArray(String str)
        {
            var encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        public static void SendConfirmationMail(AccountModel accountModel)
        {
            MailMessage m = new MailMessage();
            m.From = new MailAddress(EMAIL);
            m.To.Add(accountModel.Email);
            m.Subject = CONF_SUBJECT;
            m.Body = CONF_BASE_URI+accountModel.ConfirmationCode;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(EMAIL, EMAIL_PW);
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(m);
        }
    }
}