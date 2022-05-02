using System.Collections;

internal class SkrzynkaIterator : IEnumerator<WiadomoscEmail>
{
    private IEnumerator<WiadomoscEmail> iterator;

    public SkrzynkaIterator(IEnumerable<WiadomoscEmail> wiadomosci)
    {
        iterator = wiadomosci.GetEnumerator();
    }

    public WiadomoscEmail Current => iterator.Current;

    object IEnumerator.Current => iterator.Current;

    public void Dispose()
    {
        iterator.Dispose();
    }

    public bool MoveNext()
    {
        return iterator.MoveNext();
    }

    public void Reset()
    {
        iterator.Reset();
    }
}