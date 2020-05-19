using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dropinion4u.Controllers
{
    public class WebPartsController : Controller
    {
        // GET: WebParts
        public ActionResult _RssFeeds()
        {
            return View();
        }

        //public List<RSSFeed> GetRSSFeedList()
        //{
        //    var list = objDirectoryDataAccess.GetRssFeedList().Where(r => r.IsActive == true).Select(l => new { l.Url });

        //    List<RSSFeed> feeds = new List<RSSFeed>();
        //    foreach (var item in list)
        //    {
        //        feeds.AddRange(GetRSS(item.Url));

        //    }
        //    return feeds;
        //}

        //private List<RSSFeed> GetRSS(string url)
        //{
        //    List<RSSFeed> rssFeeds = new List<RSSFeed>();
        //    try
        //    {

        //        XmlReader reader = XmlReader.Create(url);
        //        // SyndicationFeed feed = SyndicationFeed.Load(reader);
        //        //reader.Close();
        //        //foreach (SyndicationItem feedItem in feed.Items)
        //        //{
        //        //    RSSFeed rssFeed = new RSSFeed();
        //        //    rssFeed.FeedUrl = feedItem.Links[0].GetAbsoluteUri().ToString();
        //        //    rssFeed.FeedTitle = feedItem.Title.Text;
        //        //    rssFeeds.Add(rssFeed);

        //        //}
        //        return rssFeeds;
        //    }
        //    catch (Exception)
        //    {
        //        // throw ex;
        //    }
        //    return rssFeeds;
        //}

    }
}