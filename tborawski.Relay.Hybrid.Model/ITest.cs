namespace tborawski.Relay.Hybrid.Model
{
    public interface ITest
    {
        int Add(int i, int j);
        Task<int> AddAsync(int i, int j);
        void TestMethod();
        Task TestAsync();

        Task<int> TestCancelationToken(CancellationToken cancellationToken);
        Task TestProgressAsync(IProgress<int> progress);
    }
}
