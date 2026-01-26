using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Fask.Encryption
{
    public class RijndaelWrapper
    {
        /// <summary>
        /// Generates key and IV from the specified password, using an MD5 hash.
        /// </summary>
        /// <remarks>
        /// Usually you would prefer to use Rfc2898DeriveBytes for this purpose but since 
        /// .net compact framework (for windows mobile) does not have any implementation
        /// for Rcf2898DeriveBytes or even PasswordDeriveBytes, this is a simple workaround
        /// for it.
        /// </remarks>
        /// <param name="password">The password to generate a key/ITfrom it.</param>
        /// <param name="sa">The SymmetricAlgorithm class for which to create a key/IV pair.</param>
        private static void PasswordToKey(string password, SymmetricAlgorithm sa)
        {
            HashAlgorithm hashAlgo = new MD5CryptoServiceProvider();
            byte[] hash = hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(password));
            sa.BlockSize = hash.Length * 8;
            sa.Key = hash;
            sa.IV = hash;
        }

        // Returns symmetric algorithm with key, IV and salt extracted from 
        //  password string.
        private static SymmetricAlgorithm GetAlgorithm(string password)
        {
            // Init key
            //byte[] saltBytes = Password2Salt(password);
            //Rfc2898DeriveBytes rfc2898 =
            // new Rfc2898DeriveBytes(password, saltBytes);

            // Init algorithm
            SymmetricAlgorithm sa = new RijndaelManaged();
            //sa.Key = rfc2898.GetBytes(sa.KeySize / 8);
            //sa.IV = rfc2898.GetBytes(sa.BlockSize / 8);
            PasswordToKey(password, sa);
            return sa;
        }

        // Encrypts text using password, returns encrypted string
        //  in base64 representation.
        public static String Encrypt(String text, String password)
        {
            SymmetricAlgorithm sa = GetAlgorithm(password);

            // Encrypt text
            ICryptoTransform ict = sa.CreateEncryptor();
            using (MemoryStream mso = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(mso, ict,
                                                      CryptoStreamMode.Write))
                {
                    byte[] textByteArray = Encoding.UTF8.GetBytes(text);
                    cs.Write(textByteArray, 0, textByteArray.Length);
                }
                // Convert encrypted text from bytes to base64 representation
                byte[] encryptedTextBytesArray = mso.ToArray();
                string base64EncryptedText =
                     Convert.ToBase64String(encryptedTextBytesArray);
                return base64EncryptedText;
            }
        }

        // Decrypts text in base64 representation (thats what Encrypt() returns) 
        // using password, returns decrypted string. Throws CryptographicException 
        // in case of bad password, input text or other errors.
        public static String Decrypt(String base64EncryptedText, String password)
        {
            SymmetricAlgorithm sa = GetAlgorithm(password);

            // Decrypt text
            ICryptoTransform ict = sa.CreateDecryptor();
            using (MemoryStream mso = new MemoryStream())
            {
                using (CryptoStream sc = new CryptoStream(mso, ict,
                                              CryptoStreamMode.Write))
                {
                    byte[] base64EncryptedTextByteArray =
                             Convert.FromBase64String(base64EncryptedText);

                    sc.Write(base64EncryptedTextByteArray, 0,
                             base64EncryptedTextByteArray.Length);
                }
                // Return decrypted string
                byte[] data = mso.ToArray();
                return Encoding.UTF8.GetString(data, 0, data.Length);
            }
        }
    }

}
