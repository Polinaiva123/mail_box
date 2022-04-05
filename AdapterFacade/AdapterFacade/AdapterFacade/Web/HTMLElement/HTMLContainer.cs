internal class HTMLContainer : HTMLElement
{
    public HTMLContainer(string zawartosc) : base("div", zawartosc) { }
    public HTMLContainer(List<HTMLElement> zawartosc) : this(zawartosc.Select(el => el.ToString()).Aggregate("", (z, el) => z + el + "\n")) { }
    public HTMLContainer(HTMLElement zawartosc) : this(zawartosc.ToString()) { }

}