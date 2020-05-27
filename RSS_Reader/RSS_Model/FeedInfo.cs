using System.Collections.Generic;

namespace RSS_Model
{
    public class FeedInfo : IFeedInfo
    {
        public List<ItemModel> FeedList { get; set; }
    }
}
