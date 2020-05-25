using RSS_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RSS_Reader
{
    public static class RssService
    {
        private static HttpClient Client = new HttpClient();

        private static List<ItemModel> booksList = new List<ItemModel>();
        public static async Task LoadFeeds()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var result = await Client.GetStringAsync("https://www.buzzfeed.com/books.xml");

            if (result != null)
            {
                var rssDoc = new XmlDocument();
                // Technically you could just load the url but i prefer to use HttpClient for my web requests so here we use LoadXml instead.
                rssDoc.LoadXml(result);

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

                    booksList.Add(item);
                }

                booksList.ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                Console.ReadKey();
            }
        }
    }
}
