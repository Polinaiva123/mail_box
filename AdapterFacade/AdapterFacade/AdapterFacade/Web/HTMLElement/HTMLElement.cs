using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class HTMLElement
    {
        private string nazwa;
        private string zawartosc;

        public HTMLElement(string nazwa, string zawartosc)
        {
            if (String.IsNullOrWhiteSpace(nazwa))
            {
                throw new ArgumentException("Nazwa HTMLElementu nie może być pusta");
            }

            this.nazwa = nazwa;
            if (zawartosc is not null)
            {
                this.zawartosc = zawartosc;
            }
            else
            {
                this.zawartosc = "";
            }

        }

        public HTMLElement(string nazwa, HTMLElement zawartosc) : this(nazwa, zawartosc.ToString()) { }
        public HTMLElement(string nazwa, List<HTMLElement> zawartosc) : this(nazwa, zawartosc.Select(el => el.ToString()).Aggregate("", (z, el) => z + el + "\n")) { }

        public HTMLElement(string zawartosc) : this("html", zawartosc) { }
        public HTMLElement(HTMLElement zawartosc) : this(zawartosc.ToString()) { }
        public HTMLElement(List<HTMLElement> zawartosc) : this("html", zawartosc) { }

        public override string ToString()
        {
            return $"<{nazwa}>\n{zawartosc}\n</{nazwa}>";
        }
    }
}
