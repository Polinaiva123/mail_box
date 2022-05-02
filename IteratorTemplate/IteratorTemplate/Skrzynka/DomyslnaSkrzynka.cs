internal class DomyslnaSkrzynka : Skrzynka
{
    public DomyslnaSkrzynka() { }

    protected override void DodajWiadomosci() { }

    protected override void DodajDomyslneFoldery()
    {
        DodajFolder(new Folder { Nazwa = "Przychodzące" });
        DodajFolder(new Folder { Nazwa = "Wychodzące" });
        DodajFolder(new Folder { Nazwa = "Ważne" });
        DodajFolder(new Folder { Nazwa = "Spam" });
        DodajFolder(new Folder { Nazwa = "Kosz" });
    }

    public override string ToString()
    {
        return "Domyślna skrzyńka:\n" + base.ToString();
    }
}
