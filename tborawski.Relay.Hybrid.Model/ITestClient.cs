using tborawski.Relay.Hybrid.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tborawski.Relay.Hybrid.Model
{
    public interface ITestClient :ITest, IClientChannel
    {
    }
}
