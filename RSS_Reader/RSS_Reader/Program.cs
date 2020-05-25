using RSS_Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace RSS_Reader
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await LoadMenu();
            }
            
        }

        public static async Task<bool> LoadMenu()
        {
            var showMenu = await Task.Run(async () =>
             {
                 Console.Clear();
                 Console.WriteLine("1. Download RSS feeds. ");
                 Console.WriteLine("2. Read RSS Books feed. ");
                 Console.WriteLine("3. Read RSS Food feed.");
                 Console.WriteLine("4. Exit");
                 Console.Write("\r\nPlease select an option below by entering a number: ");

                 int selectedValue;
                 if (int.TryParse(Console.ReadLine(), out selectedValue))
                 {
                     switch (selectedValue)
                     {
                         case 1:
                             await RssService.LoadFeeds();
                             return true;
                         case 2:

                             return true;
                         case 3:
                             return true;
                         case 4:
                             return false;
                         default:
                             return true;
                     }
                 }
                 else
                 {
                     Console.Clear();
                     Console.WriteLine("\r\nInvalid selection, press any key to return to menu.");
                     Console.ReadKey();
                     return true;
                 }
             });
            return showMenu;
        }
    }
}
