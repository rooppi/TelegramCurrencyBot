namespace Convertor;

public class ConversionBotClass 
{
    
    private readonly CurrencyConverter _converter;

    public ConversionBotClass(CurrencyConverter converter)
    {
        _converter = converter;
    }
    
    public async Task DemoConversionAsync() 
    {
        try
        {
            foreach (var c in new[] { "USD", "EUR" })
                Console.WriteLine($"1 {c} = {await _converter.ConvertFromUahToCurrencyAsync(c):0.00} UAH");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
