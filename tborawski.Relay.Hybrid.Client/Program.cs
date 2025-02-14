using tborawski.Relay.Hybrid.Model;

namespace tborawski.Relay.Hybrid.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConfigBootstrap.Init();
                var config = ConfigBootstrap.Read<HybidRelayConfiguration>("HybidRelayConfiguration");
                ClientChannelFactory<ITest, ITestClient> clientChannelFactory = new ClientChannelFactory<ITest, ITestClient>();
                using (var channel = clientChannelFactory.CreateChannel(config))
                {
                    Parallel.For(1, 100, i => Console.WriteLine($"Add({i}, 1) = {channel.Add(i, 1)}"));
                    Console.ReadLine();
                }
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
