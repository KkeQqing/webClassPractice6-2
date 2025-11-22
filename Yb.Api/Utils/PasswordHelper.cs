//using Microsoft.AspNetCore.Cryptography.KeyDerivation;
//using System;
//using System.Security.Cryptography;
//using System.Text;

//namespace Yb.Api.Utils
//{
//    public static class PasswordHelper
//    {
//        public static string BuildPassword(string password)
//        {
//            var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
//            var hash = KeyDerivation.Pbkdf2(
//                password,
//                Encoding.UTF8.GetBytes(salt),
//                KeyDerivationPrf.HMACSHA256,
//                10000,
//                32);
//            return $"{salt}:{Convert.ToBase64String(hash)}";
//        }
//    }
//}