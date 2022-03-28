using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class HTMLLink : HTMLElement
    {
        public HTMLLink(string zawartosc) : base("a", zawartosc) { }
    }
}
