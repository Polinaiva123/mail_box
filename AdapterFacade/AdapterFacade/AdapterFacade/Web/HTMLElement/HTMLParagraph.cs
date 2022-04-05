internal class HTMLParagraph : HTMLElement
{
    public HTMLParagraph(string zawartosc) : base("p", zawartosc) { }
    public HTMLParagraph(HTMLElement zawartosc) : this(zawartosc.ToString()) { }

}