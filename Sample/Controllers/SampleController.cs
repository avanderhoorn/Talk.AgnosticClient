using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Framework;

namespace Sample.Controllers
{
    public partial class SampleController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Metadata()
        { 
            return new JsonpResult(_metadata);
        }

        public virtual ActionResult Currency()
        {
            return Json(_listing, "application/vnd.sample.currency-v1+json", JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Quote(string id)
        {
            var generator = _listingPrice[id];
            return Json(new { Id = id, Buy = generator.GenerateMin(), Sell = generator.GenerateMax() }, "application/vnd.sample.quote-v1+json", JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult News(string id)
        {
            Response.AppendHeader("Cache-Control", "public, max-age=60000");
            return Json(_listingNews[id], "application/vnd.sample.news-v1+json", JsonRequestBehavior.AllowGet);
        }

        #region Support Class/Methdos
        private static Random _random = new Random();

        private static object _metadata = new
            {
                Resources = new
                    {
                        Listing = "/Sample/Currency",
                        News = "/Sample/News{/id}"
                    }
            };

        private static IList<CurrencyPair> _listing = new List<CurrencyPair>
            {
                BuildCurrencyPair("AUD/USD"), 
                BuildCurrencyPair("AUD/GBP"),
                BuildCurrencyPair("AUD/EUR"),
                BuildCurrencyPair("AUD/DKK"),
                BuildCurrencyPair("EUR/USD")
            };
        

        private static IDictionary<string, IList<CurrencyNewsItem>> _listingNews = new Dictionary<string, IList<CurrencyNewsItem>>
            {
                { "AUDUSD", new List<CurrencyNewsItem>
                                 {
                                     BuildCurrencyNews("12412", "FOREX Technical Analysis: AUD/USD Trades Through December High", "DailyFX", "Fri 13:37"),
                                     BuildCurrencyNews("12413", "Aussie Lifts, Yen Slips, Central Banks Are Busy", "CNBC", "Thu 10 Jan"),
                                     BuildCurrencyNews("12414", "Forex: Euro Rallies on Strong Spanish Bond Auction - ECB Ahead", "DailyFX", "Thu 10 Jan")
                                 } }, 
                { "AUDGBP", new List<CurrencyNewsItem>
                                 {
                                     BuildCurrencyNews("3234", "Testing the Waters with Tentative Yen Cross Turn", "DailyFX", "Wed 9 Jan"),
                                     BuildCurrencyNews("3235", "Forex: British Pound to Steady Amid Mixed CPI, BoE Minutes", "DailyFX", "Sat 15 Dec")
                                 } },
                { "AUDEUR", new List<CurrencyNewsItem>
                                 {
                                     BuildCurrencyNews("75742", "FOREX Trading: Just Missed EURUSD Longs, Pending EURAUD Long", "DailyFX", "Fri 01:23"),
                                     BuildCurrencyNews("75743", "Trading Defensively with Holiday Trading Conditions and the Fiscal Cliff Ahead", "DailyFX", "Sat 5 Jan"),
                                     BuildCurrencyNews("75744", "Forex Analysis: EUR/AUD Trading into a Near-term Top", "DailyFX", "Fri 4 Jan")
                                 } },
                { "AUDDKK", new List<CurrencyNewsItem>
                                 {
                                     BuildCurrencyNews("12412", "Forex Strategy: Australian Dollar Sold Against Canadian Counterpart", "DailyFX", "Wed 2 Jan"),
                                     BuildCurrencyNews("12412", "Daily Observations: November 14, 2012", "CNBC", "Sat 17 Nov")
                                 } },
                { "EURUSD", new List<CurrencyNewsItem>
                                 {
                                     BuildCurrencyNews("12412", "Has Draghi Won the Battle With Financial Markets?", "CNBC", "Fri 13:34"),
                                     BuildCurrencyNews("12412", "FOREX Technical Analysis: EUR/USD Rally is Largest Since August", "CNBC", "Fri 13:29"),
                                     BuildCurrencyNews("12412", "How to Trade the Euro's Rise", "CNBC", "Fri 08:18"),
                                     BuildCurrencyNews("12412", "Europe Shares Close Mixed After ECB Decision; Nokia Jumps", "DailyFX", "Fri 03:51")
                                 } }
            };

        private static IDictionary<string, CurrencyQuoteGenerator> _listingPrice = new Dictionary<string, CurrencyQuoteGenerator>
            {
                { "AUDUSD", new CurrencyQuoteGenerator { Min = 103, Max = 110 } },
                { "AUDGBP", new CurrencyQuoteGenerator { Min = 60, Max = 65 } },
                { "AUDEUR", new CurrencyQuoteGenerator { Min = 72, Max = 80 } },
                { "AUDDKK", new CurrencyQuoteGenerator { Min = 595, Max = 600 } },
                { "EURUSD", new CurrencyQuoteGenerator { Min = 130, Max = 139 } }
            };
        public class CurrencyPair
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public IList<CurrencyLinks> Links { get; set; } 
        }

        public class CurrencyNewsItem
        {  
            public string Title { get; set; }
            public string Source { get; set; }
            public string Date { get; set; }
            public IList<CurrencyLinks> Links { get; set; } 
        }

        public class CurrencyLinks
        {
            public string Rel { get; set; }
            public string Uri { get; set; }
        }

        public class CurrencyQuoteGenerator
        {
            public int Min { get; set; } 
            public int Max { get; set; }
            public double GenerateMin()
            {
                return _random.Next(Min - 10, Min) / 100.0;
            }
            public double GenerateMax()
            {
                return _random.Next(Max, Max + 10) / 100.0;
            }
        }

        public static CurrencyPair BuildCurrencyPair(string pair)
        {
            var pairId = pair.Replace("/", "");

            return new CurrencyPair
                {
                    Name = pair,
                    Id = pairId,
                    Links = new List<CurrencyLinks>
                                {
                                    new CurrencyLinks { Rel = "/Rel/Sample/Quote", Uri = "/Sample/Quote/" + pairId }
                                } 
                };
        }

        private static CurrencyNewsItem BuildCurrencyNews(string id, string title, string source, string date)
        {
            return new CurrencyNewsItem
                {
                    Title = title, 
                    Source = source, 
                    Date = date, 
                    Links = new List<CurrencyLinks>
                        {
                            new CurrencyLinks { Rel = "/Rel/Sample/News", Uri = "/Sample/News/" + id }
                        }
                };
        }
        #endregion
    }
}
