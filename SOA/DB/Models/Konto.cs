using System.Text.Json.Serialization;

public class Konto
{
    public int? Id { get; set; } = null;
    public string AdresEmail { get; set; } = null!;
    public string Name { get; set; } = null!;
}
