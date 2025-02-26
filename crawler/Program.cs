using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        string sitemapUrls = "https://www.fcomputer.dk/sitemap/dk/sitemap.products1.xml";
        string outputFilePath = "sitemap_items.txt";

        try
        {
            HttpClient client = new HttpClient();
            string xmlContent = await client.GetStringAsync(sitemapUrls);

            var urls = XDocument.Parse(xmlContent).Descendants("urlset");
            foreach (var url in urls)
            {
                Console.WriteLine($"URL: {url.Value}");
            }

            Console.WriteLine("All items have been written to " + outputFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}