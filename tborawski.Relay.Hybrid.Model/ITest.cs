using tborawski.Relay.Hybrid.Attributes;

namespace tborawski.Relay.Hybrid.Model
{
    public interface ITest
    {
        int Add(int i, int j);
        Task<int> AddAsync(int i, int j);
        void TestMethod();
        Task TestAsync();

        [OneWay]
        Task TestOneWayAsync(IProgress<int> progress, CancellationToken cancellationToken);
        Task TestProgressAsync(IProgress<int> progress);
    }
}
