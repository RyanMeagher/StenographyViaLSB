using System;
using System.Collections.Generic;
using System.Linq;

namespace P1_1
{
    public class HideBytes
    {
        public static List<string> Run(string[] hideBytes, IEnumerable<string> bmpHeader, IEnumerable<string> bmpImg)
        {
            //initialize count, we will be stepping 4 bytes at a time because in order to store 1 byte of information
            // we need 4 bytes in the bitmap image
            int count = 0;
            
            //initialize List to append bytes to after they have been modified to store our hidden bytes
            List<string> hiddenImg = bmpHeader.ToList();

            // Step through each byte and using bitwise & and XOR operations save 2 bits of information in the
            // least significant bits of each Byte of the BitmapImage
            for (var i = 0; i < hideBytes.Length; i++)
            {
                int n = Convert.ToByte(hideBytes[i], 16);
                //moves bits XX000000 -> 000000XX
                int n1 = (n & 192) / 64;
                //moves bits 00XX0000 -> 000000XX
                int n2 = (n & 48) / 16;
                //moves bits 0000XX00 ->000000XX
                int n3 = (n & 12) / 4;
                //makes 000000XX
                int n4 = n & 3;

                // use XOR to hide 2 bits of the byte. To hide 1 byte of information we need to use 4 bytes in bitmap file
                int b1 = Convert.ToByte(bmpImg.ElementAt(count), 16) ^ n1;
                int b2 = Convert.ToByte(bmpImg.ElementAt(count + 1), 16) ^ n2;
                int b3 = Convert.ToByte(bmpImg.ElementAt(count + 2), 16) ^ n3;
                int b4 = Convert.ToByte(bmpImg.ElementAt(count + 3), 16) ^ n4;

                count += 4;

                //adding each byte to our list that was initialized with the bitmapHeader 
                hiddenImg.Add(b1.ToString("X2"));
                hiddenImg.Add(b2.ToString("X2"));
                hiddenImg.Add(b3.ToString("X2"));
                hiddenImg.Add(b4.ToString("X2"));
            }

            return hiddenImg;
        }
    }
}