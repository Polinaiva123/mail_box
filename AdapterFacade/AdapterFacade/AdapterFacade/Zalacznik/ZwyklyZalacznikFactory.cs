internal class ZwyklyZalacznikFactory : ZalacznikFactory
{
    public override Zalacznik CreateZalacznik(String rodzaj)
    {
        switch (rodzaj.ToLower())
        {
            case "dokument":
                return new Dokument();
            case "obraz":
                return new Obraz();
            default:
                throw new ApplicationException("Nie rozpoznany rodzaj zalacznika");
        }
    }
}