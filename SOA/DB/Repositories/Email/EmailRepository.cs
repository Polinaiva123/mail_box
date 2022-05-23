public class EmailRepository
{
    private readonly List<Email> emails;
    private KontoRepository _kontoRepository = null!;

    private int NewId => emails.Count;

    private readonly int seedCount;

    public EmailRepository(int seedCount)
    {
        this.seedCount = Math.Abs(seedCount);

        emails = new List<Email>();
    }

    public void RegisterKontoRepository(KontoRepository kontoRepository)
    {
        _kontoRepository = kontoRepository;
    }

    public EmailRepository() : this(10) { }

    public bool Validate(Email email)
    {
        var nonEmpty = !(String.IsNullOrEmpty(email.Title) || email.From is null || email.To is null);

        return nonEmpty;
    }

    public void ValidateThrow(Email email)
    {
        if (!Validate(email))
            throw new EmailValidationException(email);
    }

    public IEnumerable<Email> Records => emails;

    public Email Add(Email email)
    {
        ValidateThrow(email);

        email.Id = NewId;

        // Stworz nowe konto odbiorcy, jeżeli nie istnieje
        if (!_kontoRepository.Records.Any(k => k.AdresEmail.Equals(email.To.AdresEmail)))
        {
            _kontoRepository.Add(email.To);
        }

        emails.Add(email);

        return email;
    }

    public void Usun(Email email)
    {
        var found = Znajdz(email);

        if (found is null)
            throw new EmailNotFoundException(email);

        
        emails.Remove(found);
    }

    public void Usun(int id)
    {
        var found = Znajdz(id);

        if (found is null)
            throw new EmailNotFoundException(id);

        emails.Remove(found);
    }

    public Email? Znajdz(Email email)
    {
        ValidateThrow(email);

        return emails.Find(e => e.Id == email.Id);
    }

    public Email? Znajdz(int id)
    {
        return emails.Find(e => e.Id == id);
    }

    public void KontoAddedAction(Konto konto)
    {
        for (var i = 0; i < seedCount; i++)
        {
            var option = new Random().Next(0, 2) > 0;
            var from = option ? RandomKonto() : konto;
            var to = option ? konto : RandomKonto(exclude: from);

            var email = new Email { From = from, To = to, Text = "Hello, World!", Title = "Hello, World!" };

            email.Id = NewId;

            emails.Add(email);
        }
    }

    private Konto RandomKonto()
    {
        return RandomKonto(new List<Konto>());
    }

    private Konto RandomKonto(Konto exclude)
    {
        return RandomKonto(new List<Konto> { exclude });
    }

    private Konto RandomKonto(IEnumerable<Konto> exclude) 
    {
        var konta = _kontoRepository.Records;

        var filtered = konta.Where(k => exclude.All(e => k.Id != e.Id)).ToList();
        var randomKonto = filtered[new Random().Next(0, filtered.Count)];

        return randomKonto;
    }

}