internal class HTMLBold : HTMLElement
{
    public HTMLBold(string zawartosc) : base("b", zawartosc) { }
    public HTMLBold(HTMLElement zawartosc) : this(zawartosc.ToString()) { }
}