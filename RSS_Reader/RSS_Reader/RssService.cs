using Newtonsoft.Json;
using RSS_Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace RSS_Reader
{
    public class RssService
    {
        private static HttpClient _client;
        public IFeedInfo _booksFeedInfo;
        public IFeedInfo _foodFeedInfo;

        public RssService(IFeedInfo booksFeedInfo, IFeedInfo foodFeedInfo)
        {
            _client = new HttpClient();
            _booksFeedInfo = booksFeedInfo;
            _foodFeedInfo = foodFeedInfo;
        }

        public async Task DownloadFeeds()
        {
            _booksFeedInfo.FeedList = new List<ItemModel>();
            _booksFeedInfo.FeedList = await LoadFeed(ConfigurationManager.AppSettings["books"]);
            SaveJsonFile(_booksFeedInfo.FeedList, "books");
            _foodFeedInfo.FeedList = await LoadFeed(ConfigurationManager.AppSettings["food"]);
            SaveJsonFile(_foodFeedInfo.FeedList, "food");
        }

        public async Task<List<ItemModel>> LoadFeed(string url)
        {
            Console.Clear();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            List<ItemModel> itemsList = new List<ItemModel>();

            try
            {
                var feedResult = await _client.GetStringAsync(url);

                if (feedResult != null)
                {
                    var rssDoc = new XmlDocument();

                    // Technically you could just load the url but i prefer to use HttpClient for my web requests so here we use LoadXml instead.
                    rssDoc.LoadXml(feedResult);

                    XmlNodeList rssNodes = rssDoc.SelectNodes("rss/channel/item");

                    foreach (XmlNode rssNode in rssNodes)
                    {
                        var item = new ItemModel();
                        XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                        item.Title = rssSubNode != null ? rssSubNode.InnerText : "";

                        rssSubNode = rssNode.SelectSingleNode("link");
                        item.Link = rssSubNode != null ? rssSubNode.InnerText : "";

                        rssSubNode = rssNode.SelectSingleNode("description");
                        item.Description = rssSubNode != null ? rssSubNode.InnerText : "";

                        rssSubNode = rssNode.SelectSingleNode("pubDate");
                        item.PubDate = DateTime.Parse(rssSubNode.InnerText);

                        itemsList.Add(item);
                    }
                }

                itemsList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                Console.Write($"\r\nItems Listed above were loaded from {url}, press any key to continue.");
            }
            catch (WebException)
            {
                Console.Write($"\r\nSomething went wrong loading from {url}, press any key to continue.");
            }

            
            Console.ReadKey();
            return itemsList;
        }

        public void SaveJsonFile(List<ItemModel> feedItems, string feedType)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;

            var json = JsonConvert.SerializeObject(feedItems);

            File.WriteAllText(path + feedType + ".json", json);
        }
    }
}
