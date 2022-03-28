using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    // Adapter
    internal class HTMLWiadomoscEmail : HTMLStrona
    {
        private readonly WiadomoscEmail _wiadomosc;

        public HTMLWiadomoscEmail(WiadomoscEmail wiadomosc)
        {
            Tytul = wiadomosc.Tytul;
            Zawartosc = new HTMLParagraph(
                new HTMLBold("Od:").ToString() + new HTMLSpan(wiadomosc.Od).ToString() +
                new HTMLBold("Do:").ToString() + new HTMLSpan(wiadomosc.Do).ToString() +
                new HTMLBold("Tresc:").ToString() + new HTMLSpan(wiadomosc.Tresc).ToString()
            ).ToString() +
            new HTMLContainer(wiadomosc.Zalaczniki.Select(z => new HTMLLink(z.ToString())).Cast<HTMLElement>().ToList()).ToString();

            _wiadomosc = wiadomosc;
        }

        public WiadomoscEmail GetZrodlo()
        {
            return _wiadomosc;
        }
    }
}
