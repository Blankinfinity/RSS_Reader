using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_Model
{
    public interface IFeedInfo
    {
        List<List<ItemModel>> FeedsList { get; set; }
    }
}
