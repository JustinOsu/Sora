﻿#region LICENSE
/*
    Sora - A Modular Bancho written in C#
    Copyright (C) 2019 Robin A. P.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace Shared.Helpers
{
    public static class Crypto
    {
        public static string RandomString(int n)
        {
            StringBuilder ret   = new StringBuilder();
            const string  ascii = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/=";

            for (int i = 0; i < n; i++)
                ret.Append(ascii[new Random().Next(0, ascii.Length)]);

            return ret.ToString();
        }

        public static string EncryptString(string message, byte[] key, ref string iv)
        {
            byte[] rawMessage = Encoding.ASCII.GetBytes(message);

            byte[] newiv = iv == null ? Encoding.ASCII.GetBytes(RandomString(32)) : Convert.FromBase64String(iv);

            RijndaelEngine            engine         = new RijndaelEngine(256);
            CbcBlockCipher            blockCipher    = new CbcBlockCipher(engine);
            PaddedBufferedBlockCipher cipher         = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding());
            KeyParameter              keyParam       = new KeyParameter(key);
            ParametersWithIV          keyParamWithIv = new ParametersWithIV(keyParam, newiv, 0, 32);

            cipher.Init(true, keyParamWithIv);
            byte[] comparisonBytes = new byte[cipher.GetOutputSize(rawMessage.Length)];
            int length          = cipher.ProcessBytes(rawMessage, comparisonBytes, 0);
            cipher.DoFinal(comparisonBytes, length);

            iv = Convert.ToBase64String(newiv);

            return Convert.ToBase64String(comparisonBytes);
        }

        public static string DecryptString(byte[] message, byte[] key, byte[] iv)
        {            
            RijndaelEngine            engine         = new RijndaelEngine(256);
            CbcBlockCipher            blockCipher    = new CbcBlockCipher(engine);
            PaddedBufferedBlockCipher cipher         = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding());
            KeyParameter              keyParam       = new KeyParameter(key);
            ParametersWithIV          keyParamWithIv = new ParametersWithIV(keyParam, iv, 0, 32);
            
            cipher.Init(false, keyParamWithIv);
            byte[] comparisonBytes = new byte[cipher.GetOutputSize(message.Length)];
            int    length          = cipher.ProcessBytes(message, comparisonBytes, 0);
            cipher.DoFinal(comparisonBytes, length);

            return Encoding.UTF8.GetString(comparisonBytes);
        }

        public static byte[] GetMd5(byte[] data)
        {
            using (MD5 md5 = MD5.Create())
                return md5.ComputeHash(data);
        }

        public static byte[] GetMd5(Stream data)
        {
            using (MD5 md5 = MD5.Create())
                return md5.ComputeHash(data);
        }

        public static byte[] GetMd5(string data) => GetMd5(Encoding.UTF8.GetBytes(data));
    }
}