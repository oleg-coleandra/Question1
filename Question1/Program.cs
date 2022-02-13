using System;

namespace Question1
{
    class Program
    {
        static void Main(string[] args)
        {
            var test_string = "This is a test string";
            var codec = new Codec();


            if (String.Compare(test_string, codec.Decode(codec.Encode(test_string))) == 0)
            {
                Console.WriteLine("Test succeeded");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
        }
    }
}