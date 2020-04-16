using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicTests();
            Console.WriteLine("Hello World!");
        }

        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            //your code here...
            return "";
        }

        public static void BasicTests()
        {
            Assert.AreEqual("ABCDAB", UniqueInOrder("AAAABBBCCDAABBB"));
        }
    }
}
