using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class HTMLTitle : HTMLElement
    {
        public HTMLTitle(string zawartosc) : base("title", zawartosc) { }
    }
}
