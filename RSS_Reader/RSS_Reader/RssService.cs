using RSS_Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace RSS_Reader
{
    public class RssService
    {
        private static HttpClient _client;
        public IFeedInfo _feedInfo;

        public RssService(IFeedInfo feedInfo)
        {
            _client = new HttpClient();
            _feedInfo = feedInfo;
        }

        public async Task DownloadFeeds()
        {
            _feedInfo.FeedsList = new List<List<ItemModel>>();
            _feedInfo.FeedsList.Add(await LoadFeed(ConfigurationManager.AppSettings["books"]));
            _feedInfo.FeedsList.Add(await LoadFeed(ConfigurationManager.AppSettings["food"]));
         }

        public async Task<List<ItemModel>> LoadFeed(string url)
        {
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
            }
            catch (WebException)
            {
                //
            }

            itemsList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
            Console.Write($"\r\nItems Listed above were loaded from {url}, press any key to continue.");
            Console.ReadKey();
            return itemsList;
        }
    }
}
