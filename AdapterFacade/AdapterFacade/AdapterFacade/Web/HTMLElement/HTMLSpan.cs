using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class HTMLSpan : HTMLElement
    {
        public HTMLSpan(string zawartosc) : base("span", zawartosc) { }
    }
}
