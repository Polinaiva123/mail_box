public class Repository
{
    private IList<Email> mails;
    private bool seeded;

    public IList<Email> Mails { get => mails; }

    public Repository()
    {
        mails = new List<Email>();
        seeded = false;
    }

    public void Seed()
    {
        if (!seeded)
        {
            SeedEmails(5);
            seeded = true;
        }
        else
        {
            throw new AlreadySeededException();
        }
    }

    private void SeedEmails(int count)
    {
        var providers = new [] { "wp.pl", "onet.pl", "gmail.com", "outlook.com", "o2.pl", "yahoo.com" };

        var femaleFirstNames = new [] { "Anna", "Katarzyna", "Magda", "Maria", "Angnieżka", "Monika" };
        var maleFirstNames = new [] { "Grzegorz", "Jan", "Tadeusz", "Marcin", "Paweł", "Dawid" };

        var lastNames = new [] { "Nowak", "Klepacz", "Koś", "Waś", "Abramowicz", "Kołodziejczyk", "Kowalczyk" };

        string RandomName()
        {
            var maleFemale = new Random().Next(0, 2) > 0;

            return (
                (
                    maleFemale ?
                        maleFirstNames[new Random().Next(0, maleFirstNames.Length)] :
                        femaleFirstNames[new Random().Next(0, femaleFirstNames.Length)]
                    )
                + " " +
                    lastNames[new Random().Next(0, lastNames.Length)]
                );
        }

        string EmailFromName(string name)
        {
            return name.Replace(" ", "").ToLower() + "@" + providers[new Random().Next(0, providers.Length)];
        }

        for (int i = 0; i < count; i++)
        {
            {
                var name = RandomName();
                var fromEmail = EmailFromName(name);

                var n = RandomName();
                while (n.Equals(name))
                {
                    n = RandomName();
                }

                var toEmail = EmailFromName(n);
                
                var email = new Email(fromEmail, toEmail, name, "Hello, World!", "Hello, World!");
                mails.Add(email);
            }
        }
    }

    public void Clear()
    {
        mails.Clear();
    }

    public void AddEmail(Email email)
    {
        mails.Add(email);
    }
}
