public class Email
{
    public int? Id { get; set; } = null;
    public Konto From { get; set; } = null!;
    public Konto To { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
}
