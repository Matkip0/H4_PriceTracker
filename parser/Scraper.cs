using parser.Models;
using HtmlAgilityPack;

namespace parser;

public class Scraper
{
    public void GetProductDetails(string html)
    {
        var website = new HtmlWeb();
        var htmlDoc = website.Load(html);

        var name = htmlDoc.DocumentNode.QuerySelector("div .top-product-data h1").InnerHtml;
        var price = htmlDoc.DocumentNode.QuerySelector("div .product-price").InnerText;
        var details = htmlDoc.DocumentNode.QuerySelector("div .product-essentials div").ChildNodes;

        var ean = details[0];
        foreach (var line in details)
        {
            if (!line.InnerHtml.Contains("EAN:")) continue;
            ean = line;
            break;
        }

        Product product = CleanProduct(name, price, ean.InnerText);
        // Console.WriteLine($"Name: {product.Name} \nPrice: {product.Price} \nEAN: {product.Ean}");
        // Console.WriteLine($"Unclean Name: {name} \nUnclean Price: {price} \nUnclean EAN: {ean}"); 
    }

    /// <summary>
    /// Parses and Trims product properties.
    /// </summary>
    /// <returns>Parsed Product</returns>
    private Product CleanProduct(string name, string price, string ean)
    {
        // Removes whitespaces and undesired characters (Price's ",-")
        var parsedName = name.Trim();
        var parsedEan = ean.Trim().Split(" ")[1];
        price = price.Split('D')[0].Trim().Replace(",-", "");

        if (!double.TryParse(price, out var parsedPrice))
        {
            Console.WriteLine("Parsing failed. - Price type = " + price.GetType());
        }

        var product = new Product(parsedEan, parsedName, parsedPrice, DateTime.Now);
        return product;
    }
}