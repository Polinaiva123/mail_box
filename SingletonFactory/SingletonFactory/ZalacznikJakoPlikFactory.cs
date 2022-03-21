using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonFactory
{
    internal class ZalacznikJakoPlikFactory : ZalacznikFactory
    {
        public override Zalacznik CreateZalacznik(string rodzaj)
        {
            switch (rodzaj.ToLower())
            {
                case "plik":
                    return new Plik();
                default:
                    throw new ApplicationException("Nie rozpoznany rodzaj zalacznika");
            }
        }
    }
}
