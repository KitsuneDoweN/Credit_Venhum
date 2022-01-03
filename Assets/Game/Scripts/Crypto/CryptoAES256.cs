using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;


public static class CryptoAES256
{







    public static string encrypt(string plaintText)
    {
        RijndaelManaged aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Key.SECRET_KEY;
        aes.IV = Key.KEY_IV;

        var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] xBuff = null;
        using(var ms = new MemoryStream())
        {
            using(var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Encoding.UTF8.GetBytes(plaintText);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        string strOutPut = Convert.ToBase64String(xBuff);
        return strOutPut;
    }


    public static string decrypt(string combinedString)
    {

            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Key.SECRET_KEY;
            aes.IV = Key.KEY_IV;

        var decrypt = aes.CreateDecryptor();
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Convert.FromBase64String(combinedString);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        string Output = Encoding.UTF8.GetString(xBuff);
        return Output;

    }
}