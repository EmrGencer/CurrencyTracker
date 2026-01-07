using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class CurrencyResponse
{
    public string Base { get; set; }
    public Dictionary<string, decimal> Rates { get; set; }
}

class Currency
{
    public string Code { get; set; }
    public decimal Rate { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        List<Currency> currencies = await GetCurrenciesAsync();

        while (true)
        {
            Console.WriteLine("===== CurrencyTracker =====");
            Console.WriteLine("1. Tüm dövizleri listele");
            Console.WriteLine("2. Koda göre döviz ara");
            Console.WriteLine("3. Belirli bir değerden büyük dövizleri listele");
            Console.WriteLine("4. Dövizleri değere göre sırala");
            Console.WriteLine("5. İstatistiksel özet göster");
            Console.WriteLine("0. Çıkış");
            Console.Write("Seçiminiz: ");

            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    currencies
                        .Select(c => c)
                        .ToList()
                        .ForEach(c =>
                            Console.WriteLine($"{c.Code} : {c.Rate}"));
                    break;

                case "2":
                    Console.Write("Döviz kodu girin: ");
                    string code = Console.ReadLine();

                    currencies
                        .Where(c => c.Code.Equals(
                            code,
                            StringComparison.OrdinalIgnoreCase))
                        .ToList()
                        .ForEach(c =>
                            Console.WriteLine($"{c.Code} : {c.Rate}"));
                    break;

                case "3":
                    Console.Write("Minimum değer girin: ");
                    decimal min = decimal.Parse(Console.ReadLine());

                    currencies
                        .Where(c => c.Rate > min)
                        .ToList()
                        .ForEach(c =>
                            Console.WriteLine($"{c.Code} : {c.Rate}"));
                    break;

                case "4":
                    Console.WriteLine("1 - Artan");
                    Console.WriteLine("2 - Azalan");
                    Console.Write("Seçim: ");
                    string sort = Console.ReadLine();

                    var sorted = sort == "2"
                        ? currencies.OrderByDescending(c => c.Rate)
                        : currencies.OrderBy(c => c.Rate);

                    sorted
                        .ToList()
                        .ForEach(c =>
                            Console.WriteLine($"{c.Code} : {c.Rate}"));
                    break;

                case "5":
                    Console.WriteLine($"Toplam Döviz Sayısı: {currencies.Count()}");
                    Console.WriteLine($"En Yüksek Kur: {currencies.Max(c => c.Rate)}");
                    Console.WriteLine($"En Düşük Kur: {currencies.Min(c => c.Rate)}");
                    Console.WriteLine($"Ortalama Kur: {currencies.Average(c => c.Rate)}");
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }

            Console.WriteLine("\nDevam etmek için bir tuşa bas...");
            Console.ReadKey();
            Console.WriteLine();
        }
    }

    static async Task<List<Currency>> GetCurrenciesAsync()
    {
        using HttpClient client = new HttpClient();
        string url = "https://api.frankfurter.app/latest?from=TRY";

        string json = await client.GetStringAsync(url);

        CurrencyResponse response =
            JsonSerializer.Deserialize<CurrencyResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (response == null || response.Rates == null)
        {
            Console.WriteLine("Döviz verileri alınamadı.");
            return new List<Currency>();
        }

        return response.Rates
            .Select(r => new Currency
            {
                Code = r.Key,
                Rate = r.Value
            })
            .ToList();
    }
}
