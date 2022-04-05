internal class Skrzynka
{
    private List<WiadomoscEmail> skrzynka;
    private string adres;

    public string Adres { get { return adres; } }
    public Skrzynka(string adres)
    {
        this.adres = adres;
        skrzynka = new List<WiadomoscEmail>();
    }

    public void DodajWiadomosc(WiadomoscEmail wiadomosc)
    {
        skrzynka.Add(wiadomosc);
    }

    public void DodajWiadomosci(List<WiadomoscEmail> wiadomosci)
    {
        wiadomosci.ForEach(w => DodajWiadomosc(w));
    }

    public List<WiadomoscEmail> OtrzymajWiadomosci()
    {
        return skrzynka;
    }
}