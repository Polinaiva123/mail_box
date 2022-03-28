using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class HTMLBold : HTMLElement
    {
        public HTMLBold(string zawartosc) : base("b", zawartosc) { }
        public HTMLBold(HTMLElement zawartosc) : this(zawartosc.ToString()) { }
    }
}
