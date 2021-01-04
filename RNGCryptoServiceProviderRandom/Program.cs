using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RNGCryptoServiceProviderRandom
{
    class Program
    {
        // stop watch instance
        static Stopwatch sw = new Stopwatch();

        // byte array of 1048576 indexes
        static byte[] data = new byte[1048576];

        // integer number 
        static int number = 0;

        // string builder instnace
        static StringBuilder sb = new StringBuilder();

        // path where i am storing the csv file with data
        private static string strPath =
            @"C:\Users\\mathi\source\repos\RNGCryptoServiceProviderRandom\RNGCryptoServiceProviderRandom\excel\randomNumber.csv";

        // seperator to be used in file
        private static string strSeperator = ",";

        // method to generate 10 random numbers with the use of random
        public static string RandomNumberWithRandom()
        {
            Random rand = new Random();


            for (int i = 0; i < 10; i++)
            {
                sw.Start();
                rand.NextBytes(data);

                number = BitConverter.ToInt32(data, 0);

                sw.Stop();

                sb.AppendLine("random num :" + number + strSeperator + "time :" + sw.Elapsed + strSeperator + "\n");

                sw.Reset();
            }

            return sb.ToString();
        }

        // method to generate 10 random numbers with the use of RNGCryptoServiceProvider
        public static string RandomNumberWithRNGCryptoService()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    sw.Start();
                    rng.GetBytes(data);

                    number = BitConverter.ToInt32(data, 0);
                    sw.Stop();
                    sb.AppendLine("RNGCrypto :" + number + strSeperator + "time :" + sw.Elapsed + strSeperator + "\n");
                    sw.Reset();
                }
            }

            return sb.ToString();
        }

        // method to write result set to excel file
        public static void WriteToExcelFile()
        {
            string randomNumber = RandomNumberWithRandom();
            string rngCrypto = RandomNumberWithRNGCryptoService();
            File.WriteAllText(strPath, randomNumber);
            File.WriteAllText(strPath, rngCrypto);
        }

        static void Main(string[] args)
        {
            // call write to excel file
            WriteToExcelFile();

            Console.Read();
        }
    }
}