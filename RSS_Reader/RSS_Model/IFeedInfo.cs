using System.Collections.Generic;

namespace RSS_Model
{
    public interface IFeedInfo
    {
        List<ItemModel> FeedList { get; set; }
    }
}
