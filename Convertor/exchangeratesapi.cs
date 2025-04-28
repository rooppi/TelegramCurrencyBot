using Newtonsoft.Json;

namespace Convertor;
// TODO: в текущем варианте базовая валюта "USD" ("base": "USD") , change to UAH
public class CurrencyConverter
{
    private readonly string _apiKey = "b0dde1c24dd949b4a150ff5ad71fc3a1";
    private readonly string _baseUrl = "https://openexchangerates.org/api"; 
    private readonly HttpClient _httpClient;
    
    public CurrencyConverter() 
    { 
        _httpClient = new HttpClient();
    }
    
    public async Task<decimal> ConvertFromUahToCurrencyAsync(string targetCurrency)
    {
        string url = $"{_baseUrl}/latest.json?app_id={_apiKey}";
           
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);
            var data = JsonConvert.DeserializeObject<ExchangeRatesResponse>(json);


            decimal usdPerUah = 1 / data.Rates["UAH"];
            decimal targetPerUSd = 1 / data.Rates[targetCurrency];
               
            return (1 / usdPerUah) / targetPerUSd;
        }
        throw new Exception("Не вийшло отримати данні о курсах валют");
    }
}

public class ExchangeRatesResponse
{
    [JsonProperty("disclaimer")]
    public string Disclaimer { get; set; }
    
    [JsonProperty("license")]
    public string License { get; set; }
    
    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }
    
    [JsonProperty("base")]
    public string Base { get; set; }
    
    [JsonProperty("rates")]
    public Dictionary<string, decimal> Rates { get; set; }
}