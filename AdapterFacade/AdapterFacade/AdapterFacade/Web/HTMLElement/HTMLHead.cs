internal class HTMLHead : HTMLElement
{
    public HTMLHead(string zawartosc) : base("head", zawartosc) { }
    public HTMLHead(HTMLElement zawartosc) : this(zawartosc.ToString()) { }
}