public class EmailDTO
{
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }

    public static bool Validate(EmailDTO data)
    {
        return !(
            String.IsNullOrEmpty(data.From) ||
            String.IsNullOrEmpty(data.To) ||
            String.IsNullOrEmpty(data.Name) ||
            String.IsNullOrEmpty(data.Title) ||
            String.IsNullOrEmpty(data.Text)
            );
    }

    public Email Email { get => new Email { From = From, To = To, Name = Name, Title = Title, Text = Text }; }
}