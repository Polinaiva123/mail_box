// Facade
internal class KlientPoczty
{
    protected List<Skrzynka> skrzynkiPocztowe;
    protected List<WiadomoscEmail>? wszystkieWiadomosci;
    protected Skrzynka? wybranaSkrzynka;

    protected static KlientPoczty? _instance;

    public static KlientPoczty Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = new KlientPoczty();
            }
            return _instance;
        }
    }

    protected KlientPoczty()
    {
        skrzynkiPocztowe = new List<Skrzynka>();
    }

    public void SynchronizujPoczte()
    {
        if (skrzynkiPocztowe.Any())
        {
            wszystkieWiadomosci = skrzynkiPocztowe
            .Select(s => s.OtrzymajWiadomosci())
            .ToList()
            .Aggregate(new List<WiadomoscEmail>(), (wszyWiad, skrzWiad) => wszyWiad.Concat(skrzWiad).ToList());
        }
        else
        {
            wszystkieWiadomosci = new List<WiadomoscEmail>();
        }
    }

    public void ZobaczSkrzynki()
    {
        Console.WriteLine("Dodane skrzynki pocztowe:\n");
        if (skrzynkiPocztowe.Any())
        {
            Console.WriteLine(skrzynkiPocztowe.Select(s => s.Adres).ToList().Aggregate("", (wynik, adres) => wynik + adres + "\n"));
        }
        else
        {
            Console.WriteLine("Nie masz dodanych skrzynek pocztowych");
        }
    }

    public void WybierzSkrzynke()
    {
        ZobaczSkrzynki();

        if (!skrzynkiPocztowe.Any())
        {
            Console.WriteLine("Nie masz żanych dodanych skrzynek.");
            UtworzSkrzynke();
        }

        Console.Write("Wybierz skrzyńkę: ");
        string adres = Console.ReadLine()!.Trim().ToLower();
        wybranaSkrzynka = skrzynkiPocztowe.Find(s => s.Adres.Equals(adres));

        while (wybranaSkrzynka is null)
        {
            Console.WriteLine("Nie znaleziono podanej skrzynki.");
            Console.Write("Wybierz skrzyńkę: ");
            adres = Console.ReadLine()!.Trim().ToLower();

            wybranaSkrzynka = skrzynkiPocztowe.Find(s => s.Adres.Equals(adres));
        }
    }

    public void WybierzDomyslnaSkrzynke()
    {
        if (skrzynkiPocztowe.Any())
        {
            wybranaSkrzynka = skrzynkiPocztowe.First();
        }
    }

    protected void UstawDomyslnaSkrzynke(Skrzynka skrzynka)
    {
        if (!skrzynkiPocztowe.Any())
        {
            wybranaSkrzynka = skrzynka;
        }
    }

    protected bool AdresPoprawny(string adres)
    {
        return adres.Contains("@") && adres.Length > 3 && !adres.Contains(" ");
    }

    protected bool SkrzynkaIstnieje(string adres)
    {
        return skrzynkiPocztowe.FindAll(s => s.Adres.Equals(adres)).Any();
    }

    public void UtworzSkrzynke()
    {
        Console.WriteLine("Utwórz skrzyńkę:\n");
        Console.Write("Podaj adres: ");

        string adres = Console.ReadLine()!;

        bool poprawny = AdresPoprawny(adres);
        bool istnieje = SkrzynkaIstnieje(adres);

        while (!poprawny || istnieje)
        {
            if (!poprawny)
                Console.WriteLine("Podano nieprawidłowy adres.");
            else if (istnieje)
                Console.WriteLine("Skrzynka już istnieje.");

            Console.Write("Podaj adres: ");
            adres = Console.ReadLine()!;

            poprawny = AdresPoprawny(adres);
            istnieje = SkrzynkaIstnieje(adres);
        }

        Skrzynka skrzynka = new Skrzynka(adres);

        UstawDomyslnaSkrzynke(skrzynka);

        skrzynkiPocztowe.Add(skrzynka);
        Console.WriteLine($"Skrzynka {adres} została utworzona");
    }

    public void UsunSkrzynke()
    {
        if (skrzynkiPocztowe.Any())
        {
            Console.Write("Usuń skrzyńkę: ");
            string adres = Console.ReadLine()!.Trim().ToLower();

            Skrzynka? docelowaSkrzynka = skrzynkiPocztowe.Find(s => s.Adres.Equals(adres));

            while (docelowaSkrzynka is null)
            {
                Console.WriteLine("Nie znaleziono podanej skrzynki.");
                Console.Write("Usuń skrzyńkę: ");
                adres = Console.ReadLine()!.Trim().ToLower();
                docelowaSkrzynka = skrzynkiPocztowe.Find(s => s.Adres.Equals(adres));
            }

            if (!skrzynkiPocztowe.Remove(docelowaSkrzynka))
            {
                throw new ApplicationException("Nie udało się usunąć skrzyńki.");
            }

            if (wybranaSkrzynka is not null)
            {
                if (wybranaSkrzynka.Equals(docelowaSkrzynka))
                {
                    WybierzDomyslnaSkrzynke();
                }
            }
        }
        else
        {
            throw new ApplicationException("Nie masz żadnych dodanyh skrzynek.");
        }
    }

    public virtual void ZobaczWiadomosci()
    {
        if (wszystkieWiadomosci is null)
        {
            SynchronizujPoczte();
        }
        Console.WriteLine("Wiadomości:\n");

        if (wszystkieWiadomosci!.Any())
        {
            Console.WriteLine(wszystkieWiadomosci!.Select(w => new HTMLWiadomoscEmail(w).ToString()).ToList().Aggregate("", (wynik, html) => wynik + html + "\n\n"));
        }
        else
        {
            Console.WriteLine("Nie masz żadnych wiadomości");
        }
    }

    public void ZaladujPrzykladoweWiadomosci()
    {
        if (skrzynkiPocztowe.Any())
        {
            ZalacznikFactory zwyklyZalacznik = new ZwyklyZalacznikFactory();
            ZalacznikFactory zalacznikJakoPlik = new ZalacznikJakoPlikFactory();

            var wiadomosci = new List<WiadomoscEmail>
            {
                new WiadomoscEmail
                {
                    Od = "abc123@gmail.com",
                    Do = "123abc@outlook.com",
                    Tytul = "Hello, World!",
                    Tresc = "Hello, World!",
                    Zalaczniki = new List<Zalacznik>
                    {
                        zwyklyZalacznik.CreateZalacznik("dokument"),
                        zwyklyZalacznik.CreateZalacznik("dokument"),
                        zalacznikJakoPlik.CreateZalacznik("plik")
                    }
                },

                new WiadomoscEmail
                {
                    Od = "helloworld@gmail.com",
                    Do = "dawdaw@outlook.com",
                    Tytul = "Inny Hello, World!",
                    Tresc = "HelloWorldHelloWorldHelloWorld",
                    Zalaczniki = new List<Zalacznik>
                    {
                        zwyklyZalacznik.CreateZalacznik("dokument"),
                        zwyklyZalacznik.CreateZalacznik("obraz"),
                        zwyklyZalacznik.CreateZalacznik("obraz"),
                        zwyklyZalacznik.CreateZalacznik("obraz"),
                        zalacznikJakoPlik.CreateZalacznik("plik")
                    }
                },

                new WiadomoscEmail
                {
                    Od = "helloworld@gmail.com",
                    Do = "dawdaw@outlook.com",
                    Tytul = "Inny Hello, World!",
                    Tresc = "HelloWorldHelloWorldHelloWorld",
                    Zalaczniki = new List<Zalacznik>
                    {
                        zwyklyZalacznik.CreateZalacznik("dokument"),
                        zalacznikJakoPlik.CreateZalacznik("plik"),
                        zalacznikJakoPlik.CreateZalacznik("plik"),
                        zalacznikJakoPlik.CreateZalacznik("plik")
                    }
                }

            };

            skrzynkiPocztowe.ForEach(s => s.DodajWiadomosci(wiadomosci));
        }
        else
        {
            throw new ApplicationException("Nie masz żadnych dodanych skrzynek.");
        }
    }
}
