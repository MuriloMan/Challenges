using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Solution
{
    public class OpenClosePrices
    {

        public static void Main(string firstDate, string lastDate, string weekDay)
        {
            string url2 = "<cantShowApiAddress>__pageNumber__";
            var datestart = DateTime.ParseExact(firstDate, "d-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var dateend = DateTime.ParseExact(lastDate, "d-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

            List<ldata> all = new List<ldata>();
            List<ldata> f = new List<ldata>();
            var n = 1;
            var stop = false;
            do
            {
                pdata stocks = JsonConvert.DeserializeObject<pdata>(GetList(url2.Replace("__pageNumber__", n.ToString())));
                if (stocks.data.Count == 0)
                {
                    stop = true;
                }
                else
                {
                    stocks.data.ForEach(s =>
                    {
                        if ((s.df >= datestart && s.df <= dateend) && (s.df.DayOfWeek.ToString() == weekDay))
                        {
                            f.Add(s);
                        }
                    });
                    all = all.Concat(f).ToList();
                }
                f.Clear();
                n++;
            } while (!stop);


            all.ForEach(x => { Console.WriteLine($"{x.date} {x.open} {x.close}"); });
        }
        private static string GetList(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse wbr = (HttpWebResponse)webrequest.GetResponse();
            StreamReader responseStream = new StreamReader(wbr.GetResponseStream());
            string result = responseStream.ReadToEnd();
            wbr.Close();
            return result;
        }
        public class pdata
        {
            public int page { get; set; }
            public int per_page { get; set; }
            public int total { get; set; }
            public int total_pages { get; set; }
            public List<ldata> data { get; set; }
        }

        public class ldata
        {
            public string date { get; set; }
            public DateTime df
            {
                get
                {
                    return DateTime.ParseExact(this.date, "d-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture); ;
                }
            }
            public float open { get; set; }
            public float close { get; set; }
            public float high { get; set; }
            public float low { get; set; }

        }
    }
}
