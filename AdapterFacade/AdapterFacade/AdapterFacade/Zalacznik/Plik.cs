internal class Plik : Zalacznik
{
    public void Otworz()
    {
        Console.WriteLine("To jest plik");
    }

    public override string ToString()
    {
        return "Plik";
    }
}