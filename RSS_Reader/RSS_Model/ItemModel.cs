using System;

namespace RSS_Model
{
    /// <summary>
    /// Model for RSS Item
    /// </summary>
    public class ItemModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PubDate { get; set; }
    }
}