using Newtonsoft.Json;

namespace Convertor;

public static class CurrencyConverter
{
    private static readonly string _apiKey = "api key";
    private static readonly string _baseUrl = "https://openexchangerates.org/api"; 
    private static readonly HttpClient _httpClient = new HttpClient();
    
    public static async Task<decimal> ConvertFromUahToCurrencyAsync(string targetCurrency)
    {
        string url = $"{_baseUrl}/latest.json?app_id={_apiKey}";
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            var data = JsonConvert.DeserializeObject<ExchangeRatesResponse>(json);


            decimal usdToUah = data.Rates["UAH"];
            decimal usdToTarget = data.Rates[targetCurrency];

            decimal targetToUah = usdToUah / usdToTarget;

            return targetToUah;
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