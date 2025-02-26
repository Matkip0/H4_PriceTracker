using System.Xml;

class Program
{
    static async Task Main(string[] args)
    {
        var sitemapUrls = new List<string>([
                "https://www.fcomputer.dk/sitemap/dk/sitemap.products1.xml",
                "https://www.fcomputer.dk/sitemap/dk/sitemap.products2.xml",
                "https://www.fcomputer.dk/sitemap/dk/sitemap.products3.xml",
                "https://www.fcomputer.dk/sitemap/dk/sitemap.products4.xml",
        ]);

        try
        {
            HttpClient client = new HttpClient();
            foreach (var sitemapUrl in sitemapUrls)
            {
                string xmlContent = await client.GetStringAsync(sitemapUrl);

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);

                var urls = new List<string>();
                foreach (XmlNode node in xmlDoc.GetElementsByTagName("url"))
                {
                    var loc = node["loc"];
                    if (loc == null) continue;

                    urls.Add(loc.InnerText);
                }

                Console.WriteLine($"Found {urls.Count} product URLs in {sitemapUrl}.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
}
