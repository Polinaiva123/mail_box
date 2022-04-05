internal class KlientPocztyTekstowy : KlientPoczty
{
    public static new KlientPoczty Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = new KlientPocztyTekstowy();
            }
            return _instance;
        }
    }

    private KlientPocztyTekstowy() : base() { }

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