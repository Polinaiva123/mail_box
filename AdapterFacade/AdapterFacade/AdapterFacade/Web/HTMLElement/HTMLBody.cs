internal class HTMLBody : HTMLElement
{
    public HTMLBody(string zawartosc) : base("body", zawartosc) { }
    public HTMLBody(HTMLElement zawartosc) : this(zawartosc.ToString()) { }

}