using Telegram.Bot;

public static class ErrorHandler
{
    
    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Ошибка: {exception.Message}");
        await Task.CompletedTask;
    }
}