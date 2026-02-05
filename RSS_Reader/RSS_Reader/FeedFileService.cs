using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RSS_Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RSS_Reader
{
    public class FeedFileService
    {
        public IFeedInfo _booksFeedInfo;
        public IFeedInfo _foodFeedInfo;

        public FeedFileService(IFeedInfo booksFeedInfo, IFeedInfo foodFeedInfo)
        {
            _booksFeedInfo = booksFeedInfo;
            _foodFeedInfo = foodFeedInfo;
        }

        public async Task ReadFeed(string feedName)
        {
            Console.Clear();


            if (feedName == "books")
            {

                if (_booksFeedInfo.FeedList == null)
                {

                    await LoadFile(feedName);
                }
                else
                {
                    _booksFeedInfo.FeedList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                    Console.Write($"\r\nItems Listed above were loaded from {feedName} list, press any key to continue.");
                }
            }
            else
            {
                if (_foodFeedInfo.FeedList == null)
                {
                    await LoadFile(feedName);
                }
                else
                {
                    _foodFeedInfo.FeedList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                    Console.Write($"\r\nItems Listed above were loaded from {feedName} list, press any key to continue.");
                }
            }
            Console.ReadKey();
        }

        public async Task LoadFile(string feedName)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var file = basePath + $"{feedName}.json";
            await Task.Run(() =>
            {
                try
                {
                    using (StreamReader sr = File.OpenText(file))
                    using (JsonTextReader reader = new JsonTextReader(sr))
                    {
                        try
                        {
                            var jsonBooks = (JArray)JToken.ReadFrom(reader);
                            if (feedName == "books")
                            {
                                _booksFeedInfo.FeedList = jsonBooks.ToObject<List<ItemModel>>();
                                _booksFeedInfo.FeedList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                            }
                            else
                            {
                                _foodFeedInfo.FeedList = jsonBooks.ToObject<List<ItemModel>>();
                                _foodFeedInfo.FeedList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                            }

                            Console.Write($"\r\nItems Listed above were loaded from {feedName} json file, press any key to continue.");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"No results found for {feedName}. Press a key to continue.");
                        }
                    }
                }
                catch (Exception)
                {
                    File.Create(file).Dispose();
                    Console.WriteLine($"No results found for {feedName}. Press a key to continue.");
                }
            });
        }
    }
}
