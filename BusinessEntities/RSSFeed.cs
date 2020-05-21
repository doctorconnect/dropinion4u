using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class RSSFeed : Base
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string FeedUrl { get; set; }
        public string FeedTitle { get; set; }
    }
}