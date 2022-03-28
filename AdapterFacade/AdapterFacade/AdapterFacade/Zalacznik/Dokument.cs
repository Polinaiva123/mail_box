using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class Dokument : Zalacznik
    {
        public void Otworz()
        {
            Console.WriteLine("To jest dokument");
        }

        public override string ToString()
        {
            return "Dokument";
        }
    }
}
