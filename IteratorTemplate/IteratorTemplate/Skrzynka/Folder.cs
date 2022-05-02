internal class Folder
{
    public string Nazwa { get; set; } = "Nowy folder";

    public override bool Equals(object? obj)
    {
        if (obj is not null)
        {
            var folder = obj as Folder;
            if (folder is not null)
            {
                return folder.Nazwa.Equals(Nazwa);
            }
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Nazwa.GetHashCode();
    }
}