public class KontoExistsException : RecordExistsException
{
    public KontoExistsException(Konto konto) : base(
      $"Konto z adresem {konto.AdresEmail} już istnieje"
    ) { }
}