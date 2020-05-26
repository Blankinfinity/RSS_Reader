using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_Model
{
    public class FeedInfo : IFeedInfo
    {
        public List<List<ItemModel>> FeedsList { get; set; }
    }
}
