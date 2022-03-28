using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class Obraz : Zalacznik
    {
        public void Otworz()
        {
            Console.WriteLine("To jest obraz");
        }
        public override string ToString()
        {
            return "Obraz";
        }
    }
}
