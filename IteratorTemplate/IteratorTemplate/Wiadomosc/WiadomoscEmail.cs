using System.Text;

internal class WiadomoscEmail
{
    public String Od { get; set; }
    public String Do { get; set; }
    public String Tytul { get; set; }
    public String Tresc { get; set; }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Od: " + Od);
        stringBuilder.AppendLine("Do: " + Do);
        stringBuilder.AppendLine("Tytul: " + Tytul);
        stringBuilder.AppendLine("Tresc: " + Tresc + "\n");

        return stringBuilder.ToString();
    }
}