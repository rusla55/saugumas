using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rsa
{

    internal class CrypterDecrypter
    {
        public int p;
        public int q;
        public int n;
        public int FIn;
        public int FOut;
        public int e;
        public int d;
        public string encryptor(string text)
        {
            char[] x = text.ToCharArray();
            List<char> cypher = new List<char>();
            n = p * q;
            FIn = (p - 1) * (q - 1);
            Random random = new Random();
            while (true)
            {
                e = random.Next(2, FIn - 1);
                if (coprime(e, FIn) == true)
                    break;
            }
            foreach (char b in x)
            {
                cypher.Add(Convert.ToChar(Convert.ToInt32((b) ^ e) % FIn));
            }
            return new string(cypher.ToArray());
        }
        public string decryptor(string text)
        {
            int localp=0;
            int localq=0;
            char[] x = text.ToCharArray();
            List<char> cypher = new List<char>();
            for (int i = 2; i<100; i++)
                if(prime(i)==true)
                    for (int z = 2; z < 100; z++)
                        if(prime(z)==true)
                            if(z*i==n)
                            {
                                localp = i;
                                localq = z;
                            }
            FOut = (localp-1)*(localq-1);
            for (int i = 2; i < 100; i++)
                if ((e * i) % FIn == 1 && i != e)
                {
                    d = i;
                    break;
                }
            foreach (char b in x)
            {
                cypher.Add(Convert.ToChar(Convert.ToInt32((b) ^ e) % FIn));
            }
            return new string(cypher.ToArray());
        }
        // Recursive function to
        // return gcd of a and b
        static int __gcd(int a, int b)
        {
            // Everything divides 0
            if (a == 0 || b == 0)
                return 0;
            // base case
            if (a == b)
                return a;

            // a is greater
            if (a > b)
                return __gcd(a - b, b);

            return __gcd(a, b - a);
        }

        // function to check and print if
        // two numbers are co-prime or not
        static bool coprime(int a, int b)
        {

            if (__gcd(a, b) == 1)
                return true;
            else
                return false;
        }

        static bool prime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
