using System;
using System.Collections.Generic;
using System.Linq;

namespace P1_1
{
    public class Init
    {
        public static (string[] hideBytes, IEnumerable<string> bmpHeader, IEnumerable<string> bmpImg) Run(
            byte[] bitMapImage, string[] args)
        {
            //convert the bitmap byte array into a string array where each element contains a string representation
            // of a byte in hex form
            string bmpString = BitConverter.ToString(bitMapImage).Replace("-", " ");
            string[] byteArr = bmpString.Split((" "));
            
            var bmpHeader = byteArr.Take(26); //First 26 bytes are Header 
            var bmpImg = byteArr.Skip(26); //48 bytes corresponding to 16 pixels RBG

            // cmd line arg given of bytes as well as idx to iterate every 4 bytes of the 
            var hideBytes = args[0].Split(" ");

            return (hideBytes, bmpHeader, bmpImg);
        }
    }
}