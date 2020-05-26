﻿using RSS_Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace RSS_Reader
{
    class Program
    {
        private static IFeedInfo feedInfo = new FeedInfo();
        private static RssService rssService;
        private static FeedFileService feedFileService;

        public static async Task Main(string[] args)
        {

            rssService = new RssService(feedInfo);
            feedFileService = new FeedFileService(feedInfo);
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
                             await rssService.DownloadFeeds();                             
                             return true;
                         case 2:
                             await feedFileService.ReadFromFile("books");
                             Console.ReadKey();
                             return true;
                         case 3:
                             await feedFileService.ReadFromFile("food");
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
