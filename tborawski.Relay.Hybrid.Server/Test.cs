using tborawski.Relay.Hybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tborawski.Relay.Hybrid.Server
{
    internal class Test : ITest
    {
        public int Add(int i, int j)
        {
            return i + j;
        }
    }
}
