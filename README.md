# CurrencyTracker
# CurrencyTracker – Döviz Takip Konsol Uygulaması

Bu proje, Türk Lirası (TRY) bazlı döviz kurlarını takip etmek amacıyla geliştirilmiş bir C# konsol uygulamasıdır.  
Döviz verileri Frankfurter FREE API üzerinden gerçek zamanlı olarak alınmakta ve LINQ kullanılarak işlenmektedir.

---

## Kullanılan Teknolojiler

- C#
- .NET Console Application
- HttpClient
- async / await
- LINQ
- Frankfurter FREE API

---

## Kullanılan API

Veriler aşağıdaki API üzerinden alınmaktadır:

https://api.frankfurter.app/latest?from=TRY

---

## Model Sınıfları

Uygulamada aşağıdaki model sınıfları kullanılmıştır:

```csharp
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
