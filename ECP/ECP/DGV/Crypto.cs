using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




namespace ECP.DGV
{

    public class Crypto
    {
        /// <summary>
        /// Кодирование сообщение
        /// </summary>
        /// <param name="s">Исходное сообщение</param>
        /// <returns>Шифрованое сообщение</returns>
        public string Encrypt(string s)
        {
            string result = "";
            int L, m, N, k, A, B, p;
            A = 0;
            B = 1;
            N = 256;
            for (int i = 0; i < s.Length; i++)
            {
                p = i + 1;
                m = ((int)s[i]);
                k = A * p + B;
                L = (m + k) % N;
                result += ((char)L);
            }
            return result;

        }

        /// <summary>
        /// Декодирование сообщения
        /// </summary>
        /// <param name="result">шифрованное сообщение</param>
        /// <returns>исходное сообщение</returns>
        public string Decrypt(string result)
        {
            string s = "";
            int L, m, N, k, A, B, p, shift;
            A = 0;
            B = 1;
            N = 256;
            for (int i = 0; i < result.Length; i++)
            {
                p = i + 1;
                L = ((int)result[i]);
                k = A * p + B;
                shift = L - k;
                if (shift < 0)
                {
                    shift += N;
                }
                m = shift % N;
                s += (char)(m);
            }
            return s;


        }

    }
}