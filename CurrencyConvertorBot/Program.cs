using Convertor;
using CurrencyConvertorBot;
using CurrencyConvertorBot.Config;
using DotNetEnv;


Env.Load(".env");
var cfg = LoadConfig();

Config LoadConfig()
{
    //подгружаем с ENV токен бота, и проверяем его на null.
    var botToken = Env.GetString("BOT_TOKEN");
    if (string.IsNullOrEmpty(botToken))
        throw new Exception("Bot token is missing.");
    // тоже самое только с ApI конвертора.
    var currencyApiKey = Env.GetString("CURRENCY_API_KEY");
    if (string.IsNullOrEmpty(currencyApiKey))
        throw new Exception("Weather API key is missing.");
    
    return new Config(botToken, currencyApiKey);
}


var botClient = new CurrencyBot(botToken: cfg.BotToken , cfg);
await botClient.StartBotAsync();