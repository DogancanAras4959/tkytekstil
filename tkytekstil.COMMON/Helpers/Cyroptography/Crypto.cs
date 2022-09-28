﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkytekstil.COMMON.Helpers.Cyroptography
{
    public class Crypto
    {
        private const string _key = "AD9847CC-E0D4-4C3B-991C-F3422722ACB2";

        Provider crypto;

        public Crypto()
        {
            crypto = new Provider(_key);
        }

        public string Encrypt(string val)
        {
            return crypto.Encrypt(val);
        }

        public string TryEncrypt(string val)
        {
            try
            {
                return crypto.Encrypt(val);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public string DeCrypt(string val)
        {
            return crypto.Decrypt(val);
        }

        public string TryDecrypt(string val)
        {
            try
            {
                return crypto.Decrypt(val);
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
