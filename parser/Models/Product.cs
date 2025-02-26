namespace parser.Models;

public class Product
{
    public string Ean { get; private set; }
    public string Name;
    public double Price;
    public DateTime LastChecked { get; private set; }

    public Product(string ean, string name, double price, DateTime lastChecked)
    {
        Ean = ean;
        Name = name;
        Price = price;
        LastChecked = lastChecked;
    }
}