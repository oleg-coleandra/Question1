using Question1.Exceptions;
using System;
using System.Linq;

namespace Question1
{
    public class Codec
    {
        public string Transcode { get; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        
        public Codec()
        {
        }

        private void validateInput(string input)
        {
            if (input.Except(Transcode).Any())
                throw new InvalidCharacterException($"Following characters are not allowed: {String.Join(",", input.Except(Transcode))}");
        }

        public string Encode(string input)
        {
            validateInput(input);
            int l = input.Length;
            int cb = (l / 3 + ((l % 3) != 0 ? 1 : 0)) * 4;

            char[] output = new char[cb];
            for (int i = 0; i < cb; i++)
            {
                output[i] = '=';
            }

            int c = 0;
            int reflex = 0;
            const int s = 0x3f;

            for (int j = 0; j < l; j++)
            {
                reflex <<= 8;
                reflex &= 0x00ffff00;
                reflex += input[j];

                int x = ((j % 3) + 1) * 2;
                int mask = s << x;
                while (mask >= s)
                {
                    int pivot = (reflex & mask) >> x;
                    output[c++] = Transcode[pivot];
                    reflex &= ~mask;
                    mask >>= 6;
                    x -= 6;
                }
            }

            switch (l % 3)
            {
                case 1:
                    reflex <<= 4;
                    output[c++] = Transcode[reflex];
                    break;

                case 2:
                    reflex <<= 2;
                    output[c++] = Transcode[reflex];
                    break;

            }
            Console.WriteLine("{0} --> {1}\n", input, new string(output));
            return new string(output);
        }

        public string Decode(string input)
        {
            int l = input.Length;
            int cb = (l / 4 + ((l % 4) != 0 ? 1 : 0)) * 3 + 1;
            char[] output = new char[cb];
            int c = 0;
            int bits = 0;
            int reflex = 0;
            for (int j = 0; j < l; j++)
            {
                reflex <<= 6;
                bits += 6;
                bool fTerminate = ('=' == input[j]);
                if (!fTerminate)
                    reflex += Transcode.IndexOf(input[j]);

                while (bits >= 8)
                {
                    int mask = 0x000000ff << (bits % 8);
                    output[c++] = (char)((reflex & mask) >> (bits % 8));
                    reflex &= ~mask;
                    bits -= 8;
                }

                if (fTerminate)
                    break;
            }
            Console.WriteLine("{0} --> {1}\n", input, new string(output));
            return new string(output);
        }
    }
}
