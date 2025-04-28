namespace CurrencyConvertorBot.Config;

public class Config
{
    public string BotToken { get; set; }
    public string CurrencyApiKey { get; set; }

    public Config(string botToken, string currencyApiKey)
    {
        BotToken = botToken;
        CurrencyApiKey = currencyApiKey;
    }

   
}