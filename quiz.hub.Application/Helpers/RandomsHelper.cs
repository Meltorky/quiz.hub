using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Helpers
{
    public static class RandomsHelper
    {
        public static string GenerateSecureConnectionCode(int length = 7)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[RandomNumberGenerator.GetInt32(chars.Length)])
                .ToArray());
        }

    }
}
