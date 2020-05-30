using BusinessEntities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Schema;

namespace dropinion4u.Controllers
{
    public class WebPartsController : Controller
    {
        private WebPartsDataAccess objWebPartsDataAccess;

        public WebPartsController()
        {
            objWebPartsDataAccess = new WebPartsDataAccess();
            
        }
        // GET: WebParts
        public ActionResult _RssFeeds()
        {
            return View();
        }

        public List<RSSFeed> GetRSSFeedList()
        {
            var list = objWebPartsDataAccess.GetRssFeedList().Where(r => r.IsActive == true).Select(l => new { l.Url });

            List<RSSFeed> feeds = new List<RSSFeed>();
            foreach (var item in list)
            {
                feeds.AddRange(GetRSS(item.Url));

            }
            return feeds;
        }

        private List<RSSFeed> GetRSS(string url)
        {
            List<RSSFeed> rssFeeds = new List<RSSFeed>();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                // Set the validation settings.
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                settings.ValidationType = ValidationType.DTD;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                XmlReader reader = XmlReader.Create(url,settings);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                foreach (SyndicationItem feedItem in feed.Items)
                {
                    RSSFeed rssFeed = new RSSFeed();
                    rssFeed.FeedUrl = feedItem.Links[0].GetAbsoluteUri().ToString();
                    rssFeed.FeedTitle = feedItem.Title.Text;
                    rssFeeds.Add(rssFeed);

                }
                return rssFeeds;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return rssFeeds;
        }
        // Display any validation errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation Error: {0}", e.Message);
        }
    }
}