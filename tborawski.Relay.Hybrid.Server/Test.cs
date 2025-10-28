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

        public async Task<int> AddAsync(int i, int j)
        {
            await Task.Delay(1000);
            return i + j;
        }
        public void TestMethod()
        {

        }
        public async Task TestAsync()
        {
            throw new NotImplementedException();
        }

        public Task TestOneWayAsync(IProgress<int> progress, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    progress.Report(i * 10);
                    await Task.Delay(1000, cancellationToken);
                }
            });
        }

        public async Task TestProgressAsync(IProgress<int> progress)
        {
            for (int i = 0; i < 10; i++)
            {
                progress.Report(i * 10);
                await Task.Delay(1000);
            }
        }
    }
}
