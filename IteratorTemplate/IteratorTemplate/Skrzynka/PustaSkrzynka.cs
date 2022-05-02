internal class PustaSkrzynka : Skrzynka
{
    protected override void DodajWiadomosci() { }
    protected override void DodajDomyslneFoldery() { }

    public override string ToString()
    {
        return "Pusta skrzyńka:\n" + base.ToString();
    }
}
