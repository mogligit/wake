using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace wake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("wake by mogli");

            // MAC regex
            Regex macExpression = new Regex("[0-9|a-f|A-F]{2}:[0-9|a-f|A-F]{2}:[0-9|a-f|A-F]{2}:[0-9|a-f|A-F]{2}:[0-9|a-f|A-F]{2}:[0-9|a-f|A-F]{2}");

            if (args.Length < 2)
            {
                PrintUsage();
                return;
            }
            
            if (macExpression.Match(args[0]).Value != args[0])
            {
                Console.WriteLine("MAC address is not valid!");
                Console.WriteLine("Valid format (uppercase or lowercase): a1:b2:c3:d4:e5:f6");
                return;
            }

            string mac = args[0];
            string ip = args[1];

            Console.WriteLine("MAC -> {0} | Address -> {1}", mac, ip);




            byte[] magic = new byte[102];
            byte[] macBytes = new byte[6];
            string[] macStrings = mac.Split(":");

            // converts MAC address into bytes
            for (int i = 0; i <= 5; i++)
            {
                macBytes[i] = Convert.ToByte(macStrings[i], 16);
            }

            // Iterates through array and fills first 6 bytes with FF
            // The rest is the MAC repeated 16 times
            for (int i = 0; i <= 5; i++)
            {
                magic[i] = 255;
                for (int j = 1; j <= 16; j++)
                {
                    magic[(6 * j) + i] = macBytes[i];
                }
            }

            //Sends
            if (SendData(ip, magic))
            {
                Console.WriteLine("Magic packet sent.");
            }
        }
        static void PrintUsage()
        {
            Console.WriteLine("USAGE: wake <mac> <address>");
        }
        static bool SendData(string ip, byte[] data)
        {
            using (UdpClient client = new UdpClient(9))
            {
                try
                {
                    client.Connect(ip, 9);
                    client.Send(data, 102);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: {0}", e.ToString());
                    return false;
                }
            }
        }
    }
}
