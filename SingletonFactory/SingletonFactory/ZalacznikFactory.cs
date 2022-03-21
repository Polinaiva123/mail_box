using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonFactory
{
    internal abstract class ZalacznikFactory
    {
        public abstract Zalacznik CreateZalacznik(String rodzaj);
    }
}
