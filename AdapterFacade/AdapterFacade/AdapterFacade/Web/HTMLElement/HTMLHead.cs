using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class HTMLHead : HTMLElement
    {
        public HTMLHead(string zawartosc) : base("head", zawartosc) { }
        public HTMLHead(HTMLElement zawartosc) : this(zawartosc.ToString()) { }
    }
}
