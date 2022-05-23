public class EmailNotFoundException : RecordNotFoundException
{
    public EmailNotFoundException(Email email) : base(
      $"Nie znaleziono email [ Id: {email.Id}, Title: {email.Title}, Text: {email.Text}, From: {email.From}, To: {email.To} ]"
    ) { }

    public EmailNotFoundException(int id) : base(
      $"Nie znaleziono email [ Id: {id} ]"
    ) { }
}