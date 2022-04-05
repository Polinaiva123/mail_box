using System.Text;

internal class HTMLStrona
{
    private string _tytul;
    private string _zawartosc;

    public string Tytul
    { 
        get { return _tytul; }
        set
        {
            _tytul = value;
            Odswiez();
        }
    }

    public string Zawartosc
    {
        get { return _zawartosc; }
        set
        {
            _zawartosc = value;
            Odswiez();
        }
    }

    public HTMLElement drzewo;

    public HTMLStrona()
    {
        _tytul = "";
        _zawartosc = "";

        Odswiez();
    }

    public void Odswiez()
    {
        drzewo = new HTMLElement(
            new List<HTMLElement>()
            {
                new HTMLHead(new HTMLTitle(Tytul)),
                new HTMLBody(Zawartosc)
            }
        );
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine("<!DOCTYPE html>");
        builder.Append(drzewo.ToString());

        return builder.ToString();
    }
}