using System.Collections;

internal abstract class Skrzynka : IEnumerable<WiadomoscEmail>
{
    protected Dictionary<Folder, IList<WiadomoscEmail>> wiadomosci;
    protected List<Folder> foldery;

    public Skrzynka()
    {
        wiadomosci = new Dictionary<Folder, IList<WiadomoscEmail>>();
        foldery = new List<Folder>();

        DodajDomyslneFoldery();
        DodajWiadomosci();
    }

    public void DodajFolder(Folder folder)
    {
        if (foldery.Any(f => f.Nazwa.Equals(folder.Nazwa)))
        {
            int licznik = 1;
            var innaNazwa = $"{folder.Nazwa} ({licznik})";

            while (foldery.Any(f => f.Nazwa.Equals(innaNazwa)))
            {
                licznik++;
                innaNazwa = $"{folder.Nazwa} ({licznik})";
                if (licznik > 100)
                    throw new ApplicationException($"Nie mogę utworzyć folder z taką nazwą: {folder.Nazwa}");
            }

            folder.Nazwa = innaNazwa;
        }

        foldery.Add(folder);
        wiadomosci[folder] = new List<WiadomoscEmail>();
    }

    public void DodajWiadomosc(Folder folder, WiadomoscEmail wiadomosc)
    {
        if (!foldery.Contains(folder))
        {
            throw new ApplicationException($"Skrzyńka nie zawiera folderu z taką nazwą: {folder.Nazwa}");
        }

        wiadomosci[folder].Add(wiadomosc);
    }

    public IEnumerator<WiadomoscEmail> GetEnumerator()
    {
        var wszystko = new List<WiadomoscEmail>();

        foreach (var folder in wiadomosci.Values)
        {
            wszystko.AddRange(folder);
        }

        return new SkrzynkaIterator(wszystko);
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        var wszystko = new List<WiadomoscEmail>();

        foreach (var folder in wiadomosci.Values)
        {
            wszystko.AddRange(folder);
        }

        return new SkrzynkaIterator(wszystko);
    }

    protected abstract void DodajDomyslneFoldery();
    protected abstract void DodajWiadomosci();

    public override string ToString()
    {
        var f = "Nie ma żadnych folderów.";

        if (foldery.Any())
        {
            f = foldery.Aggregate("Foldery: ", (acc, val) => acc + ", " + val.Nazwa);
        }

        var w = "Skrzyńka pusta";

        if (wiadomosci.Values.Any())
        {
            w = "Wiadomości:\n\n";
            foreach (var kv in wiadomosci)
            {
                if (kv.Value.Any())
                {
                    w += kv.Value.Aggregate(kv.Key.Nazwa + ":\n", (acc, val) => acc + val.ToString() + "\n");
                }
                else
                {
                    w += "Folder jest pusty.\n";
                }
            }
        }

        return f + "\n\n" + w;
    }
}