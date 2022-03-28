using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class HTMLParagraph : HTMLElement
    {
        public HTMLParagraph(string zawartosc) : base("p", zawartosc) { }
        public HTMLParagraph(HTMLElement zawartosc) : this(zawartosc.ToString()) { }

    }
}
