using RSS_Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace RSS_Reader
{
    class Program
    {
        private static List<ItemModel> booksList;
        private static List<ItemModel> foodList;
        private static RssService rssService = new RssService();

        public static async Task Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenu();
            }
            
        }

        public static async Task<bool> MainMenu()
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
                             
                             booksList = await rssService.LoadFeed(ConfigurationManager.AppSettings["books"]);
                             foodList = await rssService.LoadFeed(ConfigurationManager.AppSettings["food"]);
                             return true;
                         case 2:
                             booksList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                             Console.ReadKey();
                             return true;
                         case 3:
                             foodList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                             Console.ReadKey();
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
