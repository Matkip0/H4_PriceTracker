using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


public class Product
{
    private static readonly JsonSerializerSettings SERIALIZER_SETTINGS = new JsonSerializerSettings
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        }
    };

    public string Url { get; }
    public long FetchedAt { get; }

    public Product(string url)
    {
        this.Url = url;
        this.FetchedAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, SERIALIZER_SETTINGS);
    }
}
