using HtmlAgilityPack;

class Program
{
    static async Task Main(string[] args)
    {
        var baseUrl = "https://www.fcomputer.dk/computer/baerbar/acer?page=";
        var pageNumber = 1;
        var allProductUrls = new List<string>();

        while (true)
        {
            var url = baseUrl + pageNumber;
            Console.WriteLine($"Fetching page {pageNumber}...");
            var pageContent = await FetchPageContentAsync(url);
            var productUrls = GetProductUrls(pageContent);

            if (productUrls.Length == 0)
            {
                break;
            }

            allProductUrls.AddRange(productUrls);
            pageNumber++;
        }

        File.WriteAllLines("product_urls.txt", allProductUrls);
        Console.WriteLine("URLs have been written to product_urls.txt");
    }

    static async Task<string> FetchPageContentAsync(string url)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetStringAsync(url);
            return response;
        }
    }

    static string[] GetProductUrls(string htmlContent)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);
        var productUrls = new List<string>();

        for (int i = 1; i <= 24; i++)
        {
            var node = doc.DocumentNode.SelectSingleNode($"//div[contains(@class, 'related-product-list-wrapper')][{i}]//a");

            if (node != null && node.Attributes.Contains("href"))
            {
                productUrls.Add(node.Attributes["href"].Value);
            }
        }

        return productUrls.ToArray();
    }
}