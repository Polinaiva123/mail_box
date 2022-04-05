// Facade
internal class KlientPocztyTekstowy : KlientPoczty
{
    public KlientPocztyTekstowy() : base() { }

    public override void ZobaczWiadomosci()
    {
        if (wszystkieWiadomosci is null)
        {
            SynchronizujPoczte();
        }
        Console.WriteLine("Wiadomości:\n");

        if (wszystkieWiadomosci!.Any())
        {
            Console.WriteLine(wszystkieWiadomosci!.Select(w => new HTMLWiadomoscEmail(w).GetZrodlo().ToString()).ToList().Aggregate("", (wynik, text) => wynik + text + "\n\n"));
        }
        else
        {
            Console.WriteLine("Nie masz żadnych wiadomości");
        }
    }
}