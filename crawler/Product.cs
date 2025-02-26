using Newtonsoft.Json;

public class Product
{
    public string Url { get; }
    public long FetchedAt { get; }

    public Product(string url)
    {
        this.Url = url;
        this.FetchedAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
