public class KontoNotFoundException : RecordNotFoundException
{
    public KontoNotFoundException(Konto konto) : base(
      $"Nie znaleziono konta [ Id: {konto.Id}, AdresEmail: {konto.AdresEmail}, Name: {konto.Name} ]"
    ) { }

    public KontoNotFoundException(int id) : base(
      $"Nie znaleziono konta [ Id: {id} ]"
    ) { }
}