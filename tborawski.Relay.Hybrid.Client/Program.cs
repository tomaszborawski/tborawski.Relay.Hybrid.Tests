using System.Threading.Channels;
using System.Threading.Tasks;
using tborawski.Relay.Hybrid.Model;
using tborawski.Relay.Hybrid.Public;

namespace tborawski.Relay.Hybrid.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Go().Wait();
        }
        static async Task Go()
        {
            try
            {
                ConfigBootstrap.Init();
                var config = ConfigBootstrap.Read<HybidRelayConfiguration>("HybidRelayConfiguration");
                ClientChannelFactory<ITest, ITestClient> clientChannelFactory = new ClientChannelFactory<ITest, ITestClient>();

                using (var channel = await clientChannelFactory.CreateChannelAsync(config))
                {
                    Parallel.For(1, 5, i => Console.WriteLine($"Add({i}, 1) = {channel.Add(i, 1)}"));
                    Task.Run(async () => Console.WriteLine(await channel.AddAsync(2, 5))).Wait();
                    channel.TestMethod();
                }

                using (var channel2 = await clientChannelFactory.CreateChannelAsync(config))
                {

                    Parallel.For(1, 5, i => Console.WriteLine($"Add({i}, 1) = {channel2.Add(i, 1)}"));
                    Task.Run(async () => Console.WriteLine(await channel2.AddAsync(2, 5))).Wait();
                    channel2.TestMethod();

                    //Task.Run(async () => await channel2.TestAsync()).Wait();
                }

                using (var channel3 = await clientChannelFactory.CreateChannelAsync(config))
                {
                    await channel3.TestProgressAsync(new Progress<int>(p =>
                    {
                        Console.WriteLine($"Progres {p}");
                    }));
                }

                using (var channel4 = await clientChannelFactory.CreateChannelAsync(config))
                {
                    var cts = new CancellationTokenSource();

                    await channel4.TestOneWayAsync(new Progress<int>(p =>
                    {
                        Console.WriteLine($"Progres {p}");
                    }), cts.Token);
                    await Task.Delay(1000);
                    cts.Cancel();
                    await Task.Delay(500000);
                }

            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex!.InnerException!.GetType().Name);
                Console.WriteLine(ex!.InnerException!.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
