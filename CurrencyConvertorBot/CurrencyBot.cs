using System.Globalization;
using Convertor;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CurrencyConvertorBot
{
    public class CurrencyBot
    {
        private readonly string _botToken;
        private readonly TelegramBotClient _botClient;
        private readonly Config.Config _config;
        
        
        public CurrencyBot(string botToken, Config.Config config)
        {
            _botToken = botToken;
            _config = config;
            
            _botClient = new TelegramBotClient(_botToken);
        }
        
        
        public async Task StartBotAsync()
        {
           
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() 
            };

            _botClient.OnUpdate += MassegeHandler;
            
            Console.WriteLine("Press Enter to stop the bot");
            Console.ReadLine();
        }
        
        public async Task MassegeHandler(Update update)
        { 
           if (update.Type == UpdateType.Message)
           {
              var messege = update.Message;
              var chatId = update.Message.Chat.Id;

              if ( messege.Text == "/start")
              {
                 await _botClient.SendMessage(messege.Chat,"Привіт. Я бот, котрий вміє конвертувати валюти до гривні.\nБудь ласка, обери валюту!");
              
                 var inlineKeyboard = new InlineKeyboardMarkup(new[]
                 {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("🇺🇸", "USD"),
                       InlineKeyboardButton.WithCallbackData("🇵🇱", "PLN"),
                       InlineKeyboardButton.WithCallbackData("🇪🇺", "EUR")
                    }
                 });
                 await _botClient.SendMessage(
                    chatId: chatId,
                    text: "Обери валюту 👇",
                    replyMarkup: inlineKeyboard);
              }
           }
           else if (update.Type == UpdateType.CallbackQuery)
           {
              var callbackQuery = update.CallbackQuery;
              var data = callbackQuery?.Data;
              var chatId = callbackQuery?.Message?.Chat.Id ?? callbackQuery?.From.Id;
            
              await _botClient.AnswerCallbackQuery(callbackQuery.Id);

              if (data == "USD")
              {
                 var value = await ConvertToUah("USD");
                 await _botClient.SendMessage(chatId: chatId, 
                    text: $"Курс USD к UAH:" + value.ToString("0.00",CultureInfo.InvariantCulture));
              }
            
              else if (data == "PLN")
              {
                 var value = await ConvertToUah("PLN");
                 await _botClient.SendMessage(chatId: chatId, 
                    text: $"Курс PLN к UAH:" + value.ToString("0.00",CultureInfo.InvariantCulture));
              }
              else if (data == "EUR")
              {
                 var value = await ConvertToUah("EUR");
                 await _botClient.SendMessage(chatId: chatId, 
                    text: $"Курс EUR к UAH:" + value.ToString("0.00",CultureInfo.InvariantCulture));
              }
           }

           async Task<decimal> ConvertToUah(string currency)
           {
              return await CurrencyConverter.ConvertFromUahToCurrencyAsync(currency);
           }
         }
    }
}