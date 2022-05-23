public class Context
{
    public KontoRepository Konta { get; private init; }
    public EmailRepository Emails { get; private init; }

    public Context()
    {
        Konta = new KontoRepository();
        Emails = new EmailRepository();

        Konta.RegisterEmailRepository(Emails);
        Emails.RegisterKontoRepository(Konta);

        Konta.Seed(10);
    }
}