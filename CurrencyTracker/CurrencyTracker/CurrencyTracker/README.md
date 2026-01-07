# CurrencyTracker – Döviz Takip Konsol Uygulamasý

Bu proje, Türk Lirasý (TRY) bazlý döviz kurlarýný takip etmek amacýyla geliþtirilmiþ bir C# konsol uygulamasýdýr.  
Döviz verileri Frankfurter FREE API üzerinden gerçek zamanlý olarak alýnmakta ve LINQ kullanýlarak iþlenmektedir.

---

## Kullanýlan Teknolojiler

- C#
- .NET Console Application
- HttpClient
- async / await
- LINQ
- Frankfurter FREE API

---

## Kullanýlan API

Veriler aþaðýdaki API üzerinden alýnmaktadýr:

https://api.frankfurter.app/latest?from=TRY

---

## Model Sýnýflarý

Uygulamada aþaðýdaki model sýnýflarý kullanýlmýþtýr:

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
