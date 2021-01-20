using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetectionOci
{
    class Program
    {
        static void Main(string[] args)
        {
            FilterMossResults();
            //RenameFiles.Rename("C:\\Users\\Rafael Fernanadez\\Documents\\OCI\\Envios x Usuario\\Day 0");
        }

        public static void FilterMossResults()
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("Dia 1 - Dragones C++", "http://moss.stanford.edu/results/101320485");
            //dict.Add("Dia 1 - Polrect C++", "http://moss.stanford.edu/results/600408467");
            //dict.Add("Dia 1 - Secnum C++", "http://moss.stanford.edu/results/447692212");
            //dict.Add("Dia 1 - Dragones Python", "http://moss.stanford.edu/results/750119884");
            //dict.Add("Dia 1 - Polrect Python", "http://moss.stanford.edu/results/902792022");
            //dict.Add("Dia 1 - Secnum Python", "http://moss.stanford.edu/results/924786109");

            //dict.Add("Dia 2 - Constru C++", "http://moss.stanford.edu/results/535315625");
            //dict.Add("Dia 2 - Constru Python", "http://moss.stanford.edu/results/41497681");
            //dict.Add("Dia 2 - Graforueda C++", "http://moss.stanford.edu/results/450943234");
            //dict.Add("Dia 2 - Graforueda Python", "http://moss.stanford.edu/results/825116657");
            //dict.Add("Dia 2 - Rayos C++", " http://moss.stanford.edu/results/491776790");
            //dict.Add("Dia 2 - Rayos Python", "http://moss.stanford.edu/results/357292716");

            //dict.Add("All Python", "http://moss.stanford.edu/results/929038210");
            //dict.Add("SC C++", "http://moss.stanford.edu/results/147943323");
            //dict.Add("AR C++", "http://moss.stanford.edu/results/880024600");
            //dict.Add("CF C++", "http://moss.stanford.edu/results/187433593");
            //dict.Add("CM C++", "http://moss.stanford.edu/results/936783688");
            dict.Add("CA C++", "http://moss.stanford.edu/results/510480652");
            dict.Add("GR C++", "http://moss.stanford.edu/results/541251241");
            dict.Add("GT C++", "http://moss.stanford.edu/results/122887578");
            dict.Add("HO C++", "http://moss.stanford.edu/results/789389761");
            dict.Add("IJ C++", "http://moss.stanford.edu/results/688645168");
            dict.Add("LT C++", "http://moss.stanford.edu/results/303452217");
            dict.Add("LH C++", "http://moss.stanford.edu/results/414864492");
            dict.Add("MT C++", "http://moss.stanford.edu/results/493077427");
            dict.Add("MY C++", "http://moss.stanford.edu/results/446921545");
            dict.Add("PR C++", "http://moss.stanford.edu/results/397954720");
            dict.Add("SS C++", "http://moss.stanford.edu/results/114410634");
            dict.Add("VC C++", "http://moss.stanford.edu/results/194641501");

            foreach (KeyValuePair<string, string> valuePair in dict)
            {
                Console.WriteLine(valuePair.Key);
                string html = FecthMossResults(valuePair.Value);
                string results = RemoveNonFraud(html);
                SaveResults(valuePair.Key, results);
            }
        }

        static string FecthMossResults(string url)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(url).GetAwaiter().GetResult();
        }

        static string RemoveNonFraud(string htmlContent)
        {
            HtmlDocument doc = new HtmlDocument();

            // Load the html from a string
            doc.LoadHtml(htmlContent);

            var tr = doc.DocumentNode.SelectSingleNode("//table");
            tr = tr.FirstChild;
            tr = tr.NextSibling;
            var trNext = tr.NextSibling;
            try
            {
                do
                {
                    tr = trNext;

                    //HtmlNodeCollection childs = tr.ChildNodes;
                    //var td1 = childs[0];
                    //var td2 = childs[1];
                    //var td3 = childs[2];

                    //string txt1 = td1.InnerHtml,
                    //       txt2 = td2.InnerHtml;

                    //int a1 = txt1.IndexOf("_"), a2 = txt1.LastIndexOf("_");
                    //int b1 = txt2.IndexOf("_"), b2 = txt2.LastIndexOf("_");

                    //trNext = tr.NextSibling;

                    //if (txt1.Substring(a1, a2 - a1) == txt2.Substring(b1, b2 - b1))
                    //{
                    //    tr.RemoveAll();
                    //    tr.Remove();
                    //}
                    //else
                    //{
                    //    string prov1 = txt1.Substring(a1 + 1, 2);
                    //    string prov2 = txt1.Substring(b1 + 1, 2);

                    //    a1 = txt1.IndexOf("(") + 1;
                    //    a2 = txt1.LastIndexOf("%");
                    //    int p1 = int.Parse(txt1.Substring(a1, a2 - a1));

                    //    if (p1 > 40)
                    //    {
                    //        td1.InnerHtml = (prov1 == prov2 ? "!!!!! " : "") + ">>> " + td1.InnerHtml + " <<<";
                    //    }

                    //    b1 = txt2.IndexOf("(") + 1;
                    //    b2 = txt2.LastIndexOf("%");
                    //    int p2 = int.Parse(txt2.Substring(b1, b2 - b1));

                    //    if (p2 > 40)
                    //    {
                    //        td2.InnerHtml = (prov1 == prov2 ? "!!!!! " : "") + ">>> " + td2.InnerHtml + " <<<";
                    //    }
                    //}

                    HtmlNodeCollection childs = tr.ChildNodes;
                    var td1 = childs[0];
                    var td2 = childs[1];
                    var td3 = childs[2];

                    string txt1 = td1.InnerHtml,
                           txt2 = td2.InnerHtml;

                    int a1 = txt1.IndexOf("_"), a2 = txt1.IndexOf("_", a1 + 5);
                    int a3 = txt1.IndexOf("_", a2), a4 = txt1.LastIndexOf("_");
                    int b1 = txt2.IndexOf("_"), b2 = txt2.IndexOf("_", b1 + 5);
                    int b3 = txt2.IndexOf("_", 11), b4 = txt2.LastIndexOf("_");

                    trNext = tr.NextSibling;
                    //              Mismo Usuario                                                Diferente Problema
                    if (txt1.Substring(a1, a2 - a1) != txt2.Substring(b1, b2 - b1) || txt1.Substring(a2, a4 - a2) == txt2.Substring(b2, b4 - a2))
                    {
                        tr.RemoveAll();
                        tr.Remove();
                    }
                    else
                    {
                        string prov1 = txt1.Substring(a1 + 1, 2);
                        string prov2 = txt1.Substring(b1 + 1, 2);

                        a1 = txt1.IndexOf("(") + 1;
                        a2 = txt1.LastIndexOf("%");
                        int p1 = int.Parse(txt1.Substring(a1, a2 - a1));

                        if (p1 > 40)
                        {
                            td1.InnerHtml = (prov1 == prov2 ? "!!!!! " : "") + ">>> " + td1.InnerHtml + " <<<";
                        }

                        b1 = txt2.IndexOf("(") + 1;
                        b2 = txt2.LastIndexOf("%");
                        int p2 = int.Parse(txt2.Substring(b1, b2 - b1));

                        if (p2 > 40)
                        {
                            td2.InnerHtml = (prov1 == prov2 ? "!!!!! " : "") + ">>> " + td2.InnerHtml + " <<<";
                        }
                    }
                }
                while (trNext != null);
            }
            catch (Exception) { }

            return doc.DocumentNode.OuterHtml;
        }

        static void SaveResults(string file, string content)
        {
            System.IO.File.WriteAllText("C:\\Users\\Rafael Fernanadez\\Documents\\OCI\\Fraud Detection Results\\" + file + ".html", content);
        }
    }
}
