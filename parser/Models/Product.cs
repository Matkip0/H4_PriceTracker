namespace parser.Models;

public class Product
{
    public int Ean { get; private set; }
    public string Name;
    public string Price;

    public Product(int ean, string name, string price)
    {
        Ean = ean;
        Name = name;
        Price = price;
    }
}