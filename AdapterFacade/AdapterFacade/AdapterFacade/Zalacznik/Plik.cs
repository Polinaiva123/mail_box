using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterFacade
{
    internal class Plik : Zalacznik
    {
        public void Otworz()
        {
            Console.WriteLine("To jest plik");
        }

        public override string ToString()
        {
            return "Plik";
        }
    }
}
