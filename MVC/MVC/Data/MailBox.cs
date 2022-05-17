public class MailBox
{
    private IList<Email> emails;
    private bool seeded;
    private int lastIndex;

    public MailBox()
    {
        emails = new List<Email>();
        seeded = false;
        lastIndex = -1;
    }

    public void Seed()
    {
        if (!seeded)
        {
            SeedEmails(10);
            seeded = true;
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
                
                var email = new Email()
                {
                    From = fromEmail, To = toEmail, Name = name, Title = "Hello, World!", Text = "Hello, World!"
                };

                AddEmail(email);
            }
        }
    }

    public void Clear()
    {
        emails.Clear();
    }

    public void AddEmail(Email email)
    {
        lastIndex++;

        email.Id = lastIndex;
        emails.Add(email);
    }

    public void DeleteEmail(Email email)
    {
        emails.Remove(email);
    }

    public Email FindEmail(int id)
    {
        var found = emails.Where(e => e.Id == id);

        if (!found.Any()) throw new Exception($"Email with id {id} not found");

        return found.First();
    }

    public IEnumerable<Email> FindEmails(string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return emails;
        }

        return emails.Where(e =>
        {
            var value = e.From + e.Text + e.Name + e.Title + e.To;

            return value.Contains(search);
        });
    }

    public IEnumerable<Email> FindEmails()
    {
        return FindEmails("");
    }
}
