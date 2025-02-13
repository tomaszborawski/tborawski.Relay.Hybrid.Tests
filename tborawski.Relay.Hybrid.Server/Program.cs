namespace tborawski.Relay.Hybrid.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigBootstrap.Init();
            var config = ConfigBootstrap.Read<HybidRelayConfiguration>("HybidRelayConfiguration");
            var service = new ServiceHost<Test>();
            Task.Run(() => service.OpenAsync(config, CancellationToken.None));
            Console.WriteLine("Server is running");
            Console.ReadLine();
        }
    }
}
