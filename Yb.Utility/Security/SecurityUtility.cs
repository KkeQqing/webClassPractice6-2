using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Yb.Utility.Security
{
    public class SecurityUtility
    {
        public static bool CheckRegEx(string C_Value, string C_Str)
        {
            Regex objAlphaPatt = new Regex(C_Str, RegexOptions.Compiled);
            return objAlphaPatt.Match(C_Value).Success;
        }

        public static string BuildPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return password;
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hash).Replace("-", "").ToLower().Substring(8, 16);
        }

        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\'|+|=]");
        }
    }
}