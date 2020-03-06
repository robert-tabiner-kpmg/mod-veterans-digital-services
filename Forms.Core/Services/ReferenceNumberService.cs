using System;
using System.Security.Cryptography;
using System.Text;
using Forms.Core.Services.Interfaces;

namespace Forms.Core.Services
{
    public class ReferenceNumberService : IReferenceNumberService
    {
        public string GenerateReferenceNumber(string formId)
        {
            // Implementation from: https://paper.dropbox.com/doc/Transaction-reference-numbers-rutTu74goUwXfKxG9NBcr
            using SHA256 sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(formId));
            var hex = BitConverter.ToString(bytes).Replace("-", string.Empty);
            return hex.Substring(0, 6);
        }
    }
}