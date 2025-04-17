using tborawski.Relay.Hybrid.Model;
using tborawski.Relay.Hybrid.Public;

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
                    Parallel.For(1, 5, i => Console.WriteLine($"Add({i}, 1) = {channel.Add(i, 1)}"));
                    Task.Run(async () => Console.WriteLine(await channel.AddAsync(2, 5))).Wait();
                    channel.TestMethod();
                }

                using (var channel2 = clientChannelFactory.CreateChannel(config))
                {

                    Parallel.For(1, 5, i => Console.WriteLine($"Add({i}, 1) = {channel2.Add(i, 1)}"));
                    Task.Run(async () => Console.WriteLine(await channel2.AddAsync(2, 5))).Wait();
                    channel2.TestMethod();

                    //Task.Run(async () => await channel2.TestAsync()).Wait();
                }

                using (var channel3 = clientChannelFactory.CreateChannel(config))
                {
                    var cts = new CancellationTokenSource();
                    Task.Run(async () =>
                    {
                        await Task.Delay(10000);
                        cts.Cancel();
                    });
                    Task.Run(() => channel3.TestCancelationToken(cts.Token)).Wait();
                }
            }
            catch (RelayMethodException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex!.InnerException!.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
