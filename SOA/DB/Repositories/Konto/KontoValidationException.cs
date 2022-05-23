public class KontoValidationException : ValidationException
{
    public KontoValidationException(Konto konto) : base(
      $"Konto {konto.AdresEmail}, {konto.Name} nie jest poprawne") { }
}