var klient = GetKlientPoczty();

klient.UtworzSkrzynke();
klient.UtworzSkrzynke();
klient.ZaladujPrzykladoweWiadomosci();
klient.WybierzSkrzynke();
klient.ZobaczWiadomosci();
klient.UsunSkrzynke();


static KlientPoczty GetKlientPoczty()
{
    Console.WriteLine("W jakiej postaci wyświetlać wiadomości (html/TEKST)?: ");
    string? wartosc = Console.ReadLine();

    while (String.IsNullOrEmpty(wartosc))
    {
        Console.WriteLine("W jakiej postaci wyświetlać wiadomości (html/TEKST)?: ");
        wartosc = Console.ReadLine();
    }

    switch (wartosc)
    {
        case "html":
            return KlientPoczty.Instance;
        case "tekst":
            return KlientPocztyTekstowy.Instance;
        default:
            return KlientPocztyTekstowy.Instance;
    }
}