using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_Model
{
    /// <summary>
    /// An RSS Feed
    /// </summary>
    public class FeedModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public List<ItemModel> Items { get; set; }
    }
}
