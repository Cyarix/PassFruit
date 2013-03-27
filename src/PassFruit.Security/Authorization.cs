﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PassFruit.Security.Cryptography;

namespace PassFruit.Security
{
    public class Authorization
    {

        private const string AuthorizedMessage = "AUTHORIZED";

        /// <summary>
        /// A new message is generated by encrypting a hard coded value with the provided Master Key and random 
        /// generated Initialization Vector.
        /// The message is then passed to the hashing function and the key used is the provided Master Key.
        /// </summary>
        public Authorization(MasterKey masterKey, Aes aes, HmacSha256 hmacSha256)
            : this(masterKey, aes.GenerateInitializationVector(), aes, hmacSha256)
        {
            
        }

        /// <summary>
        /// A message is generated by encrypting a hard coded value with the provided Master Key and 
        /// Initialization Vector.
        /// The message is then passed to the hashing function and the key used is the provided Master Key.
        /// </summary>
        public Authorization(MasterKey masterKey, byte[] initializationVector, Aes aes, HmacSha256 hmacSha256)
        {
            InitializationVector = initializationVector;
            var ciphertext = aes.Encrypt(AuthorizedMessage, masterKey.SecretKey, initializationVector);
            Hmac = hmacSha256.Compute(ciphertext, masterKey.SecretKey);
        }

        public byte[] InitializationVector { get; private set; }

        public byte[] Hmac { get; private set; }

    }
}
