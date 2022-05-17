public class EmailActionViewModel
{
    public bool ActionCompleted { get; set; } = false;
    public Email Email { get; set; }
    public bool HasError { get; set; } = false;
}
