public class KontoRepository
{
    private List<Konto> konta;

    private int NewId => konta.Count;

    private EmailRepository _emailRepository = null!;

    public KontoRepository()
    {
        konta = new List<Konto>();
    }

    public void RegisterEmailRepository(EmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
    }

    public bool Validate(Konto konto)
    {
        var nonEmpty = !(String.IsNullOrEmpty(konto.AdresEmail) || String.IsNullOrEmpty(konto.Name));
        var emailValid = konto.AdresEmail.Length > 3 && konto.AdresEmail.Contains("@");

        return nonEmpty && emailValid;
    }

    private bool Exists(Konto konto)
    {
        var found = konta.Find(k => k.AdresEmail.Equals(konto.AdresEmail));

        return found is not null;
    }

    private void ExistsThrow(Konto konto)
    {
        if (Exists(konto))
            throw new KontoExistsException(konto);
    }

    public void ValidateThrow(Konto konto)
    {
        if (!Validate(konto))
        {
            throw new KontoValidationException(konto);
        }
    }

    public Konto Add(Konto konto)
    {
        ValidateThrow(konto);
        ExistsThrow(konto);

        konto.Id = NewId;

        konta.Add(konto);

        _emailRepository.KontoAddedAction(konto);

        return konto;
    }

    public void Usun(Konto konto)
    {
        var found = Znajdz(konto);

        if (found is null)
            throw new KontoNotFoundException(konto);

        konta.Remove(found);
    }

    public void Usun(int id)
    {
        var found = Znajdz(id);

        if (found is null)
            throw new KontoNotFoundException(id);

        konta.Remove(found);
    }

    public Konto? Znajdz(Konto konto)
    {
        ValidateThrow(konto);

        return konta.Find(k => k.Id == konto.Id);
    }

    public Konto? Znajdz(int id)
    {
        return konta.Find(k => k.Id == id);
    }

    public IEnumerable<Konto> Records => konta;

    public void Seed(int seedCount)
    {
        if (!konta.Any())
        {
            for (var i = 0; i < seedCount; i++)
            {
                var konto = RandomKonto();

                while (konta.FindAll(k => k.AdresEmail.Equals(konto.AdresEmail)).Any())
                {
                    konto = RandomKonto();
                }

                konto.Id = NewId;

                konta.Add(konto);
            }

            konta.ForEach(k => _emailRepository.KontoAddedAction(k));
        }
    }
    
    private Konto RandomKonto()
    {
        var providers = new [] { "wp.pl", "onet.pl", "gmail.com", "outlook.com", "o2.pl", "yahoo.com" };

        var femaleFirstNames = new [] { "Anna", "Katarzyna", "Magda", "Maria", "Angnieżka", "Monika" };
        var maleFirstNames = new [] { "Grzegorz", "Jan", "Tadeusz", "Marcin", "Paweł", "Dawid" };

        var lastNames = new [] { "Nowak", "Klepacz", "Koś", "Waś", "Abramowicz", "Kołodziejczyk", "Kowalczyk" };

        var maleFemale = new Random().Next(0, 2) > 0;

        var randomName = (
            (
                maleFemale ?
                    maleFirstNames[new Random().Next(0, maleFirstNames.Length)] :
                    femaleFirstNames[new Random().Next(0, femaleFirstNames.Length)]
                )
            + " " +
                lastNames[new Random().Next(0, lastNames.Length)]
            );

        var email = randomName.Replace(" ", "").ToLower() + "@" + providers[new Random().Next(0, providers.Length)];

        return new Konto { AdresEmail = email, Name = randomName };
    }
}