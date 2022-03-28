using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class HTMLBody : HTMLElement
    {
        public HTMLBody(string zawartosc) : base("body", zawartosc) { }
        public HTMLBody(HTMLElement zawartosc) : this(zawartosc.ToString()) { }

    }
}
