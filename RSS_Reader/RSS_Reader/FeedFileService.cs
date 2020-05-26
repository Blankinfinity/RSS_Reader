using RSS_Model;
using System;
using System.Threading.Tasks;

namespace RSS_Reader
{
    public class FeedFileService
    {
        public IFeedInfo _feedInfo;

        public FeedFileService(IFeedInfo feedInfo)
        {
            _feedInfo = feedInfo;
        }

        public async Task ReadFromFile(string feedName)
        {
            if (feedName == "books")
            {
                if (_feedInfo.FeedsList == null)
                {
                    Console.WriteLine("No feed data.");
                }
                else
                {
                    _feedInfo.FeedsList[0].ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                }
            }
            else
            {
                if (_feedInfo.FeedsList == null)
                {
                    Console.WriteLine("No feed data.");
                }
                else
                {
                    _feedInfo.FeedsList[1].ForEach(item => Console.WriteLine($"Title: {item.Title}\r\nDescription: {item.Description}\r\nLink: {item.Link}\r\nPubDate: {item.PubDate}\r\n", item));
                }
            }
        }
    }
}
