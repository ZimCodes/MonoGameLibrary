﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MonoGameLibrary.Util
{
    static class ListUtil
    {
        /// <summary>
        /// Shuffle Lists of any type
        /// </summary>
        /// <typeparam name="T">The type of contents to shuffle</typeparam>
        /// <param name="list">The List of types</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
