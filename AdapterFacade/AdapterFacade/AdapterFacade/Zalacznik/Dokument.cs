internal class Dokument : Zalacznik
{
    public void Otworz()
    {
        Console.WriteLine("To jest dokument");
    }

    public override string ToString()
    {
        return "Dokument";
    }
}