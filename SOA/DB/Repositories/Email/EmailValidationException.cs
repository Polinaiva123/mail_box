public class EmailValidationException : ValidationException
{
    public EmailValidationException(Email email) : base(
      $"Email [ Title: {email.Title}, Text: {email.Text}, From: {email.From}, To: {email.To} ] jest niepoprawny"
    ) { }
}