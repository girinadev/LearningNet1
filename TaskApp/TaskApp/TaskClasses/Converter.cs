namespace TaskApp.TaskClasses
{
  public class Converter
  {
    private readonly double _usd;
    private readonly double _eur;
    private readonly double _rub;
    private const double _uah = 1.0;

    public Converter(double usd, double eur, double rub)
    {
      _usd = usd;
      _eur = eur;
      _rub = rub;
    }

    public double ConvertFromUah(double amount, Currencies currency) => amount * GetK(Currencies.UAH) / GetK(currency);

    public double ConvertToUah(double amount, Currencies currency) => amount * GetK(currency) / GetK(Currencies.UAH);

    private double GetK(Currencies currency)
    {
      if (currency == Currencies.EUR)
        return _eur;
      if (currency == Currencies.USD)
        return _usd;
      if (currency == Currencies.RUB)
        return _rub;

      return _uah;
    }
  }
}
